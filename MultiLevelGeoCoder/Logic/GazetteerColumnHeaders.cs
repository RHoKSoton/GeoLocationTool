// GazetteerColumnNames.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Simple class containing the selected column names of the gazetteer data.
    /// </summary>
    public class GazetteerColumnHeaders
    {
        #region Properties

        public string Level1AltName { get; set; }

        public string Level1Code { get; set; }

        public string Level1Name { get; set; }

        public string Level2AltName { get; set; }

        public string Level2Code { get; set; }

        public string Level2Name { get; set; }

        public string Level3AltName { get; set; }

        public string Level3Code { get; set; }

        public string Level3Name { get; set; }

        #endregion Properties
    }
}