// FormLoadData.cs

namespace GeoLocationTool.UI
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Windows.Forms;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Main form: displays options and loads the input data into a grid.
    /// </summary>
    public partial class FormLoadData : Form
    {
        #region Fields

        private readonly GeoCoder geoCoder;

        #endregion Fields

        #region Constructors

        public FormLoadData(LocationData gazetteer)
        {
            InitializeComponent();
            geoCoder = new GeoCoder(gazetteer);            
        }

        #endregion Constructors

        #region Methods

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLoadInputFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoImportCsv.Checked)
                {
                    ReadCsvFile();
                }
                else if (rdoImportTabDelim.Checked)
                {
                    ReadTabDelimFile();
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

        private void btnManualMatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (geoCoder.Gazetteer == null)
                {
                    MessageBox.Show(
                        "Gazetteer data missing, please read in a gazetteer file.");
                    return;
                }

                if (geoCoder.InputDataTable == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.");
                    return;
                }

                FormManualMatch formManualMatch = new FormManualMatch(geoCoder);
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
                if (geoCoder.Gazetteer == null)
                {
                    MessageBox.Show(
                        "Gazetteer data missing, please read in a gazetteer file.");
                    return;
                }

                if (geoCoder.InputDataTable == null)
                {
                    MessageBox.Show("Input data missing, please read in an input file.");
                    return;
                }

                SetOriginalColumnIndices();
                geoCoder.InitialiseLocationColumns();

                geoCoder.MatchAll();
                dataGridView1.DataSource = geoCoder.InputDataTable;
            }
            catch (Exception ex)
            {
                ErrorHandler.Process(
                    "A problem occurred with the data matching process.",
                    ex);
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

        private void ReadCsvFile()
        {
            const string filter = "csv files (*.csv)|*.csv";
            txtFileName.Clear();
            txtFileName.Text = UiHelper.GetFileName(filter);
            var path = txtFileName.Text.Trim();
            if (!String.IsNullOrWhiteSpace(path))
            {
                geoCoder.LoadInputFileCsv(path);
                dataGridView1.DataSource = geoCoder.InputDataTable;
                dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
                SetColumnStyle();
            }
        }

        private void ReadExcelFile()
        {
            //const string filter = "excel files (*.xls,*.xlsx)|*.xls*";
            //txtFileName.Clear();
            //txtFileName.Text = UiHelper.GetFileName(filter);
            //var path = txtFileName.Text.Trim();

            // todo provide the user with a list of worksheet names for the selected file
            // todo remove this
            //string worksheetName = txtWorksheetName.Text;
            //if (!String.IsNullOrWhiteSpace(path))
            //{
            //    inputData.LoadExcelFile(path, worksheetName);
            //    dataGridView1.DataSource = inputData.dt;
            //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    SetColumnStyle();
            //}
        }

        private void ReadTabDelimFile()
        {
            const string filter = "tab delimited files (*.csv)|*.csv";
            txtFileName.Clear();
            txtFileName.Text = UiHelper.GetFileName(filter);
            var path = txtFileName.Text.Trim();
            if (!String.IsNullOrWhiteSpace(path))
            {
                geoCoder.LoadInputFileTabDelim(path);
                dataGridView1.DataSource = geoCoder.InputDataTable;
                dataGridView1.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
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
                    geoCoder.SaveToCsvFile(dialog.FileName);
                }
            }
        }

        private void SaveAsExcel()
        {
            //using (SaveFileDialog dialog = new SaveFileDialog())
            //{
            //    dialog.AddExtension = true;
            //    dialog.DefaultExt = "xlsx";
            //    dialog.Filter = "Excel(*.xlsx)|*.*";
            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        inputData.SaveToExcelFile(dialog.FileName);
            //    }
            //}
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
            ColumnHeaderIndices indices = new ColumnHeaderIndices();
            indices.Admin1 = (int) udProvince.Value - 1;
            indices.Admin2 = (int) udMunicipality.Value - 1;
            indices.Admin3 = (int) udBarangay.Value - 1;

            geoCoder.SetOriginalInputColumns(indices);
        }

        #endregion Methods
    }
}