// FuzzyResult.cs

namespace GeoLocationTool.Logic
{
    /// <summary>
    /// Holds matched location details
    /// </summary>
    internal class FuzzyMatchResult
    {
        #region Constructors

        public FuzzyMatchResult(string location, double coefficient)
        {
            Location = location;
            Coefficient = coefficient;
        }

        #endregion Constructors

        #region Properties

        public double Coefficient { get; private set; }

        public string DisplayText
        {
            // todo remove after testing
            get { return string.Format("{0}: {1}", Location, Coefficient); }
        }

        public string Location { get; private set; }

        #endregion Properties
    }
}