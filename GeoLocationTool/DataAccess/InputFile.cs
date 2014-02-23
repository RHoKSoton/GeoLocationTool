// InputFile.cs

namespace GeoLocationTool.DataAccess
{
    using CsvHelper;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Read the input data from file
    /// </summary>
    internal static class InputFile
    {
        #region Methods

        internal static DataTable ReadCsvFileOld(string path, bool isFirstRowHeader, string delimiter)
        {
            string header = isFirstRowHeader ? "Yes" : "No";
            string extendedProperties = "Text;HDR=" + header;
            if (delimiter != null)
            {
                extendedProperties += ";FMT=" + delimiter;
            }

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                ";Extended Properties=\"" + extendedProperties + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        internal static DataTable ReadCsvFile(string path, bool isFirstRowHeader, string delimiter = ",")
        {
            DataTable dataTable = new DataTable();
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var csvReader = new CsvReader(new StreamReader(fileStream)))
                {
                    csvReader.Configuration.Delimiter = delimiter;
                    while (csvReader.Read())
                    {
                        if (dataTable.Columns.Count == 0)
                        {
                            if (isFirstRowHeader)
                            {
                                foreach (var field in csvReader.FieldHeaders)
                                    dataTable.Columns.Add(field);
                            }
                            else
                            {
                                for (int j = 0; j < csvReader.FieldHeaders.Length; j++)
                                    dataTable.Columns.Add((j+1).ToString());
                            }
                        }

                        DataRow row = dataTable.NewRow();
                        foreach (var field in csvReader.FieldHeaders)
                        {
                            row[field] = csvReader.GetField(field).Trim();
                        }
                        dataTable.Rows.Add(row);
                    }
                }
            }
            return dataTable;
        }

        internal static DataTable ReadExcelFile(string path, string worksheetName)
        {
            //todo make this read all versions of excel
            var connectionString =
                String.Format(
                    "Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=Excel 12.0;",
                    path);

            string commandText = String.Format("SELECT *  FROM [{0}$]", worksheetName);

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                commandText,
                connectionString))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        #endregion Methods
    }
}