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

        private InputData inputData;
        private GazetteerData gazetteerData;

        #endregion Fields

        #region Constructors

        public GeoCoder(GazetteerData gazetteerData)
        {
           GazetteerData = gazetteerData;
        }

        #endregion Constructors

        #region Properties

        public LocationCodes Codes { get; private set; }

        public DataTable InputRecords
        {
            get
            {
                return inputData!= null ? inputData.data : null;
            }
        }

        public GazetteerData GazetteerData
        {
            get
            {
                return gazetteerData;
            }
            set
            {
                gazetteerData = value;
                Codes = new LocationCodes(value.LocationList);
                // todo should the location Codes just be recreated as needed from the gazetteer data?
            }
        }

        #endregion Properties

        #region Methods

        public static GazetteerData GetGazetteer(string path)
        {
            const bool isFirstRowHeader = true;
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            return new GazetteerData(dt);
        }

        public ColumnHeaderIndices InputColumnIndices()
        {
            return inputData.HeaderIndices;
        }

        public void InitialiseLocationColumns()
        {
            inputData.InitialiseLocationColumns();
        }

        public void LoadInputFileCsv(string path)
        {
            const bool isFirstRowHeader = true;
            DataTable dt = FileImport.ReadCsvFile(path, isFirstRowHeader);
            inputData = new InputData(dt);
        }

        public void LoadInputFileTabDelim(string path)
        {
            throw new NotImplementedException();
            //DataFileReader dataFileReader = new DataFileReader();
            //dataFileReader.ReadTabDelimFile(path);
        }

        public void MatchAll()
        {
            inputData.AddMatchedLocationCodes(Codes);
        }

        public void SaveToCsvFile(string fileName)
        {
            FileExport.SaveToCsvFile(fileName, InputRecords);
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