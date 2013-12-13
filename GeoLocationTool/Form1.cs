// Form1.cs
// This code needs a really good clean up!
// also the Excel functionality  needs the work sheet selection added to a dropdown and then reenabled
// 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using CsvHelper;

namespace GeoLocationTool
{
    public partial class Form1 : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt;
        private OleDbDataAdapter adapter;

        private IEnumerable<Gadm> gadmList = new List<Gadm>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set defaults
            udBarangay.DecimalPlaces = 0;
            udProvince.DecimalPlaces = 0;
            udMunicipality.DecimalPlaces = 0;
            udProvince.Value = 1;
            udMunicipality.Value = 2;
            udBarangay.Value = 3;
            txtWorksheetName.Text = "sheet1";
        }

        private void btnReadExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                //read excel files
                const string filter = "excel files (*.xls,*.xlsx)|*.xls*";
                string fileName = GetFileName(filter);
                if (String.IsNullOrWhiteSpace(fileName)) return;

                txtFileName.Text = fileName;
                ReadExcelFile(fileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "There was an error loading the file:\r\n" + ex.Message, "IO Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReadCsv_Click(object sender, EventArgs e)
        {
            try
            {
                //read csv files
                const string filter = "csv files (*.csv)|*.csv";
                string fileName = GetFileName(filter);
                if (String.IsNullOrWhiteSpace(fileName)) return;

                txtFileName.Text = fileName;
                ReadCsvFile(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void btnReadGADMData_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "csv files (*.csv)|*.csv";
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
                            using (var csvReader = new CsvReader(new StreamReader(myStream)))
                            {
                                gadmList = csvReader.GetRecords<Gadm>().ToList();
                            }
                        }
                    }
                }
            

        }
            catch (Exception ex)
            {
                MessageBox.Show(this, "There was an error loading the CSV file:\r\n" + ex.Message, "IO Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMatchData_Click(object sender, EventArgs e)
        {
            if (dt == null)
            {
                return;
            }
            AddCodeCollumns();

            foreach (DataRow dataRow in dt.Rows)
            {
                Location location = new Location();
                location.Province = dataRow.ItemArray[(int)udProvince.Value - 1].ToString();
                location.Baracay = dataRow.ItemArray[(int)udBarangay.Value - 1].ToString();
                location.Municipality = dataRow.ItemArray[(int)udMunicipality.Value - 1].ToString();
                AddCodes(location);
                dataRow["ProvinceCode"] = location.ProvinceCode;
                dataRow["MunicipalityCode"] = location.MunicipalityCode;
                dataRow["BaracayCode"] = location.BaracayCode;
                dataRow.AcceptChanges();
            }
            dt.AcceptChanges();

            dataGridView1.DataSource = dt;
        }
      
        private void btnSaveCsv_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "csv";
                dialog.Filter = "CSV(*.csv)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveAsCsv(dialog.FileName);
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            // save as excel
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = "xlsx";
                dialog.Filter = "Excel(*.xlsx)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveAsExcel(dialog.FileName);
                }
            }
        }

        private void AddCodeCollumns()
        {
            if (!dt.Columns.Contains("ProvinceCode"))
            {
                dt.Columns.Add("ProvinceCode", typeof(String));
            }

            if (!dt.Columns.Contains("MunicipalityCode"))
            {
                dt.Columns.Add("MunicipalityCode", typeof(String));
            }
            if (!dt.Columns.Contains("BaracayCode"))
            {
                dt.Columns.Add("BaracayCode", typeof(String));
            }
        }

        private void AddCodes(Location location)
        {
            Location location1 = location;
            var matchRecords = from record in gadmList
                where
                    (String.Equals(record.NAME_1, location1.Province.Trim(), StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(record.NAME_2, location1.Municipality.Trim(), StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(record.NAME_3, location1.Baracay.Trim(), StringComparison.OrdinalIgnoreCase))
                select record;
          
            var firstOrDefault = matchRecords.FirstOrDefault();
            if (firstOrDefault != null)
            {
                location.ProvinceCode = firstOrDefault.ID_1;
                location.MunicipalityCode = firstOrDefault.ID_2;
                location.BaracayCode = firstOrDefault.ID_3;
            }
        }

        private void ReadCsvFile(string fileName)
        {
            dt = GetDataTableFromCsv(fileName, true);
            ds.Tables.Add(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private static string GetFileName(string filter)
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
                    MessageBox.Show("File does not exist:\r\n" + fileName, "No File", MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
            }

            return fileName;
        }

        private void ReadExcelFile(string path)
        {
            var connectionString = 
                string.Format(
                    "Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;",
                    path);
            string worksheetName = txtWorksheetName.Text;
            adapter = new OleDbDataAdapter("SELECT *  FROM [" + worksheetName + "$]", connectionString);
            ds = new DataSet();

            adapter.Fill(ds, "input");
            dt = ds.Tables["input"];
            dataGridView1.DataSource = dt;
        }

        private static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private void SaveAsExcel(string fileName)
        {
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(ds);
            wb.SaveAs(fileName);
        }

        private void SaveAsCsv(string fileName)
        {
            var lines = new List<string>();

            string[] columnNames = dt.Columns.Cast<DataColumn>().
                Select(column => column.ColumnName).
                ToArray();

            var header = string.Join(",", columnNames);
            lines.Add(header);

            var valueLines = dt.AsEnumerable()
                .Select(row => string.Join(",", EscapeQuotes(row.ItemArray)));

            lines.AddRange(valueLines);

            File.WriteAllLines(fileName, lines);
        }

        private string[] EscapeQuotes(object[] itemArray)
        {
            string[] escaped = new string[itemArray.Length];

            for (int i = 0; i < itemArray.Length; i++)
            {
                escaped[i] = "\"" + itemArray[i] + "\"";
            }

            return escaped;
        }

        public string[] GetExcelSheetNames(string excelFileName)
        {
            OleDbConnection con = null;
            DataTable dt2 = null;
            String conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + excelFileName +
                            ";Extended Properties=Excel 8.0;";
            con = new OleDbConnection(conStr);
            con.Open();
            dt2 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

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

    }
}