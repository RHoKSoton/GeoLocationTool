// InputData.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Holds the input data and added codes
    /// </summary>
    public class InputData
    {
        #region Fields

        public const string Level1CodeColumnName = "Code 1";
        public const string Alt1ColumnName = "Alt Name 1";
        public const string Level2CodeColumnName = "Code 2";
        public const string Alt2ColumnName = "Alt Name 2";
        public const string Level3CodeColumnName = "Code 3";
        public const string Alt3ColumnName = "Alt Name 3";

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

        public DataTable data { get; set; }
        public ColumnHeaderIndices HeaderIndices { get; set; }

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
                    dataRow[HeaderIndices.Admin1].ToString();
                location.Level2 =
                    dataRow[HeaderIndices.Admin2].ToString();
                location.Level3 =
                    dataRow[HeaderIndices.Admin3].ToString();

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

        private void AddAdditionalColumns()
        {
            AddCodeColumns();
            AddAltNameColumns();
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

        private void AddAltNameColumns()
        {
            // add collumns to use for the edited location data
            AddColumn(Alt1ColumnName);
            AddColumn(Alt2ColumnName);
            AddColumn(Alt3ColumnName);
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
    }
}