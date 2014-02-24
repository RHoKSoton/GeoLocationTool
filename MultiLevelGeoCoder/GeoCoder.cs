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
    public class GeoCoder
    {
        #region Fields

        private InputData inputData;

        #endregion Fields

        #region Constructors

        public GeoCoder(LocationData gazetteer)
        {
            Gazetteer = gazetteer;
        }

        #endregion Constructors

        #region Properties

        public LocationData Gazetteer { get; private set; }

        public DataTable InputDataTable
        {
            get { return inputData.data; }
        }

        #endregion Properties

        #region Methods

        public static GazetteerFile GetGazetteerFile(string path)
        {
            InputFile inputFile = new InputFile();
            DataTable dt = inputFile.ReadCsvFile(path);
            return new GazetteerFile(dt);
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
            InputFile inputFile = new InputFile();
            DataTable dt = inputFile.ReadCsvFile(path);
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
            inputData.AddMatchedLocationCodes(Gazetteer);
        }

        public void SaveToCsvFile(string fileName)
        {
            OutputFile.SaveToCsvFile(fileName, InputDataTable);
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