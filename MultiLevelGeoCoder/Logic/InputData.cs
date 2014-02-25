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

        public const string Loc1CodeColumnName = "Code 1";
        public const string Loc1ColumnName = "Location 1";
        public const string Loc2CodeColumnName = "Code 2";
        public const string Loc2ColumnName = "Location 2";
        public const string Loc3CodeColumnName = "Code 3";
        public const string Loc3ColumnName = "Location 3";

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

        //public int OriginalLoc1ColumnIndex { get; set; }
        //public int OriginalLoc2ColumnIndex { get; set; }
        //public int OriginalLoc3ColumnIndex { get; set; }
        public ColumnHeaderIndices HeaderIndices { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the matched location codes.
        /// </summary>
        /// <param name="gazetteer">The gazetteer.</param>
        public void AddMatchedLocationCodes(LocationMatcher gazetteer)
        {
            foreach (DataRow dataRow in data.Rows)
            {
                //create location, use the added location columns
                Location location = new Location();
                location.Level1 =
                    dataRow[Loc1ColumnName].ToString();
                location.Level2 =
                    dataRow[Loc2ColumnName].ToString();
                location.Level3 =
                    dataRow[Loc3ColumnName].ToString();

                // get codes
                gazetteer.GetLocationCodes(location);

                //add codes
                dataRow[Loc1CodeColumnName] = location.Level1Code;
                dataRow[Loc2CodeColumnName] = location.Level2Code;
                dataRow[Loc3CodeColumnName] = location.Level3Code;
                dataRow.AcceptChanges();
            }
            data.AcceptChanges();
        }

        /// <summary>
        /// Gets the unmatched records.
        /// </summary>
        /// <returns>A view only containing records where one or more location codes is missing.</returns>
        public DataView GetUnmatchedRecords()
        {
            // only show those records where a location code is null
            EnumerableRowCollection<DataRow> query = from record in data.AsEnumerable()
                where record.Field<String>(Loc1CodeColumnName) == null ||
                      record.Field<string>(Loc2CodeColumnName) == null ||
                      record.Field<string>(Loc3CodeColumnName) == null
                select record;

            DataView unmatched = query.AsDataView();
            return unmatched;
        }

        /// <summary>
        /// Initialises the location columns, copying the contents of the 
        /// original location columns to the added location columns.
        /// </summary>
        public void InitialiseLocationColumns()
        {
            CopyColumn(HeaderIndices.Admin1, Loc1ColumnName);
            CopyColumn(HeaderIndices.Admin2, Loc2ColumnName);
            CopyColumn(HeaderIndices.Admin3, Loc3ColumnName);
        }

        private void AddAdditionalColumns()
        {
            AddCodeColumns();
            AddLocationColumns();
        }

        private void AddCodeColumns()
        {
            AddColumn(Loc1CodeColumnName);
            AddColumn(Loc2CodeColumnName);
            AddColumn(Loc3CodeColumnName);
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
            addedColumns.Add(Loc1CodeColumnName);
            addedColumns.Add(Loc2CodeColumnName);
            addedColumns.Add(Loc3CodeColumnName);
            addedColumns.Add(Loc1ColumnName);
            addedColumns.Add(Loc2ColumnName);
            addedColumns.Add(Loc3ColumnName);

            return addedColumns;
        }

        private void AddLocationColumns()
        {
            // add collumns to use for the edited location data
            AddColumn(Loc1ColumnName);
            AddColumn(Loc2ColumnName);
            AddColumn(Loc3ColumnName);
        }

        private void CopyColumn(int sourceColumnIndex, string targetColumnName)
        {
            foreach (DataRow row in data.Rows)
            {
                row[targetColumnName] = row[sourceColumnIndex];
            }
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