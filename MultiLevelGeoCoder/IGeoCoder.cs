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

        LocationCodes GeoCodes { get; }

        DataTable InputRecords { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Provides a list of all the column header names present in the input data sheet
        /// </summary>
        /// <returns>List of column names</returns>
        IList<string> AllInputColumnNames();

        /// <summary>
        /// The names of the columns that contain the data to be matched
        /// </summary>
        /// <returns></returns>
        InputColumnNames InputColumnNames();

        /// <summary>
        /// The default names of the columns that contain the  input data to be matched.
        /// </summary>
        /// <returns></returns>
        InputColumnNames DefaultInputColumnNames();

        bool IsGazetteerInitialised();

        void LoadGazetter(string path);

        void LoadInputFileCsv(string path);

        void LoadInputFileTabDelim(string path);

        void MatchAll();

        void SaveNearMatch();

        void SaveToCsvFile(string fileName);

        void SetGazetteerColumns(GazetteerColumnHeaders headers);

        /// <summary>
        /// Sets the column names that hold the input data to be matched.
        /// </summary>
        /// <param name="columnNames">The column names.</param>
        void SetInputColumns(InputColumnNames columnNames);

        DataView UnmatchedRecords();

        #endregion Methods            
    }
}