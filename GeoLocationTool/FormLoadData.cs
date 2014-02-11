// FormLoadData.cs

namespace GeoLocationTool
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using CsvHelper;

    //using DocumentFormat.OpenXml.Wordprocessing;
    public partial class FormLoadData : Form
    {
        #region Fields

        private const string BaracayCodeColumn = "BaracayCode";
        private const string MatchedColumn = "Matched";
        private const string MunicipalityCodeColumn = "MunicipalityCode";
        private const string ProvinceCodeColumn = "ProvinceCode";
     
        private DataTable dt;
        private IEnumerable<Gadm> gadmList = new List<Gadm>();
        private GeoLocationData geoLocationData = new GeoLocationData();

        #endregion Fields

        #region Constructors

        public FormLoadData()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private static bool HasValue(DataGridViewRow row, string columnName)
        {
            var code = row.Cells[columnName].Value;
            return ! string.IsNullOrEmpty(code as string);
        }

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

        private void AddMatchedColumn()
        {
            if (!dt.Columns.Contains(MatchedColumn))
            {
                DataGridViewCheckBoxColumn matched = new DataGridViewCheckBoxColumn();
                matched.HeaderText = MatchedColumn;
                matched.Name = MatchedColumn;
                dataGridView1.Columns.Add(matched);
            }
        }

        private void btnFuzzyMatch_Click(object sender, EventArgs e)
        {
            int locationColumn1 = (int) udProvince.Value - 1;
            int locationColumn2 = (int) udMunicipality.Value - 1;
            int locationColumn3 = (int) udBarangay.Value - 1;
            FormManualMatch formSuggestions = new FormManualMatch(
                dt,
                locationColumn1,
                locationColumn2,
                locationColumn3,
                geoLocationData);
            formSuggestions.ShowDialog();
        }

        private void btnMatchData_Click(object sender, EventArgs e)
        {
            if (dt == null)
            {
                return;
            }

            foreach (DataRow dataRow in dt.Rows)
            {
                //get  input location from the row
                Location location = new Location();
                location.Province =
                    dataRow.ItemArray[(int) udProvince.Value - 1].ToString();
                location.Baracay =
                    dataRow.ItemArray[(int) udBarangay.Value - 1].ToString();
                location.Municipality =
                    dataRow.ItemArray[(int) udMunicipality.Value - 1].ToString();

                geoLocationData.AddCodesToLocation(location);

                //display codes
                dataRow[ProvinceCodeColumn] = location.ProvinceCode;
                dataRow[MunicipalityCodeColumn] = location.MunicipalityCode;
                dataRow[BaracayCodeColumn] = location.BaracayCode;
                dataRow.AcceptChanges();
            }
            dt.AcceptChanges();
            dataGridView1.DataSource = dt;

            //tell the user which rows are matched
            UpdateMatchedColumn();
        }

        private void btnReadCsv_Click(object sender, EventArgs e)
        {
            try
            {
                //read the input file into the grid and add extra columns for the computed data
                const string filter = "csv files (*.csv)|*.csv";
                GetInputFileName(filter);
                var fileName = txtFileName.Text.Trim();
                if (!String.IsNullOrWhiteSpace(fileName))
                {
                    ReadCsvInput(fileName);
                    AddCodeCollumns();
                    AddMatchedColumn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: Could not read file. Original error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnReadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //read the input file into the grid and add extra columns for the computed data
                const string filter = "excel files (*.xls,*.xlsx)|*.xls*";
                GetInputFileName(filter);
                var fileName = txtFileName.Text.Trim();

                // todo provide the user with a list of worksheet names for the selected file

                string worksheetName = txtWorksheetName.Text;
                if (!String.IsNullOrWhiteSpace(fileName))
                {
                    ReadExcelInput(fileName, worksheetName);
                    AddCodeCollumns();
                    AddMatchedColumn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: Could not read file. Original error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnReadLocation_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "csv files (*.csv)|*.csv";
                openFileDialog1.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    txtGadmFileName.Text = fileName;
                    if (!File.Exists(fileName))
                    {
                        MessageBox.Show(
                            this,
                            "File does not exist:\r\n" + fileName,
                            "No File",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                    }
                    else
                    {
                        // open for read only
                        using (Stream myStream = openFileDialog1.OpenFile())
                        {
                            // this data is a known format so we can use a strongly typed reader
                            using (
                                var csvReader = new CsvReader(new StreamReader(myStream)))
                            {
                                gadmList = csvReader.GetRecords<Gadm>().ToList();
                                geoLocationData = new GeoLocationData(gadmList);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "There was an error loading the CSV file:\r\n" + ex.Message,
                    "IO Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSaveCsv_Click(object sender, EventArgs e)
        {
            //save as csv
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

        private void btnSaveExcel_Click(object sender, EventArgs e)
        {
            // save as excel
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

        private void GetInputFileName(string filter)
        {
            //ask the user for the input file name
            txtFileName.Clear();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Filter = filter;

            // todo check the filter index values
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                if (!File.Exists(fileName))
                {
                    MessageBox.Show(
                        "File does not exist:\r\n" + fileName,
                        "No File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                else
                {
                    txtFileName.Text = fileName;
                }
            }
        }

        private void ReadCsvInput(string fileName)
        {
            dt = InputFile.ReadCsvFile(fileName, true);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ReadExcelInput(string path, string worksheetName)
        {
            dt = InputFile.ReadExcelFile(path, worksheetName);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
        }

        private void UpdateMatchedColumn()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (HasValue(row, ProvinceCodeColumn) &&
                    HasValue(row, MunicipalityCodeColumn) &&
                    HasValue(row, BaracayCodeColumn))
                {
                    row.Cells[MatchedColumn].Value = true;
                }
                else
                {
                    row.Cells[MatchedColumn].Value = false;
                }
            }
        }

        #endregion Methods
    }
}