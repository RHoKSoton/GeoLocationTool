// Location.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// DTO holding location text and codes
    /// </summary>
    public class Location
    {
        #region Constructors

        public Location(string province, string barangay, string municipality)
        {
            Province = province;
            Barangay = barangay;
            Municipality = municipality;
        }

        public Location()
        {

        }

        #endregion Constructors

        #region Properties

        public string Barangay { get; set; }

        public string BarangayCode { get; set; }

        public string Municipality { get; set; }

        public string MunicipalityCode { get; set; }

        public string Province { get; set; }

        public string ProvinceCode { get; set; }

        #endregion Properties

        //todo remove the codes?
    }
}