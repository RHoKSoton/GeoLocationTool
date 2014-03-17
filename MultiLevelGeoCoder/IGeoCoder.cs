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
        /// <returns>Fuzzy Matcher</returns>
        FuzzyMatch FuzzyMatcher();

        /// <summary>
        /// Gets the geo codes for the given location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>Location with codes added where found.</returns>
        CodedLocation GetGeoCodes(Location location);

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

        void SaveNearMatch();

        void SaveOutputFile();

        /// <summary>
        /// Sets the gazetteer columns that hold the data to provide the codes
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        void SetGazetteerColumns(GazetteerColumnNames columnNames);

        /// <summary>
        /// Sets the column names that hold the input data to be coded.
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        void SetInputColumns(InputColumnNames columnNames);

        DataView UncodedRecords();

        /// <summary>
        /// The names of the columns that contain the matched names used to find the codes.
        /// </summary>
        /// <returns></returns>
        InputColumnNames MatchColumnNames();

        #endregion Methods
    }
}