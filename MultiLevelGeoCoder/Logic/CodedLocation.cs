// CodedLocation.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;

    /// <summary>
    /// Holds the codes and a copy of the input location.
    /// </summary>
    public class CodedLocation
    {
        #region Fields

        private readonly Location inputLocation;

        #endregion Fields

        #region Constructors

        public CodedLocation(Location location)
        {
            if (location == null) throw new ArgumentNullException("location");

            //  keep a copy of the input location for comparison
            inputLocation = new Location(location.Name1, location.Name2, location.Name3);

            // initialise the names
            Name1 = string.Copy(inputLocation.Name1);
            Name2 = string.Copy(inputLocation.Name2);
            ;
            Name3 = string.Copy(inputLocation.Name3);
        }

        #endregion Constructors

        #region Properties

        public GeoCode GeoCode1 { get; set; }

        public GeoCode GeoCode2 { get; set; }

        public GeoCode GeoCode3 { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Name3 { get; set; }

        #endregion Properties

        #region Methods

        public bool IsName1Different()
        {
            if (GeoCode1 == null)
            {
                return false;
            }
            return
                !string.Equals(
                    inputLocation.Name1,
                    GeoCode1.Name,
                    StringComparison.OrdinalIgnoreCase);
        }

        public bool IsName2Different()
        {
            if (GeoCode2 == null)
            {
                return false;
            }
            return
                !string.Equals(
                    inputLocation.Name2,
                    GeoCode2.Name,
                    StringComparison.OrdinalIgnoreCase);
        }

        public bool IsName3Different()
        {
            if (GeoCode3 == null)
            {
                return false;
            }
            return
                !string.Equals(
                    inputLocation.Name3,
                    GeoCode3.Name,
                    StringComparison.OrdinalIgnoreCase);
        }

        #endregion Methods
    }
}