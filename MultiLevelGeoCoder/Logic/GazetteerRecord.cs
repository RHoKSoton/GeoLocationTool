// GazetteerRecord.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Holds a record from the Gazetteer data file
    /// </summary>
    internal class GazetteerRecord
    {
        #region Properties

        public string AltName1 { get; set; }

        public string AltName2 { get; set; }

        public string AltName3 { get; set; }

        public string Id1 { get; set; }

        public string Id2 { get; set; }

        public string Id3 { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Name3 { get; set; }

        #endregion Properties
    }
}