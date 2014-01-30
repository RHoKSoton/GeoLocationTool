// Location.cs

namespace GeoLocationTool
{
    /// <summary>
    /// DTO holding location text and codes
    /// </summary>
    public class Location
    {
        #region Properties

        public string Baracay { get; set; }

        public string BaracayCode { get; set; }

        public string Municipality { get; set; }

        public string MunicipalityCode { get; set; }

        public string Province { get; set; }

        public string ProvinceCode { get; set; }

        #endregion Properties
    }
}