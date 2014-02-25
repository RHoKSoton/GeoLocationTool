// IGeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System.Data;
    using Logic;

    public interface IGeoCoder
    {
        #region Properties

        DataTable InputRecords { get; }

        LocationMatcher Matcher { get; }

        #endregion Properties

        #region Methods

        void LoadInputFileCsv(string path);

        void LoadInputFileTabDelim(string path);

        void MatchAll();

        void SaveToCsvFile(string fileName);

        DataView UnmatchedRecords();

        #endregion Methods
    }
}