// MatchedName.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;

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
            // keep a copy
            OriginalInput = new Location(
                inputLocation.Name1,
                inputLocation.Name2,
                inputLocation.Name3);

            GazetteerLocation = gazetteerLocation;
            // keep a copy
            OriginalGazetteer = new Location(
                gazetteerLocation.Name1,
                gazetteerLocation.Name2,
                gazetteerLocation.Name3);
        }

        #endregion Constructors

        #region Properties

        public Location GazetteerLocation { get; set; }

        public Location InputLocation { get; set; }

        public Location OriginalGazetteer { get; private set; }

        public Location OriginalInput { get; private set; }

        #endregion Properties

        #region Methods

        public bool Level1NotSame()
        {
            return NotSame(InputLocation.Name1, GazetteerLocation.Name1);
        }

        public bool Level2NotSame()
        {
            return NotSame(InputLocation.Name2, GazetteerLocation.Name2);
        }

        public bool Level3NotSame()
        {
            return NotSame(InputLocation.Name3, GazetteerLocation.Name3);
        }

        private static bool NotSame(string inputName, string gazetteerName)
        {
            bool hasValues = (!string.IsNullOrEmpty(inputName)) &&
                             !string.IsNullOrEmpty(gazetteerName);
            return hasValues &&
                   !string.Equals(
                       inputName,
                       gazetteerName,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Methods
    }
}