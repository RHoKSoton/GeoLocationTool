// FormLoadData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Windows.Forms;
    using DataAccess;
    using Logic;

    /// <summary>
    /// Initial form: displays options and loads the input data into a grid.
    /// </summary>
    public partial class FormLoadData : Form
    {
        #region Fields

        private readonly InputData inputData;

        private LocationData locationData;

        #endregion Fields

        #region Constructors

        public FormLoadData()
        {
            InitializeComponent();
            inputData = new InputData();
            locationData = new LocationData();
        }

        #endregion Constructors

        #region Methods

        public void SaveAsCsv()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "csv";
                dialog.Filter = "CSV(*.csv)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OutputFile.SaveToCsvFile(dialog.FileName, inputData.dt);
                }
            }
        }

        public void SaveAsExcel()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "xlsx";
                dialog.Filter = "Excel(*.xlsx)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OutputFile.SaveToExcelFile(dialog.FileName, inputData.dt);
                }
            }
        }

        private void btnManualMatch_Click(object sender, EventArgs e)
        {
            try
            {
                inputData.ColumnIndexLoc1 = (int) udProvince.Value - 1;
                inputData.ColumnIndexLoc2 = (int) udMunicipality.Value - 1;
                inputData.ColumnIndexLoc3 = (int) udBarangay.Value - 1;
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
                if (inputData.dt == null)
                {
                    return;
                }

                inputData.ColumnIndexLoc1 = (int) udProvince.Value - 1;
                inputData.ColumnIndexLoc2 = (int) udMunicipality.Value - 1;
                inputData.ColumnIndexLoc3 = (int) udBarangay.Value - 1;

                inputData.AddLocationCodes(locationData);
                dataGridView1.DataSource = inputData.dt;
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the data matching process.",
                    ex);
            }
        }

        private void btnReadInputFile_Click(object sender, EventArgs e)
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

        private void btnReadLocationFile_Click(object sender, EventArgs e)
        {
            try
            {
                const string filter = "csv files (*.csv)|*.csv";
                txtLocationFileName.Clear();
                txtLocationFileName.Text = GetFileName(filter);
                var path = txtLocationFileName.Text.Trim();
                if (!String.IsNullOrWhiteSpace(path))
                {
                    locationData =
                        new LocationData(LocationGadmFile.ReadLocationFile(path));
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

        /// <summary>
        /// Gets the name of the file from the user.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The file name and path.</returns>
        private string GetFileName(string filter)
        {
            string fileName = string.Empty;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Filter = filter;

            // todo check the filter index values
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                if (!File.Exists(fileName))
                {
                    MessageBox.Show(
                        "File does not exist:\r\n" + fileName,
                        "No File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
            }
            return fileName;
        }

        /// <summary>
        /// Reads the CSV file into the grid and add extra columns for the computed data
        /// </summary>
        private void ReadCsvFile()
        {
            const string filter = "csv files (*.csv)|*.csv";
            txtFileName.Clear();
            txtFileName.Text = GetFileName(filter);
            var path = txtFileName.Text.Trim();
            if (!String.IsNullOrWhiteSpace(path))
            {
                inputData.dt = InputFile.ReadCsvFile(path, true);
                dataGridView1.DataSource = inputData.dt;
                dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
                inputData.AddCodeCollumns();
            }
        }

        /// <summary>
        /// Reads the excel file into the grid and adds extra columns for the computed data
        /// </summary>
        private void ReadExcelFile()
        {
            const string filter = "excel files (*.xls,*.xlsx)|*.xls*";
            txtFileName.Clear();
            txtFileName.Text = GetFileName(filter);
            var path = txtFileName.Text.Trim();

            // todo provide the user with a list of worksheet names for the selected file

            string worksheetName = txtWorksheetName.Text;
            if (!String.IsNullOrWhiteSpace(path))
            {
                inputData.dt = InputFile.ReadExcelFile(path, worksheetName);
                dataGridView1.DataSource = inputData.dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                inputData.AddCodeCollumns();
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
        }

        #endregion Methods
    }
}