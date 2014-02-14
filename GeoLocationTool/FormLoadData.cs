// FormLoadData.cs

namespace GeoLocationTool
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Windows.Forms;

    public partial class FormLoadData : Form
    {
        #region Fields

        private const string BaracayCodeColumn = "BaracayCode";
        private const string MatchedColumn = "Matched";
        private const string MunicipalityCodeColumn = "MunicipalityCode";
        private const string ProvinceCodeColumn = "ProvinceCode";

        private DataTable dt;
        private GeoLocationData geoLocationData = new GeoLocationData();

        #endregion Fields

        #region Constructors

        public FormLoadData()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void AddCodeCollumns()
        {
            if (!dt.Columns.Contains(ProvinceCodeColumn))
            {
                dt.Columns.Add(ProvinceCodeColumn, typeof (String));
            }

            if (!dt.Columns.Contains(MunicipalityCodeColumn))
            {
                dt.Columns.Add(MunicipalityCodeColumn, typeof (String));
            }

            if (!dt.Columns.Contains(BaracayCodeColumn))
            {
                dt.Columns.Add(BaracayCodeColumn, typeof (String));
            }
        }

        private void btnManualMatch_Click(object sender, EventArgs e)
        {
            try
            {
                int locationColumn1 = (int)udProvince.Value - 1;
                int locationColumn2 = (int)udMunicipality.Value - 1;
                int locationColumn3 = (int)udBarangay.Value - 1;
                FormManualMatch formSuggestions = new FormManualMatch(
                    dt,
                    locationColumn1,
                    locationColumn2,
                    locationColumn3,
                    geoLocationData);
                formSuggestions.ShowDialog();
            }
            catch (Exception ex)
            {               
               ErrorHandler.Process("A problem occurred with the Manual Match screen load.", ex);
            }         
        }

        private void btnMatchData_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt == null)
                {
                    return;
                }

                int provinceColumnIndex = (int)udProvince.Value - 1;
                int municipalityColumnIndex = (int)udMunicipality.Value - 1;
                int barangayColumnIndex = (int)udBarangay.Value - 1;
                foreach (DataRow dataRow in dt.Rows)
                {
                    //get location
                    Location location = new Location();
                    location.Province =
                        dataRow.ItemArray[provinceColumnIndex].ToString();
                    location.Barangay =
                        dataRow.ItemArray[barangayColumnIndex].ToString();
                    location.Municipality =
                        dataRow.ItemArray[municipalityColumnIndex].ToString();

                    // get codes
                    geoLocationData.GetLocationCodes(location);

                    //display codes
                    dataRow[ProvinceCodeColumn] = location.ProvinceCode;
                    dataRow[MunicipalityCodeColumn] = location.MunicipalityCode;
                    dataRow[BaracayCodeColumn] = location.BarangayCode;
                    dataRow.AcceptChanges();
                }
                dt.AcceptChanges();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                ErrorHandler.Process("A problem occurred with the data matching process.", ex);
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
                    geoLocationData =
                        new GeoLocationData(LocationGadmFile.ReadLocationFile(path));
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

        private string GetFileName(string filter)
        {
            //ask the user for the input file name
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

        private void ReadCsvFile()
        {
            //read the input file into the grid and add extra columns for the computed data
            const string filter = "csv files (*.csv)|*.csv";
            txtFileName.Clear();
            txtFileName.Text = GetFileName(filter);
            var path = txtFileName.Text.Trim();
            if (!String.IsNullOrWhiteSpace(path))
            {
                ReadCsvInput(path);
                AddCodeCollumns();
            }
        }

        private void ReadCsvInput(string fileName)
        {
            dt = InputFile.ReadCsvFile(fileName, true);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ReadExcelFile()
        {
            //read the input file into the grid and add extra columns for the computed data
            const string filter = "excel files (*.xls,*.xlsx)|*.xls*";
            txtFileName.Clear();
            txtFileName.Text = GetFileName(filter);
            var path = txtFileName.Text.Trim();

            // todo provide the user with a list of worksheet names for the selected file

            string worksheetName = txtWorksheetName.Text;
            if (!String.IsNullOrWhiteSpace(path))
            {
                ReadExcelInput(path, worksheetName);
                AddCodeCollumns();
            }
        }

        private void ReadExcelInput(string path, string worksheetName)
        {
            dt = InputFile.ReadExcelFile(path, worksheetName);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                    OutputFile.SaveToCsvFile(dialog.FileName, dt);
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
                    OutputFile.SaveToExcelFile(dialog.FileName, dt);
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
        }

        #endregion Methods
    }
}