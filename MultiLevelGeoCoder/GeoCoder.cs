// GeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using DataAccess;
    using Logic;
    using Model;

    /// <summary>
    /// Service Gateway, all access should be through this class
    /// </summary>
    public class GeoCoder : IGeoCoder
    {
        #region Fields

        private readonly IColumnsMappingProvider columnsMappingProvider;
        private readonly MatchedNames matchedNames;

        private GazetteerData gazetteerData;
        private string gazetteerFileName;
        private InputData inputData;
        private LocationCodes locationCodes;
        private LocationNames locationNames;
        private IMatchProvider matchProvider;

        #endregion Fields

        #region Constructors

        public GeoCoder(DbConnection dbConnection)
        {
            //todo make a seperate class responsible for the connection and its closing, not the geoCoder?
            matchProvider = new MatchProvider(dbConnection);
            columnsMappingProvider = new ColumnsMappingProvider(dbConnection);
            matchedNames = new MatchedNames(matchProvider);
        }

        #endregion Constructors

        #region Properties

        public DataTable GazetteerData
        {
            get { return gazetteerData.Data; }
        }

        public DataTable InputData
        {
            get { return inputData != null ? inputData.Data : null; }
        }

        public string OutputFileName { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the codes that correspond to the locations in all rows of the input data.
        /// </summary>
        public void AddAllLocationCodes()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            inputData.CodeAll(locationCodes);
            Debug.WriteLine("CodeAll: " + watch.Elapsed.TotalSeconds);
        }

        /// <summary>
        /// Adds the geo codes for the given location
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>Location with codes added where found.</returns>
        public CodedLocation AddLocationCodes(Location location)
        {
            return locationCodes.GetCodes(location);
        }

        /// <summary>
        /// The default names of the columns that contain the gazetteer data to be matched.
        /// </summary>
        /// <returns>
        /// The column names
        /// </returns>
        public GazetteerColumnNames DefaultGazetteerColumnNames()
        {
            GazetteerColumnNames columnNames = new GazetteerColumnNames();
            GazetteerColumnsMapping columnsMapping =
                columnsMappingProvider.GetGazetteerColumnsMapping(gazetteerFileName);
            if (columnsMapping != null)
            {
                columnNames.Level1Code = columnsMapping.Level1Code;
                columnNames.Level2Code = columnsMapping.Level2Code;
                columnNames.Level3Code = columnsMapping.Level3Code;
                columnNames.Level1Name = columnsMapping.Level1Name;
                columnNames.Level2Name = columnsMapping.Level2Name;
                columnNames.Level3Name = columnsMapping.Level3Name;
                columnNames.Level1AltName = columnsMapping.Level1AltName;
                columnNames.Level2AltName = columnsMapping.Level2AltName;
                columnNames.Level3AltName = columnsMapping.Level3AltName;
            }

            return columnNames;
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
        public FuzzyMatch FuzzyMatch()
        {
            return new FuzzyMatch(locationNames);
        }

        /// <summary>
        /// Provides a list of all the column header names present in the gazetteer data sheet
        /// </summary>
        /// <returns>
        /// List of column names
        /// </returns>
        public IList<string> GazetteerColumnNameList()
        {
            return gazetteerData.AllColumnNames();
        }

        /// <summary>
        /// The saved match for the given level 1 name, if any
        /// </summary>
        /// <param name="level1">The level 1.</param>
        /// <returns>
        /// The saved match
        /// </returns>
        public IEnumerable<MatchResult> GetSavedMatchLevel1(string level1)
        {
            return matchedNames.GetSavedMatchLevel1(level1);
        }

        /// <summary>
        /// The saved match for the given level 2 name, if any
        /// </summary>
        /// <param name="level2">The level 2.</param>
        /// <param name="level1">The level 1.</param>
        /// <returns>
        /// The saved match
        /// </returns>
        public IEnumerable<MatchResult> GetSavedMatchLevel2(
            string level2,
            string level1)
        {
            return matchedNames.GetSavedMatchLevel2(level2, level1);
        }

        /// <summary>
        /// The saved match for the given level 3 name, if any
        /// </summary>
        /// <param name="level3">The level 3.</param>
        /// <param name="level1">The level 1.</param>
        /// <param name="level2">The level 2.</param>
        /// <returns>
        /// The saved match
        /// </returns>
        public IEnumerable<MatchResult> GetSavedMatchLevel3(
            string level3,
            string level1,
            string level2)
        {
            return matchedNames.GetSavedMatchLevel3(
                level3,
                level1,
                level2);
        }

        /// <summary>
        /// Provides a list of all the column header names present in the input data sheet
        /// </summary>
        /// <returns>List of column names</returns>
        public IList<string> InputColumnNameList()
        {
            return inputData.AllColumnNames();
        }

        /// <summary>
        /// The names of the columns that contain the data to be matched.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames InputLocationColumnNames()
        {
            return inputData.ColumnNames;
        }

        /// <summary>
        /// Determines whether the gazetteer is initialised.
        /// </summary>
        /// <returns>
        /// Returns true if the gazetteer data has been loaded.
        /// </returns>
        public bool IsGazetteerInitialised()
        {
            return gazetteerData != null;
        }

        /// <summary>
        /// List of available Level 1 location names from the gazetteer.
        /// </summary>
        /// <returns>
        /// List of location names
        /// </returns>
        public IList<string> Level1LocationNames()
        {
            return locationNames.Level1MainLocationNames();
        }

        /// <summary>
        /// List of available Level 2 location names from the gazetteer for the given level 1.
        /// </summary>
        /// <param name="level1"></param>
        /// <returns>
        /// List of location names
        /// </returns>
        public IList<string> Level2LocationNames(string level1)
        {
            return locationNames.Level2MainLocationNames(level1);
        }

        /// <summary>
        /// List of available Level 3 location names from the gazetteer for the given level 1 and 2.
        /// </summary>
        /// <param name="level1"></param>
        /// <param name="level2"></param>
        /// <returns>
        /// List of location names
        /// </returns>
        public IList<string> Level3LocationNames(string level1, string level2)
        {
            return locationNames.Level3MainLocationNames(level1, level2);
        }

        /// <summary>
        /// Loads the gazetteer file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadGazetteerFile(string path)
        {
            const bool isFirstRowHeader = true;
            // todo remove as first row is always header
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            gazetteerData = new GazetteerData(dt);
            gazetteerFileName = path;
        }

        /// <summary>
        /// Loads the input CSV file .
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadInputFileCsv(string path)
        {
            const bool isFirstRowHeader = true;
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            inputData = new InputData(dt);
        }

        /// <summary>
        /// Loads the input tab delimited file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadInputFileTabDelim(string path)
        {
            const bool isFirstRowHeader = true;
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader, "\t");
            inputData = new InputData(dt);
        }

        /// <summary>
        /// The names of the columns that contain the location codes.
        /// </summary>
        /// <returns>
        /// The column names
        /// </returns>
        public InputColumnNames LocationCodeColumnNames()
        {
            return inputData.CodeColumnNames();
        }

        /// <summary>
        /// The names of the columns that contain the matched names used to find the codes.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames MatchColumnNames()
        {
            return inputData.MatchColumnNames();
        }

        /// <summary>
        /// Saves the match.
        /// </summary>
        /// <param name="inputLocation">The input location.</param>
        /// <param name="gazetteerLocation">The gazetteer location.</param>
        public void SaveMatch(Location inputLocation, Location gazetteerLocation)
        {
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);
        }

        /// <summary>
        /// Saves the output file.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Output file not saved, file name required.</exception>
        public void SaveOutputFile()
        {
            if (string.IsNullOrEmpty(OutputFileName))
            {
                throw new InvalidOperationException(
                    "Output file not saved, file name required.");
            }
            FileExport.SaveToCsvFile(OutputFileName, InputData);
            inputData.Data.AcceptChanges();
        }

        /// <summary>
        /// Sets the gazetteer columns that hold the data to provide the codes
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        public void SetGazetteerColumns(
            GazetteerColumnNames columnNames,
            bool saveSelection = true)
        {
            gazetteerData.ColumnNames = columnNames;
            locationCodes = new LocationCodes(gazetteerData.LocationList, matchProvider);
            locationNames = new LocationNames(gazetteerData.LocationList);

            if (saveSelection)
                SaveUserSelection(columnNames, gazetteerFileName);
        }

        /// <summary>
        /// Sets the column names that hold the input data to be coded.
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        public void SetInputColumns(InputColumnNames columnNames)
        {
            inputData.SetColumnNames(columnNames);
        }

        /// <summary>
        /// Filters the records to only those that have not had all their codes added.
        /// </summary>
        /// <returns>
        /// The records without codes
        /// </returns>
        public DataView UncodedRecords()
        {
            return inputData.GetUnCodedRecords();
        }

        // use this to inject data in for unit testing
        internal void SetGazetteerData(DataTable dt)
        {
            gazetteerData = new GazetteerData(dt);
        }

        // use this to inject data in for unit testing
        internal void SetInputData(DataTable dt)
        {
            inputData = new InputData(dt);
        }

        // use this to inject data in for unit testing
        internal void SetMatchProvider(IMatchProvider provider)
        {
            matchProvider = provider;
        }

        private void SaveUserSelection(GazetteerColumnNames columnNames, string filename)
        {
            // todo review the code duplication between GazetteerColumnNames and GazetteerColumnsMapping2 classes
            columnsMappingProvider.SaveGazetteerColumnsMapping(
                new GazetteerColumnsMapping
                {
                    FileName = filename,
                    Level1Code = columnNames.Level1Code,
                    Level1Name = columnNames.Level1Name,
                    Level1AltName = columnNames.Level1AltName,
                    Level2Code = columnNames.Level2Code,
                    Level2Name = columnNames.Level2Name,
                    Level2AltName = columnNames.Level2AltName,
                    Level3Code = columnNames.Level3Code,
                    Level3Name = columnNames.Level3Name,
                    Level3AltName = columnNames.Level3AltName,
                }
                );
        }

        #endregion Methods
    }
}