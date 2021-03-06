﻿// InputData.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Holds the input data and added codes etc
    /// </summary>
    internal class InputData
    {
        #region Fields

        // Use the cache when coding all the rows
        public static bool UseMatchedNamesCache = true; 

        // default column names
        private const string DefaultLevel1ColumnName = "Admin2";
        private const string DefaultLevel2ColumnName = "Admin3";
        private const string DefaultLevel3ColumnName = "Admin4";

        // columns to contain the codes
        private const string Level1CodeColumnName = "Code 1";

        // columns to contain the matched names used to find the codes
        private const string Level1MatchedColumnName = "Name 1";
        private const string Level2CodeColumnName = "Code 2";
        private const string Level2MatchedColumnName = "Name 2";
        private const string Level3CodeColumnName = "Code 3";
        private const string Level3MatchedColumnName = "Name 3";

        #endregion Fields

        #region Constructors

        public InputData(DataTable data)
        {
            Data = data;
            AddAdditionalColumns();
            SetColumnsAsReadOnly();

            // adding empty columns should not  be counted as a data change
            data.AcceptChanges();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the names of the columns that contain the data to be matched.
        /// </summary>
        /// <value>
        /// The column names.
        /// </value>
        public InputColumnHeaders ColumnHeaders { get; private set; }

        /// <summary>
        /// Gets or sets the input data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public DataTable Data { get; set; }

        /// <summary>
        /// Gets the default names of the columns that contain the input data to be matched.
        /// </summary>
        /// <value>
        /// The default column names.
        /// </value>
        public InputColumnHeaders DefaultColumnHeaders
        {
            get
            {
                InputColumnHeaders defaults = new InputColumnHeaders
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
        /// Provides a list of all the column header names present in the data sheet,
        /// excludes columns added by the application.
        /// </summary>
        /// <returns>List of column names</returns>
        public IList<string> AllColumnNames()
        {
            var addedColumnNames = AddedColumnNames();
            List<string> list = (
                from DataColumn dataColumn
                    in Data.Columns
                where !addedColumnNames.Contains(dataColumn.ColumnName)
                select dataColumn.ColumnName).ToList();

            return list;
        }

        /// <summary>
        /// Adds the codes and the names used to find those codes, to the input data.
        /// </summary>
        /// <param name="coder">The location codes.</param>
        public void CodeAll(Coder coder)
        {
            coder.RefreshMatchedNamesCache();
            foreach (DataRow dataRow in Data.Rows)
            {
                CodedLocation codedLocation = FindCodes(
                    coder,
                    dataRow,
                    UseMatchedNamesCache);
                ClearExistingCodes(dataRow);
                AddCodes(codedLocation, dataRow);
            }
        }

        /// <summary>
        /// Codes the column names.
        /// </summary>
        /// <returns></returns>
        public InputColumnHeaders CodeColumnNames()
        {
            InputColumnHeaders columnHeaders = new InputColumnHeaders
            {
                Level1 = Level1CodeColumnName,
                Level2 = Level2CodeColumnName,
                Level3 = Level3CodeColumnName
            };
            return columnHeaders;
        }

        /// <summary>
        /// Gets the uncoded records, i.e. those where at least one code is not present
        /// </summary>
        /// <returns>A view only containing records where one or more codes is missing.</returns>
        public DataView GetUnCodedRecords()
        {
            // only show those records where there is a name but no code
            EnumerableRowCollection<DataRow> query = from record in Data.AsEnumerable()
                where
                    // level 1 has name but no code
                    (!string.IsNullOrEmpty(record.Field<string>(ColumnHeaders.Level1)) &&
                     (string.IsNullOrEmpty(record.Field<string>(Level1CodeColumnName)))) ||
                    // level 2 is in use and has name but no code
                    (!string.IsNullOrEmpty(ColumnHeaders.Level2)) &&
                    (!string.IsNullOrEmpty(record.Field<string>(ColumnHeaders.Level2)) &&
                     (string.IsNullOrEmpty(record.Field<string>(Level2CodeColumnName)))) ||
                    // level 3 is in use and has name but no code
                    (!string.IsNullOrEmpty(ColumnHeaders.Level3)) &&
                    (!string.IsNullOrEmpty(record.Field<string>(ColumnHeaders.Level3)) &&
                     (string.IsNullOrEmpty(record.Field<string>(Level3CodeColumnName))))
                select record;

            DataView unmatched = query.AsDataView();
            return unmatched;
        }

        /// <summary>
        /// The names of the columns that contain the matched names used to find the codes.
        /// </summary>
        /// <returns>The Column Names</returns>
        public InputColumnHeaders MatchColumnNames()
        {
            InputColumnHeaders columnHeaders = new InputColumnHeaders
            {
                Level1 = Level1MatchedColumnName,
                Level2 = Level2MatchedColumnName,
                Level3 = Level3MatchedColumnName
            };
            return columnHeaders;
        }

        public void SetColumnNames(InputColumnHeaders columnHeaders)
        {
            columnHeaders.Validitate();
            ColumnHeaders = columnHeaders;
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

            // add the names used to generate those codes as information for the user.
            AddUsedMatchNames(codedLocation, dataRow);
        }

        private static void AddUsedMatchNames(
            CodedLocation codedLocation,
            DataRow dataRow)
        {
            // add the actual name used to get the code if different to that on the input

            if (codedLocation.IsName1Different())
            {
                dataRow[Level1MatchedColumnName] = codedLocation.GeoCode1.Name;
            }

            if (codedLocation.IsName2Different())
            {
                dataRow[Level2MatchedColumnName] = codedLocation.GeoCode2.Name;
            }

            if (codedLocation.IsName3Different())
            {
                dataRow[Level3MatchedColumnName] = codedLocation.GeoCode3.Name;
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
            AddColumn(Level1MatchedColumnName);
            AddColumn(Level2MatchedColumnName);
            AddColumn(Level3MatchedColumnName);
        }

        private void AddCodeColumns()
        {
            AddColumn(Level1CodeColumnName);
            AddColumn(Level2CodeColumnName);
            AddColumn(Level3CodeColumnName);
        }

        private void AddColumn(string columnName)
        {
            if (!Data.Columns.Contains(columnName))
            {
                Data.Columns.Add(columnName, typeof (String));
            }
        }

        private List<string> AddedColumnNames()
        {
            List<string> addedColumns = new List<string>();
            addedColumns.Add(Level1CodeColumnName);
            addedColumns.Add(Level2CodeColumnName);
            addedColumns.Add(Level3CodeColumnName);
            addedColumns.Add(Level1MatchedColumnName);
            addedColumns.Add(Level2MatchedColumnName);
            addedColumns.Add(Level3MatchedColumnName);

            return addedColumns;
        }

        private void ClearExistingCodes(DataRow dataRow)
        {
            dataRow[Level1CodeColumnName] = null;
            dataRow[Level2CodeColumnName] = null;
            dataRow[Level3CodeColumnName] = null;
            dataRow[Level1MatchedColumnName] = null;
            dataRow[Level2MatchedColumnName] = null;
            dataRow[Level3MatchedColumnName] = null;
        }

        private CodedLocation FindCodes(
            Coder coder,
            DataRow dataRow,
            bool useCache)
        {
            //create location, use the original name
            Location location = new Location();
            location.Name1 =
                dataRow[ColumnHeaders.Level1].ToString();

            // level 2 is optional
            if (!string.IsNullOrEmpty(ColumnHeaders.Level2))
            {
                location.Name2 =
                    dataRow[ColumnHeaders.Level2].ToString();
            }

            // level 3 is optional
            if (!string.IsNullOrEmpty(ColumnHeaders.Level3))
            {
                location.Name3 =
                    dataRow[ColumnHeaders.Level3].ToString();
            }

            // get codes
            CodedLocation codedLocation = coder.GetCodes(location, useCache);
            return codedLocation;
        }

        private void SetColumnsAsReadOnly()
        {
            List<string> addedColumns = AddedColumnNames();

            foreach (DataColumn col in Data.Columns)
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