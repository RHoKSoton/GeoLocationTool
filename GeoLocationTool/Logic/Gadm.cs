// Gadm.cs

namespace GeoLocationTool.Logic
{
    /// <summary>
    /// Holds a record from the Global Administrative Areas data file
    /// </summary>
    public class Gadm
    {
        // todo change the datatypes to mirror the data if safe to do so
        public string PID { get; set; }
        public string ID_0 { get; set; }
        public string ISO { get; set; }
        public string NAME_0 { get; set; }
        public string ID_1 { get; set; }
        public string NAME_1 { get; set; }
        public string ID_2 { get; set; }
        public string NAME_2 { get; set; }
        public string ID_3 { get; set; }
        public string NAME_3 { get; set; }
        public string NL_NAME_3 { get; set; }
        public string VARNAME_3 { get; set; }
        public string TYPE_3 { get; set; }
        public string ENGTYPE_3 { get; set; }
    }
}