// GeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
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
            inputData.CodeAll(locationCodes);
        }

        public InputColumnNames CodeColumnNames()
        {
            return inputData.CodeColumnNames();
        }

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
        /// Gets the geo codes for the given location
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>Location with codes added where found.</returns>
        public CodedLocation GetCodes(Location location)
        {
            return locationCodes.GetCodes(location);
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
            //todo change return type to single result

            List<MatchResult> result = new List<MatchResult>();
            Level1Match match = matchProvider.GetMatches(level1).FirstOrDefault();
            if (match != null)
            {
                result.Add(new MatchResult(match.Level1, match.Weight));
            }
            return result;
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
            // todo change return type to single result

            List<MatchResult> result = new List<MatchResult>();
            Level2Match match = matchProvider.GetMatches(level2, level1).FirstOrDefault();
            if (match != null)
            {
                result.Add(new MatchResult(match.Level2, match.Weight));
            }
            return result;
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
            // todo change return type to single result

            List<MatchResult> result = new List<MatchResult>();
            Level3Match match =
                matchProvider.GetMatches(level3, level1, level2).FirstOrDefault();
            if (match != null)
            {
                result.Add(new MatchResult(match.Level3, match.Weight));
            }
            return result;
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
            gazetteerFileName = path;
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

        /// <summary>
        /// The names of the columns that contain the matched names used to find the codes.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames MatchColumnNames()
        {
            return inputData.MatchColumnNames();
        }

        /// <summary>
        /// Saves the matched level 1 name that corresponds to the given alternate name.
        /// </summary>
        /// <param name="alternateLevel1">The alternate level 1 name.</param>
        /// <param name="gazetteerLevel1">The gazetteer level 1 name.</param>
        public void SaveMatchLevel1(string alternateLevel1, string gazetteerLevel1)
        {
            matchProvider.SaveMatchLevel1(alternateLevel1, gazetteerLevel1);
        }

        public void SaveMatchLevel2(
            string alternateLevel2,
            string gazetteerLevel1,
            string gazetteerLevel2)
        {
            matchProvider.SaveMatchLevel2(
                alternateLevel2,
                gazetteerLevel1,
                gazetteerLevel2);
        }

        /// <summary>
        /// Saves the level 3 name that corresponds to the given alternate name.
        /// </summary>
        /// <param name="alternateLevel3">The alternate level 3 name.</param>
        /// <param name="gazetteerLevel1">The gazetteer level 1 name.</param>
        /// <param name="gazetteerLevel2">The gazetteer level 2 name.</param>
        /// <param name="gazetteerLevel3">The gazetteer level 3 name.</param>
        public void SaveMatchLevel3(
            string alternateLevel3,
            string gazetteerLevel1,
            string gazetteerLevel2,
            string gazetteerLevel3)
        {
            matchProvider.SaveMatchLevel3(
                alternateLevel3,
                gazetteerLevel1,
                gazetteerLevel2,
                gazetteerLevel3);
        }

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

        public DataView UncodedRecords()
        {
            return inputData.GetUnCodedRecords();
        }

        internal void SetGazetteerData(DataTable dt)
        {
            gazetteerData = new GazetteerData(dt);
        }

        internal void SetInputData(DataTable dt)
        {
            inputData = new InputData(dt);
        }

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