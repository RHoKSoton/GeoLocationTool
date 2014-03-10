// InputData.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Holds the input data and added codes etc
    /// </summary>
    public class InputData
    {
        #region Fields

        public const string Level1CodeColumnName = "Code 1";
        public const string Level2CodeColumnName = "Code 2";
        public const string Level3CodeColumnName = "Code 3";
        public const string Used1ColumnName = "Name 1";
        public const string Used2ColumnName = "Name 2";
        public const string Used3ColumnName = "Name 3";

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
        /// Gets or sets the names of the columns that contain the data to be matched.
        /// </summary>
        /// <value>
        /// The column names.
        /// </value>
        public InputColumnNames ColumnNames { get; set; }

        /// <summary>
        /// Gets or sets the input data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public DataTable data { get; set; }

        /// <summary>
        /// Gets the default names of the columns that contain the input data to be matched.
        /// </summary>
        /// <value>
        /// The default column names.
        /// </value>
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
        /// Adds the location codes and the names used to find those codes,to the input data.
        /// </summary>
        /// <param name="locationCodes">The location codes.</param>
        public void AddLocationCodes(LocationCodes locationCodes)
        {
            foreach (DataRow dataRow in data.Rows)
            {
                CodedLocation codedLocation = GetCodes(locationCodes, dataRow);
                AddCodes(codedLocation, dataRow);
                AddUsedNames(codedLocation, dataRow);
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

        private static void AddCodes(CodedLocation codedLocation, DataRow dataRow)
        {
            if (codedLocation.GeoCode1 != null)
            {
                dataRow[Level1CodeColumnName] = codedLocation.GeoCode1.Code;
            }

            if (codedLocation.GeoCode2 != null)
            {
                dataRow[Level2CodeColumnName] = codedLocation.GeoCode2.Code;
            }

            if (codedLocation.GeoCode3 != null)
            {
                dataRow[Level3CodeColumnName] = codedLocation.GeoCode3.Code;
            }
        }

        private static void AddUsedNames(CodedLocation codedLocation, DataRow dataRow)
        {
            // add the actual name used to get the code if different to that on the input

            if (codedLocation.IsName1Different())
            {
                dataRow[Used1ColumnName] = codedLocation.GeoCode1.Name;
            }

            if (codedLocation.IsName2Different())
            {
                dataRow[Used2ColumnName] = codedLocation.GeoCode2.Name;
            }

            if (codedLocation.IsName3Different())
            {
                dataRow[Used3ColumnName] = codedLocation.GeoCode3.Name;
            }
        }

        private void AddAdditionalColumns()
        {
            AddCodeColumns();
            AddAltNameColumns();
        }

        private void AddAltNameColumns()
        {
            // add collumns to use for the edited location data
            AddColumn(Used1ColumnName);
            AddColumn(Used2ColumnName);
            AddColumn(Used3ColumnName);
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
            addedColumns.Add(Used1ColumnName);
            addedColumns.Add(Used2ColumnName);
            addedColumns.Add(Used3ColumnName);

            return addedColumns;
        }

        private CodedLocation GetCodes(LocationCodes gazetteer, DataRow dataRow)
        {
            //create location, use the original name
            Location location = new Location();
            location.Name1 =
                dataRow[ColumnNames.Level1].ToString();
            location.Name2 =
                dataRow[ColumnNames.Level2].ToString();
            location.Name3 =
                dataRow[ColumnNames.Level3].ToString();

            // get codes
            CodedLocation codedLocation = gazetteer.GetCodes(location);
            return codedLocation;
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