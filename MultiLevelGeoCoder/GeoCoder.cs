// GeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using DataAccess;
    using Logic;

    /// <summary>
    /// Service Gateway, all access should be through this class
    /// </summary>
    public class GeoCoder : IGeoCoder
    {
        #region Fields

        private readonly INearMatchesProvider nearMatchesProvider;

        private GazetteerData gazetteer;
        private InputData inputData;
        private LocationCodes locationCodes;
        private LocationNames locationNames;

        #endregion Fields

        #region Constructors

        public GeoCoder(DbConnection dbConnection)
        {
            //todo make the geocoder responsible for the connection and its closing, not the caller?
            nearMatchesProvider = new NearMatchesProvider(dbConnection);
        }

        #endregion Constructors

        #region Properties

        public DataTable GazetteerData
        {
            get { return gazetteer.Data; }
        }

        public DataTable InputRecords
        {
            get { return inputData != null ? inputData.data : null; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Provides a list of all the column header names present in the input data sheet
        /// </summary>
        /// <returns>List of column names</returns>
        public IList<string> AllInputColumnNames()
        {
            return inputData.AllColumnNames();
        }

        /// <summary>
        /// The default names of the columns that contain the input data to be matched.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames DefaultInputColumnNames()
        {
            return inputData.DefaultColumnNames;
        }

        /// <summary>
        /// Provides suggested name matches using fuzzy matching
        /// </summary>
        /// <returns>Fuzzy Matcher</returns>
        public FuzzyMatch FuzzyMatcher()
        {
            return new FuzzyMatch(locationNames);
        }

        /// <summary>
        /// Gets the geo codes for the given location
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>Location with codes added where found.</returns>
        public CodedLocation GetGeoCodes(Location location)
        {
            return locationCodes.GetLocationCodes(location);
        }

        /// <summary>
        /// The names of the columns that contain the data to be matched.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames InputColumnNames()
        {
            return inputData.ColumnNames;
        }

        public bool IsGazetteerInitialised()
        {
            return gazetteer != null;
        }

        public IList<string> Level1LocationNames()
        {
            return locationNames.Level1LocationNames();
        }

        public IList<string> Level2LocationNames(string level1)
        {
            return locationNames.Level2LocationNames(level1);
        }

        public IList<string> Level3LocationNames(string level1, string level2)
        {
            return locationNames.Level3LocationNames(level1, level2);
        }

        public void LoadGazetter(string path)
        {
            const bool isFirstRowHeader = true;
            // todo remove as first row is always header
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            gazetteer = new GazetteerData(dt);
        }

        public void LoadInputFileCsv(string path)
        {
            const bool isFirstRowHeader = true;
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            inputData = new InputData(dt);
        }

        public void LoadInputFileTabDelim(string path)
        {
            const bool isFirstRowHeader = true;
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader, "\t");
            inputData = new InputData(dt);
        }

        public void MatchAll()
        {
            inputData.AddMatchedLocationCodes(locationCodes);
        }

        public void SaveNearMatch()
        {
            throw new NotImplementedException();
            // Save to the database
            // refresh the code list
            // GeoCodes.RefreshAltCodeList();
        }

        public void SaveToCsvFile(string fileName)
        {
            FileExport.SaveToCsvFile(fileName, InputRecords);
        }

        public void SetGazetteerColumns(GazetteerColumnHeaders headers)
        {
            gazetteer.SetColumnHeaders(headers);
            locationCodes = new LocationCodes(gazetteer.LocationList, nearMatchesProvider);
            locationNames = new LocationNames(gazetteer.LocationList);
        }

        /// <summary>
        /// Sets the column names that hold the input data to be matched.
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        public void SetInputColumns(InputColumnNames columnNames)
        {
            inputData.ColumnNames = columnNames;
        }

        public DataView UnmatchedRecords()
        {
            return inputData.GetUnmatchedRecords();
        }

        #endregion Methods
    }
}