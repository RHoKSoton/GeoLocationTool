// InputTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using System.Data;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Provides test data to represent the input data table
    /// </summary>
    public class InputTestData
    {
        #region Fields

        private readonly List<KeyValuePair<int, string[]>> lines2 =
            new List<KeyValuePair<int, string[]>>();

        #endregion Fields

        #region Methods

        public void AddLine(string[] line)
        {
            int lineNumber = lines2.Count + 1;
            lines2.Add(new KeyValuePair<int, string[]>(lineNumber, line));
        }

        public DataTable Data(InputColumnNames inputColumnNames)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Row");
            dt.Columns.Add(inputColumnNames.Level1);
            dt.Columns.Add(inputColumnNames.Level2);
            dt.Columns.Add(inputColumnNames.Level3);

            foreach (KeyValuePair<int, string[]> line in lines2)
            {
                object[] values = {line.Key, line.Value[0], line.Value[1], line.Value[2]};
                dt.LoadDataRow(values, true);
            }

            return dt;
        }

        #endregion Methods
    }
}