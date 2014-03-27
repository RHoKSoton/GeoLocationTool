// GeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
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
        private IMatchProvider matchProvider;

        private GazetteerData gazetteerData;
        private string gazetteerFileName;
        private InputData inputData;
        private LocationCodes locationCodes;
        private LocationNames locationNames;

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
            gazetteerFileName = path;
        }

        internal void SetGazetteerData(DataTable dt)
        {
            gazetteerData = new GazetteerData(dt);
        }

        public void LoadInputFileCsv(string path)
        {
            const bool isFirstRowHeader = true;
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            inputData = new InputData(dt);
        }

        internal void SetInputData(DataTable dt)
        {
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
        public void SetGazetteerColumns(GazetteerColumnNames columnNames, bool saveSelection = true)
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
            inputData.ColumnNames = columnNames;
        }

        public DataView UncodedRecords()
        {
            return inputData.GetUnCodedRecords();
        }

        /// <summary>
        /// The names of the columns that contain the matched names used to find the codes.
        /// </summary>
        /// <returns></returns>
        public InputColumnNames MatchColumnNames()
        {
            return inputData.MatchColumnNames();
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

        internal void SetMatchProvider(IMatchProvider provider)
        {
            matchProvider = provider;
        }
    }
}