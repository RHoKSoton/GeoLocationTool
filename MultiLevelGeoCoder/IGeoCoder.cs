// IGeoCoder.cs

namespace MultiLevelGeoCoder
{
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