// InputData.cs

namespace GeoLocationTool.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using DataAccess;

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

        public DataTable dt;

        #endregion Fields

        #region Properties

        public int OriginalLoc1ColumnIndex { get; set; }

        public int OriginalLoc2ColumnIndex { get; set; }

        public int OriginalLoc3ColumnIndex { get; set; }

        #endregion Properties

        #region Methods

        public void AddAdditionalColumns()
        {
            AddCodeColumns();
            AddLocationColumns();
        }

        /// <summary>
        /// Adds the location codes to the input data records.
        /// </summary>
        /// <param name="locationData">The location data.</param>
        public void GetLocationCodes(LocationData locationData)
        {
            foreach (DataRow dataRow in dt.Rows)
            {
                //create location, use the added location columns
                Location location = new Location();
                location.Province =
                    dataRow[Loc1ColumnName].ToString();
                location.Municipality =
                    dataRow[Loc2ColumnName].ToString();
                location.Barangay =
                    dataRow[Loc3ColumnName].ToString();

                // get codes
                locationData.GetLocationCodes(location);

                //add codes
                dataRow[Loc1CodeColumnName] = location.ProvinceCode;
                dataRow[Loc2CodeColumnName] = location.MunicipalityCode;
                dataRow[Loc3CodeColumnName] = location.BarangayCode;
                dataRow.AcceptChanges();
            }
            dt.AcceptChanges();
        }

        /// <summary>
        /// Gets the unmatched records.
        /// </summary>
        /// <returns>A view only containing records where one or more location codes is missing.</returns>
        public DataView GetUnmatchedRecords()
        {
            // only show those records where a location code is null
            EnumerableRowCollection<DataRow> query = from record in dt.AsEnumerable()
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
            CopyColumn(OriginalLoc1ColumnIndex, Loc1ColumnName);
            CopyColumn(OriginalLoc2ColumnIndex, Loc2ColumnName);
            CopyColumn(OriginalLoc3ColumnIndex, Loc3ColumnName);
        }

        /// <summary>
        /// Loads the CSV file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="isFirstRowHeader">if set to <c>true</c> [is first row the header].</param>
        public void LoadCsvFile(string path, bool isFirstRowHeader)
        {
            dt = InputFile.ReadCsvFile(path, isFirstRowHeader);
            AddCodeColumns();
            AddLocationColumns();
            SetColumnsAsReadOnly();
        }

        /// <summary>
        /// Loads the excel file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="worksheetName">Name of the worksheet.</param>
        public void LoadExcelFile(string path, string worksheetName)
        {
            dt = InputFile.ReadExcelFile(path, worksheetName);
            AddCodeColumns();
        }

        /// <summary>
        /// Saves to CSV file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveToCsvFile(string fileName)
        {
            OutputFile.SaveToCsvFile(fileName, dt);
        }

        /// <summary>
        /// Saves to excel file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveToExcelFile(string fileName)
        {
            OutputFile.SaveToExcelFile(fileName, dt);
        }

        private void AddCodeColumns()
        {
            AddColumn(Loc1CodeColumnName);
            AddColumn(Loc2CodeColumnName);
            AddColumn(Loc3CodeColumnName);
        }

        private void AddColumn(string columnName)
        {
            if (!dt.Columns.Contains(columnName))
            {
                dt.Columns.Add(columnName, typeof (String));
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
            foreach (DataRow row in dt.Rows)
            {
                row[targetColumnName] = row[sourceColumnIndex];
            }
        }

        private void SetColumnsAsReadOnly()
        {
            List<string> addedColumns = AddedColumnNames();

            foreach (DataColumn col in dt.Columns)
            {
                if (! addedColumns.Contains(col.ColumnName))
                {
                    col.ReadOnly = true;
                }
            }
        }

        #endregion Methods
    }
}