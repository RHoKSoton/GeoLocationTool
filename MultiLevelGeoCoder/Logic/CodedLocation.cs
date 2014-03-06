// CodedLocation.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;

    /// <summary>
    /// Holds the codes and a copy of the original location.
    /// </summary>
    public class CodedLocation
    {
        #region Fields

        private readonly Location location;

        #endregion Fields

        #region Constructors

        public CodedLocation(Location location)
        {
            this.location = new Location(
                location.Name1,
                location.Name2,
                location.Name3);
        }

        #endregion Constructors

        #region Properties

        public GeoCode GeoCode1 { get; set; }

        public GeoCode GeoCode2 { get; set; }

        public GeoCode GeoCode3 { get; set; }

        public string Name1
        {
            get { return location.Name1.Trim(); }
        }

        public string Name2
        {
            get { return location.Name2.Trim(); }
        }

        public string Name3
        {
            get { return location.Name3.Trim(); }
        }

        #endregion Properties

        #region Methods

        public bool IsName1Different()
        {
            if (GeoCode1 == null)
            {
                return false;
            }
            return
                !string.Equals(Name1, GeoCode1.Name, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsName2Different()
        {
            if (GeoCode2 == null)
            {
                return false;
            }
            return
                !string.Equals(Name2, GeoCode2.Name, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsName3Different()
        {
            if (GeoCode3 == null)
            {
                return false;
            }
            return
                !string.Equals(Name3, GeoCode3.Name, StringComparison.OrdinalIgnoreCase);
        }

        #endregion Methods
    }
}