// FileImport.cs

namespace MultiLevelGeoCoder.DataAccess
{
    using System;
    using System.Data;
    using System.IO;
    using CsvHelper;

    /// <summary>
    /// Helper class to read data from file.
    /// </summary>
    internal class FileImport
    {
        #region Methods

        /// <summary>
        /// Reads the CSV file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="isFirstRowHeader">True if first row is a header row</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>A data table</returns>
        public static DataTable ReadCsvFile(
            string path,
            bool isFirstRowHeader,
            string delimiter = ",")
        {
            {
                DataTable dataTable = new DataTable();
                using (
                    var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
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
                                    for (int j = 0;
                                        j < csvReader.FieldHeaders.Length;
                                        j++)
                                        dataTable.Columns.Add((j + 1).ToString());
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
        }

        internal DataTable ReadTabDelimFile(string path)
        {
            // "\t"
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}