// Location.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// DTO holding location names and codes
    /// </summary>
    public class Location
    {
        //todo remove the codes to a seperate class?
        #region Constructors

        public Location(string level1, string level2 = "", string level3 = "")
        {
            Level1 = level1;
            Level2 = level2;
            Level3 = level3;
        }

        public Location()
        {

        }
      
        #endregion Constructors

        #region Properties

        public string Level3 { get; set; }

        public string Level3Code { get; set; }

        public string Level2 { get; set; }

        public string Level2Code { get; set; }

        public string Level1 { get; set; }

        public string Level1Code { get; set; }

        #endregion Properties

     
    }
}