// IGeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System.Data;
    using Logic;

    public interface IGeoCoder
    {
        #region Properties

        LocationCodes Codes { get; }
        DataTable GazetteerData { get; }
        DataTable InputRecords { get; }

        #endregion Properties

        #region Methods

        void InitialiseLocationColumns();

        ColumnHeaderIndices InputColumnIndices();

        bool IsGazetteerInitialised();

        void LoadGazetter(string path);

        void LoadInputFileCsv(string path);

        void LoadInputFileTabDelim(string path);

        void MatchAll();

        void SaveToCsvFile(string fileName);

        void SetGazetteerColumns(GazetteerColumnHeaders headers);

        void SetOriginalInputColumns(ColumnHeaderIndices indices);

        DataView UnmatchedRecords();

        #endregion Methods
    }
}