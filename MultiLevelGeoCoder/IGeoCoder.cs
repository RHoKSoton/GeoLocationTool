// IGeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Logic;

    public interface IGeoCoder : IDisposable
    {
        #region Properties

        DataTable GazetteerData { get; }

        DataTable InputData { get; }

        /// <summary>
        /// Gets a value indicating whether a name match has been saved 
        /// since AddAllLocationCodes was last run.
        /// </summary>
        /// <value>
        ///   <c>true</c> if a name match has been saved otherwise, <c>false</c>.
        /// </value>
        bool MatchSaved { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the codes that correspond to the locations in all rows of the input data.
        /// </summary>
        void AddAllLocationCodes();

        /// <summary>
        /// Adds the geo codes for the given location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>Location with codes added where found.</returns>
        CodedLocation AddLocationCodes(Location location);

        /// <summary>
        /// The default names of the columns that contain the gazetteer data to be matched.
        /// </summary>
        /// <returns>The column names</returns>
        GazetteerColumnNames DefaultGazetteerColumnNames();

        /// <summary>
        /// The default names of the columns that contain the input data to be matched.
        /// </summary>
        /// <returns>The column names</returns>
        InputColumnNames DefaultInputColumnNames();

        /// <summary>
        /// Provides suggested name matches using fuzzy matching
        /// </summary>
        /// <returns>Fuzzy Match</returns>
        IFuzzyMatch FuzzyMatch();

        /// <summary>
        /// Provides a list of all the column header names present in the gazetteer data sheet
        /// </summary>
        /// <returns>List of column names</returns>
        IList<string> GazetteerColumnNameList();

        /// <summary>
        /// The saved match for the given level 1 name, if any
        /// </summary>
        /// <param name="level1">The level 1.</param>
        /// <returns>The saved match</returns>
        MatchResult GetSavedMatchLevel1(string level1);

        /// <summary>
        /// The saved match for the given level 2 name, if any
        /// </summary>
        /// <param name="level2">The level 2.</param>
        /// <param name="level1">The level 1.</param>
        /// <returns>The saved match</returns>
        MatchResult GetSavedMatchLevel2(string level2, string level1);

        /// <summary>
        /// The saved match for the given level 3 name, if any
        /// </summary>
        /// <param name="level2">The level 2.</param>
        /// <param name="level1">The level 1.</param>
        /// <param name="level3">The level 3.</param>
        /// <returns>The saved match</returns>
        MatchResult GetSavedMatchLevel3(
            string level3,
            string level1,
            string level2);

        /// <summary>
        /// Provides a list of all the column header names present in the input data sheet
        /// </summary>
        /// <returns>List of column names</returns>
        IList<string> InputColumnNameList();

        /// <summary>
        /// The names of the columns that contain the input location names to be matched
        /// </summary>
        /// <returns></returns>
        InputColumnNames InputLocationColumnNames();

        /// <summary>
        /// Determines whether the gazetteer is initialised.
        /// </summary>
        /// <returns>Returns true if the gazetteer data has been loaded.</returns>
        bool IsGazetteerInitialised();

        /// <summary>
        /// List of available Level 1 location names from the gazetteer.
        /// </summary>
        /// <returns>List of location names</returns>
        IList<string> Level1LocationNames();

        /// <summary>
        /// List of available Level 2 location names from the gazetteer for the given level 1.
        /// </summary>
        /// <returns>List of location names</returns>
        IList<string> Level2LocationNames(string level1);

        /// <summary>
        /// List of available Level 3 location names from the gazetteer for the given level 1 and 2.
        /// </summary>
        /// <returns>List of location names</returns>
        IList<string> Level3LocationNames(string level1, string level2);

        /// <summary>
        /// Loads the gazetteer file.
        /// </summary>
        /// <param name="path">The path.</param>
        void LoadGazetteerFile(string path);

        /// <summary>
        /// Loads the input CSV file .
        /// </summary>
        /// <param name="path">The path.</param>
        void LoadInputFileCsv(string path);

        /// <summary>
        /// Loads the input tab delimited file.
        /// </summary>
        /// <param name="path">The path.</param>
        void LoadInputFileTabDelim(string path);

        /// <summary>
        /// The names of the columns that contain the location codes.
        /// </summary>
        /// <returns>The column names</returns>
        InputColumnNames LocationCodeColumnNames();

        /// <summary>
        /// The names of the columns that contain the matched names used to find the codes.
        /// </summary>
        /// <returns></returns>
        InputColumnNames MatchColumnNames();

        /// <summary>
        /// Saves the match.
        /// </summary>
        /// <param name="inputLocation">The input location.</param>
        /// <param name="gazetteerLocation">The gazetteer location.</param>
        void SaveMatch(Location inputLocation, Location gazetteerLocation);

        /// <summary>
        /// Saves the output file.
        /// </summary>
        /// <param name="outputFilePath">Path of the output file.</param>
        void SaveOutputFile(string outputFilePath);

        /// <summary>
        /// Sets the gazetteer columns that hold the data to provide the codes
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        void SetGazetteerColumns(
            GazetteerColumnNames columnNames,
            bool saveSelection = true);

        //todo remove the  version with the save selection parameter from the public api as this is only for the tests
        /// <summary>
        /// Sets the column names that hold the input data to be coded.
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        void SetInputColumns(InputColumnNames columnNames);

        /// <summary>
        /// Filters the records to only those that have not had all their codes added.
        /// </summary>
        /// <returns>The records without codes</returns>
        DataView UncodedRecords();

        #endregion Methods
    }
}