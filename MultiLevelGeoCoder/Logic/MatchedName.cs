// MatchedName.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Contains a match between an input location and a 
    /// user selected gazetteer location
    /// </summary>
    internal class MatchedName
    {
        #region Constructors

        public MatchedName(Location inputLocation, Location gazetteerLocation)
        {
            InputLocation = inputLocation;
            GazetteerLocation = gazetteerLocation;
        }

        #endregion Constructors

        #region Properties

        public Location GazetteerLocation { get; set; }

        public Location InputLocation { get; set; }

        #endregion Properties
    }
}