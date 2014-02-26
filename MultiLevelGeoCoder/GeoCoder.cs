// GeoCoder.cs

namespace MultiLevelGeoCoder
{
    using System;
    using System.Data;
    using DataAccess;
    using Logic;

    /// <summary>
    /// Service Gateway, all access should be through this class
    /// </summary>
    public class GeoCoder : IGeoCoder
    {
        #region Fields

        private GazetteerData gazetteer;
        private InputData inputData;

        #endregion Fields

        #region Properties

        public LocationCodes Codes { get; private set; }

        public DataTable GazetteerData
        {
            get { return gazetteer.Data; }
        }

        public DataTable InputRecords
        {
            get { return inputData != null ? inputData.data : null; }
        }

        #endregion Properties

        #region Methods

        public void InitialiseLocationColumns()
        {
            inputData.InitialiseLocationColumns();
        }

        public ColumnHeaderIndices InputColumnIndices()
        {
            return inputData.HeaderIndices;
        }

        public bool IsGazetteerInitialised()
        {
            return gazetteer != null;
        }

        public void LoadGazetter(string path)
        {
            const bool isFirstRowHeader = true;
            // todo remove as first row is always header
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            gazetteer = new GazetteerData(dt);
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

        public void MatchAll()
        {
            inputData.AddMatchedLocationCodes(Codes);
        }

        public void SaveToCsvFile(string fileName)
        {
            FileExport.SaveToCsvFile(fileName, InputRecords);
        }

        public void SetGazetteerColumns(GazetteerColumnHeaders headers)
        {
            gazetteer.SetColumnHeaders(headers);
            Codes = new LocationCodes(gazetteer.LocationList);
        }

        public void SetOriginalInputColumns(ColumnHeaderIndices indices)
        {
            inputData.HeaderIndices = indices;
        }

        public DataView UnmatchedRecords()
        {
            return inputData.GetUnmatchedRecords();
        }

        #endregion Methods
    }
}