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

        ColumnHeaderIndices InputColumnIndices();

        bool IsGazetteerInitialised();

        void LoadGazetter(string path);

        void LoadInputFileCsv(string path);

        void LoadInputFileTabDelim(string path);

        void MatchAll();

        void SaveNearMatch();

        void SaveToCsvFile(string fileName);

        void SetGazetteerColumns(GazetteerColumnHeaders headers);

        void SetInputColumns(ColumnHeaderIndices indices);

        DataView UnmatchedRecords();

        #endregion Methods       
    }
}