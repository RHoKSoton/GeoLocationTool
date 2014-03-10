// Location.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Simple structure holding location names
    /// </summary>
    public class Location
    {
        #region Constructors

        public Location(string name1, string name2 = "", string name3 = "")
        {
            Name1 = name1;
            Name2 = name2;
            Name3 = name3;
        }

        public Location()
        {
        }

        #endregion Constructors

        #region Properties

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Name3 { get; set; }

        #endregion Properties
    }
}