// Gadm.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Holds a record from the Gazetteer data file
    /// </summary>
    internal class Gadm
    {
        public string ID_1 { get; set; }
        public string NAME_1 { get; set; }
        public string ID_2 { get; set; }
        public string NAME_2 { get; set; }
        public string ID_3 { get; set; }
        public string NAME_3 { get; set; }
        public string AltName1 { get; set; }
        public string AltName2 { get; set; }
        public string AltName3 { get; set; }
    }
}