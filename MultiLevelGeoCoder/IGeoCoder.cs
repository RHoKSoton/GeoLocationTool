// IGeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System.Collections.Generic;
    using System.Data;
    using Logic;

    public interface IGeoCoder
    {
        #region Properties

        DataTable GazetteerData { get; }

        DataTable InputData { get; }

        string OutputFileName { get; set; }

        #endregion Properties

        #region Methods

        IList<string> AllGazetteerColumnNames();

        /// <summary>
        /// Provides a list of all the column header names present in the input data sheet
        /// </summary>
        /// <returns>List of column names</returns>
        IList<string> AllInputColumnNames();

        /// <summary>
        /// Codes all rows of the input data.
        /// </summary>
        void CodeAll();

        /// <summary>
        /// The names of the columns that contain the codes.
        /// </summary>
        /// <returns>The column names</returns>
        InputColumnNames CodeColumnNames();

        GazetteerColumnNames DefaultGazetteerColumnNames();

        /// <summary>
        /// The default names of the columns that contain the input data to be matched.
        /// </summary>
        /// <returns>The Column Names</returns>
        InputColumnNames DefaultInputColumnNames();

        /// <summary>
        /// Provides suggested name matches using fuzzy matching
        /// </summary>
        /// <returns>Fuzzy Match</returns>
        FuzzyMatch FuzzyMatch();

        /// <summary>
        /// Gets the geo codes for the given location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>Location with codes added where found.</returns>
        CodedLocation GetCodes(Location location);

        /// <summary>
        /// The names of the columns that contain the data to be matched
        /// </summary>
        /// <returns></returns>
        InputColumnNames InputColumnNames();

        bool IsGazetteerInitialised();

        IList<string> Level1LocationNames();

        IList<string> Level2LocationNames(string level1);

        IList<string> Level3LocationNames(string level1, string level2);

        void LoadGazetter(string path);

        void LoadInputFileCsv(string path);

        void LoadInputFileTabDelim(string path);

        /// <summary>
        /// The names of the columns that contain the matched names used to find the codes.
        /// </summary>
        /// <returns></returns>
        InputColumnNames MatchColumnNames();


        /// <summary>
        /// The saved match for the given level 1 name, if any
        /// </summary>
        /// <param name="level1">The level 1.</param>
        /// <returns>The saved match</returns>
        IEnumerable<MatchResult> GetSavedMatchLevel1(string level1);

        /// <summary>
        /// The saved match for the given level 2 name, if any
        /// </summary>
        /// <param name="level2">The level 2.</param>
        /// <param name="level1">The level 1.</param>
        /// <returns>The saved match</returns>
        IEnumerable<MatchResult> GetSavedMatchLevel2(string level2, string level1);

        /// <summary>
        /// The saved match for the given level 3 name, if any
        /// </summary>
        /// <param name="level2">The level 2.</param>
        /// <param name="level1">The level 1.</param>
        /// <param name="level3">The level 3.</param>
        /// <returns>The saved match</returns>
        IEnumerable<MatchResult> GetSavedMatchLevel3(
            string level3,
            string level1,
            string level2);

        /// <summary>
        /// Saves the matched level 1 name that corresponds to the given alternate name.
        /// </summary>
        /// <param name="alternateLevel1">The alternate level 1 name.</param>
        /// <param name="gazetteerLevel1">The gazetteer level 1 name.</param>
        void SaveMatchLevel1(string alternateLevel1, string gazetteerLevel1);

        /// <summary>
        /// Saves the match level 2 name that corresponds to the given alternate name.
        /// </summary>
        /// <param name="alternateLevel2">The alternate level 2 name.</param>
        /// <param name="gazetteerLevel1">The gazetteer level 1 name.</param>
        /// <param name="gazetteerLevel2">The gazetteer level 2 name.</param>
        void SaveMatchLevel2(
            string alternateLevel2,
            string gazetteerLevel1,
            string gazetteerLevel2);

        /// <summary>
        /// Saves the level 3 name that corresponds to the given alternate name.
        /// </summary>
        /// <param name="alternateLevel3">The alternate level 3 name.</param>
        /// <param name="gazetteerLevel1">The gazetteer level 1 name.</param>
        /// <param name="gazetteerLevel2">The gazetteer level 2 name.</param>
        /// <param name="gazetteerLevel3">The gazetteer level 3 name.</param>
        void SaveMatchLevel3(
            string alternateLevel3,
            string gazetteerLevel1,
            string gazetteerLevel2,
            string gazetteerLevel3);

        void SaveOutputFile();

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

        DataView UncodedRecords();

        #endregion Methods
    }
}