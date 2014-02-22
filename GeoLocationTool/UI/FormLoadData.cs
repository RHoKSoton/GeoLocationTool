// FormLoadData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Windows.Forms;
    using Logic;

    /// <summary>
    /// Main form: displays options and loads the input data into a grid.
    /// </summary>
    public partial class FormLoadData : Form
    {
        #region Fields

        private readonly InputData inputData;
        private readonly LocationData locationData;

        #endregion Fields

        #region Constructors

        public FormLoadData(LocationData locationData)
        {
            this.locationData = locationData;
            InitializeComponent();
            inputData = new InputData();
        }

        #endregion Constructors

        #region Methods

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnManualMatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (locationData == null)
                {
                    MessageBox.Show(
                        "Location data missing, please read in a location file.");
                    return;
                }

                if (inputData.dt == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.");
                    return;
                }

                inputData.AddAdditionalColumns();

                if (OriginalColumnIndicesHaveChanged())
                {
                    SetOriginalColumnIndices();
                    inputData.InitialiseLocationColumns();
                }
                else
                {
                    SetOriginalColumnIndices();
                }

                FormManualMatch formManualMatch = new FormManualMatch(
                    inputData,
                    locationData);
                formManualMatch.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the Manual Match screen load.",
                    ex);
            }
        }

        private void btnMatchData_Click(object sender, EventArgs e)
        {
            try
            {
                if (locationData == null)
                {
                    MessageBox.Show(
                        "Location data missing, please read in a location file.");
                    return;
                }

                if (inputData.dt == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.");
                    return;
                }

                inputData.AddAdditionalColumns();

                if (OriginalColumnIndicesHaveChanged())
                {
                    SetOriginalColumnIndices();
                    inputData.InitialiseLocationColumns();
                }
                else
                {
                    SetOriginalColumnIndices();
                }

                inputData.GetLocationCodes(locationData);
                dataGridView1.DataSource = inputData.dt;
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the data matching process.",
                    ex);
            }
        }

        private void btnLoadInputFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoImportCsv.Checked)
                {
                    ReadCsvFile();
                }
                else
                {
                    ReadExcelFile();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Could not read file.", ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoSaveAsCsv.Checked)
                {
                    SaveAsCsv();
                }
                else
                {
                    SaveAsExcel();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("Error saving file.", ex);
            }
        }

        private void FormLoadData_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private string[] GetExcelSheetNames(string excelFileName)
        {
            String conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                            excelFileName +
                            ";Extended Properties=Excel 8.0;";
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            DataTable dt2 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt2 == null)
            {
                return null;
            }

            String[] excelSheetNames = new String[dt2.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt2.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }

        private bool OriginalColumnIndicesHaveChanged()
        {
            int loc1ColumnIndex = (int) udProvince.Value - 1;
            int loc2ColumnIndex = (int) udMunicipality.Value - 1;
            int loc3ColumnIndex = (int) udBarangay.Value - 1;
            bool isSame =
                inputData.OriginalLoc1ColumnIndex.Equals(loc1ColumnIndex) &&
                inputData.OriginalLoc2ColumnIndex.Equals(loc2ColumnIndex) &&
                inputData.OriginalLoc3ColumnIndex.Equals(loc3ColumnIndex);

            return !isSame;
        }

        /// <summary>
        /// Reads the CSV file into the grid and add extra columns for the computed data
        /// </summary>
        private void ReadCsvFile()
        {
            const string filter = "csv files (*.csv)|*.csv";
            txtFileName.Clear();
            txtFileName.Text = UiHelper.GetFileName(filter);
            var path = txtFileName.Text.Trim();
            if (!String.IsNullOrWhiteSpace(path))
            {
                const bool isFirstRowHeader = true;
                inputData.LoadCsvFile(path, isFirstRowHeader);
                dataGridView1.DataSource = inputData.dt;
                dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
                SetColumnStyle();
            }
        }

        /// <summary>
        /// Reads the excel file into the grid and adds extra columns for the computed data
        /// </summary>
        private void ReadExcelFile()
        {
            const string filter = "excel files (*.xls,*.xlsx)|*.xls*";
            txtFileName.Clear();
            txtFileName.Text = UiHelper.GetFileName(filter);
            var path = txtFileName.Text.Trim();

            // todo provide the user with a list of worksheet names for the selected file

            string worksheetName = txtWorksheetName.Text;
            if (!String.IsNullOrWhiteSpace(path))
            {
                inputData.LoadExcelFile(path, worksheetName);
                dataGridView1.DataSource = inputData.dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                SetColumnStyle();
            }
        }

        private void SaveAsCsv()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "csv";
                dialog.Filter = "CSV(*.csv)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    inputData.SaveToCsvFile(dialog.FileName);
                }
            }
        }

        private void SaveAsExcel()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "xlsx";
                dialog.Filter = "Excel(*.xlsx)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    inputData.SaveToExcelFile(dialog.FileName);
                }
            }
        }

        private void SetColumnStyle()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.ReadOnly)
                {
                    col.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void SetDefaults()
        {
            udBarangay.DecimalPlaces = 0;
            udProvince.DecimalPlaces = 0;
            udMunicipality.DecimalPlaces = 0;
            udProvince.Value = 1;
            udMunicipality.Value = 2;
            udBarangay.Value = 3;
            txtWorksheetName.Text = "Sheet1";
            rdoImportCsv.Checked = true;
            rdoSaveAsCsv.Checked = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
        }

        private void SetOriginalColumnIndices()
        {
            inputData.OriginalLoc1ColumnIndex = (int) udProvince.Value - 1;
            inputData.OriginalLoc2ColumnIndex = (int) udMunicipality.Value - 1;
            inputData.OriginalLoc3ColumnIndex = (int) udBarangay.Value - 1;
        }

        #endregion Methods
    }
}