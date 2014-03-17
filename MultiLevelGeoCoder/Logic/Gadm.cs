// Gadm.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Holds a record from the Gazetteer data file
    /// </summary>
    internal class Gadm
    {
        // todo can we combine this with the location class?
        public string ID_1 { get; set; }
        public string NAME_1 { get; set; }
        public string ID_2 { get; set; }
        public string NAME_2 { get; set; }
        public string ID_3 { get; set; }
        public string NAME_3 { get; set; }
        public string VARNAME_3 { get; set; }
    }
}