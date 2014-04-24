// GazetteerData.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Holds all the gazetter data, and details of the columns to be used.
    /// Provides a subset of the data that just contains the required data.
    /// </summary>
    internal class GazetteerData
    {
        #region Constructors

        public GazetteerData(DataTable data)
        {
            Data = data;
        }

        #endregion Constructors

        #region Properties

        public GazetteerColumnNames ColumnNames { get; set; }

        public DataTable Data { get; private set; }

        /// <summary>
        /// Provides a location list containing just the columns in the Column Names.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public List<GazetteerRecord> LocationList
        {
            get
            {
                // create a new list of  just the gazetteer data that we need
                List<GazetteerRecord> locationCodeList = new List<GazetteerRecord>();
                foreach (DataRow row in Data.Rows)
                {
                    AddRecord(locationCodeList, row);
                }
                return locationCodeList;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Provides a list of all the column header names present in the gazetteer data.
        /// </summary>
        /// <returns>List of column names</returns>
        public IList<string> AllColumnNames()
        {
            List<string> list =
                (from DataColumn dataColumn in Data.Columns select dataColumn.ColumnName)
                    .ToList();

            return list;
        }

        private static void SetValue(
            GazetteerRecord gazetteerRecord,
            string propertyName,
            DataRow row,
            string columnName)
        {
            if (!string.IsNullOrEmpty(columnName))
            {
                Type type = gazetteerRecord.GetType();
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                propertyInfo.SetValue(
                    gazetteerRecord,
                    row[columnName].ToString(),
                    new object[] {});
            }
        }

        private void AddRecord(ICollection<GazetteerRecord> locationCodeList, DataRow row)
        {
            GazetteerRecord gazetteerRecord = new GazetteerRecord();

            SetValue(gazetteerRecord, "Name1", row, ColumnNames.Level1Name);
            SetValue(gazetteerRecord, "Name2", row, ColumnNames.Level2Name);
            SetValue(gazetteerRecord, "Name3", row, ColumnNames.Level3Name);

            SetValue(gazetteerRecord, "Id1", row, ColumnNames.Level1Code);
            SetValue(gazetteerRecord, "Id2", row, ColumnNames.Level2Code);
            SetValue(gazetteerRecord, "Id3", row, ColumnNames.Level3Code);

            SetValue(gazetteerRecord, "AltName1", row, ColumnNames.Level1AltName);
            SetValue(gazetteerRecord, "AltName2", row, ColumnNames.Level2AltName);
            SetValue(gazetteerRecord, "AltName3", row, ColumnNames.Level3AltName);

            locationCodeList.Add(gazetteerRecord);
        }

        #endregion Methods
    }
}