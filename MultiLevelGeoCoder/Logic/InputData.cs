// InputData.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Holds the input data and added codes
    /// </summary>
    public class InputData
    {
        #region Fields

        public const string Alt1ColumnName = "Alt Name 1";
        public const string Alt2ColumnName = "Alt Name 2";
        public const string Alt3ColumnName = "Alt Name 3";
        public const string Level1CodeColumnName = "Code 1";
        public const string Level2CodeColumnName = "Code 2";
        public const string Level3CodeColumnName = "Code 3";

        private const string DefaultLevel1ColumnName = "Admin2";
        private const string DefaultLevel2ColumnName = "Admin3";
        private const string DefaultLevel3ColumnName = "Admin4";

        #endregion Fields

        #region Constructors

        public InputData(DataTable data)
        {
            this.data = data;
            AddAdditionalColumns();
            SetColumnsAsReadOnly();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The names of the columns that contain the data to be matched.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames ColumnNames { get; set; }

        public DataTable data { get; set; }

        /// <summary>
        /// The default names of the columns that contain the input data to be matched.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames DefaultColumnNames
        {
            get
            {
                InputColumnNames defaults = new InputColumnNames
                {
                    Level1 = DefaultLevel1ColumnName,
                    Level2 = DefaultLevel2ColumnName,
                    Level3 = DefaultLevel3ColumnName
                };
                return defaults;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the matched location codes.
        /// </summary>
        /// <param name="gazetteer">The gazetteer.</param>
        public void AddMatchedLocationCodes(LocationCodes gazetteer)
        {
            foreach (DataRow dataRow in data.Rows)
            {
                //create location, use the original name
                Location location = new Location();
                location.Level1 =
                    dataRow[ColumnNames.Level1].ToString();
                location.Level2 =
                    dataRow[ColumnNames.Level2].ToString();
                location.Level3 =
                    dataRow[ColumnNames.Level3].ToString();

                // get codes
                gazetteer.GetLocationCodes(location);

                //add codes
                dataRow[Level1CodeColumnName] = location.Level1Code;
                dataRow[Level2CodeColumnName] = location.Level2Code;
                dataRow[Level3CodeColumnName] = location.Level3Code;
            }
        }

        /// <summary>
        /// Gets the unmatched records.
        /// </summary>
        /// <returns>A view only containing records where one or more location codes is missing.</returns>
        public DataView GetUnmatchedRecords()
        {
            // only show those records where a location code is null
            EnumerableRowCollection<DataRow> query = from record in data.AsEnumerable()
                where record.Field<String>(Level1CodeColumnName) == null ||
                      record.Field<string>(Level2CodeColumnName) == null ||
                      record.Field<string>(Level3CodeColumnName) == null
                select record;

            DataView unmatched = query.AsDataView();
            return unmatched;
        }

        /// <summary>
        /// Provides a list of all the column header names present in the data sheet,
        /// excludes columns added by the application.
        /// </summary>
        /// <returns>List of column names</returns>
        internal IList<string> AllColumnNames()
        {
            var addedColumnNames = AddedColumnNames();
            List<string> list = (
                from DataColumn dataColumn
                    in data.Columns
                where !addedColumnNames.Contains(dataColumn.ColumnName)
                select dataColumn.ColumnName).ToList();

            return list;
        }

        private void AddAdditionalColumns()
        {
            AddCodeColumns();
            AddAltNameColumns();
        }

        private void AddAltNameColumns()
        {
            // add collumns to use for the edited location data
            AddColumn(Alt1ColumnName);
            AddColumn(Alt2ColumnName);
            AddColumn(Alt3ColumnName);
        }

        private void AddCodeColumns()
        {
            AddColumn(Level1CodeColumnName);
            AddColumn(Level2CodeColumnName);
            AddColumn(Level3CodeColumnName);
        }

        private void AddColumn(string columnName)
        {
            if (!data.Columns.Contains(columnName))
            {
                data.Columns.Add(columnName, typeof (String));
            }
        }

        private List<string> AddedColumnNames()
        {
            List<string> addedColumns = new List<string>();
            addedColumns.Add(Level1CodeColumnName);
            addedColumns.Add(Level2CodeColumnName);
            addedColumns.Add(Level3CodeColumnName);
            addedColumns.Add(Alt1ColumnName);
            addedColumns.Add(Alt2ColumnName);
            addedColumns.Add(Alt3ColumnName);

            return addedColumns;
        }

        private void SetColumnsAsReadOnly()
        {
            List<string> addedColumns = AddedColumnNames();

            foreach (DataColumn col in data.Columns)
            {
                if (!addedColumns.Contains(col.ColumnName))
                {
                    col.ReadOnly = true;
                }
            }
        }

        #endregion Methods

        #region Other

        //private void InitialiseDefaultColumnNames()
        //{
        //    InputColumnNames defaultColumnNames = new InputColumnNames();
        //    defaultColumnNames.Level1 = DefaultLevel1ColumnName;
        //    defaultColumnNames.Level2 = DefaultLevel2ColumnName;
        //    defaultColumnNames.Level3 = DefaultLevel3ColumnName;
        //    DefaultColumnNames = defaultColumnNames;
        //}

        #endregion Other
    }
}