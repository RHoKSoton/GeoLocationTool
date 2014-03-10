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

        private readonly IMatchProvider matchProvider;

        private GazetteerData gazetteerData;
        private InputData inputData;
        private LocationCodes locationCodes;
        private LocationNames locationNames;

        #endregion Fields

        #region Constructors

        public GeoCoder(DbConnection dbConnection)
        {
            //todo make the geocoder responsible for the connection and its closing, not the caller?
            matchProvider = new MatchProvider(dbConnection);
        }

        #endregion Constructors

        #region Properties

        public DataTable GazetteerData
        {
            get { return gazetteerData.Data; }
        }

        public DataTable InputRecords
        {
            get { return inputData != null ? inputData.data : null; }
        }

        #endregion Properties

        #region Methods

        public IList<string> AllGazetteerColumnNames()
        {
            return gazetteerData.AllColumnNames();
        }

        /// <summary>
        /// Provides a list of all the column header names present in the input data sheet
        /// </summary>
        /// <returns>List of column names</returns>
        public IList<string> AllInputColumnNames()
        {
            return inputData.AllColumnNames();
        }

        public void CodeAll()
        {
            inputData.AddLocationCodes(locationCodes);
        }

        public GazetteerColumnNames DefaultGazetteerColumnNames()
        {
            //todo get the saved gazetteer column names from the database
            throw new NotImplementedException();
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
            return locationCodes.GetCodes(location);
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
            return gazetteerData != null;
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
            gazetteerData = new GazetteerData(dt);
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

        public void SetGazetteerColumns(GazetteerColumnNames columnNames)
        {
            gazetteerData.ColumnNames = columnNames;
            locationCodes = new LocationCodes(gazetteerData.LocationList, matchProvider);
            locationNames = new LocationNames(gazetteerData.LocationList);
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
            return inputData.GetCodedRecords();
        }

        #endregion Methods
    }
}