// GeoCode.cs

namespace MultiLevelGeoCoder.Logic
{
    /// <summary>
    /// Simple structure to hold a name and its code
    /// </summary>
    public class GeoCode
    {
        #region Constructors

        public GeoCode(string code, string name)
        {
            Code = code;
            Name = name;
        }

        #endregion Constructors

        #region Properties

        public string Code { get; private set; }

        public string Name { get; private set; }

        #endregion Properties
    }
}