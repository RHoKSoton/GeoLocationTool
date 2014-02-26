// FileExport.cs

namespace MultiLevelGeoCoder.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Helper class to write data to file.
    /// </summary>
    internal class FileExport
    {
        #region Methods

        internal static void SaveToCsvFile(string fileName, DataTable data)
        {
            var lines = new List<string>();

            string[] columnNames = data.Columns.Cast<DataColumn>().
                Select(column => column.ColumnName).
                ToArray();

            var header = String.Join(",", columnNames);
            lines.Add(header);

            var valueLines = EnumerableRowCollectionExtensions.Select(
                data.AsEnumerable(),
                row => String.Join(",", EscapeQuotes(row.ItemArray)));

            lines.AddRange(valueLines);

            File.WriteAllLines(fileName, lines);
        }

        private static string[] EscapeQuotes(IList<object> itemArray)
        {
            // todo only quote the fields that need it
            string[] escaped = new string[itemArray.Count];

            for (int i = 0; i < itemArray.Count; i++)
            {
                escaped[i] = "\"" + itemArray[i] + "\"";
            }

            return escaped;
        }

        #endregion Methods

        #region Other

        //internal static void SaveToExcelFile(string fileName, DataTable data)
        //{
        //    XLWorkbook wb = new XLWorkbook();
        //    //todo use the table name if it has one
        //    data.TableName = "Sheet1";
        //    wb.Worksheets.Add(data);
        //    wb.SaveAs(fileName);
        //}

        #endregion Other
    }
}