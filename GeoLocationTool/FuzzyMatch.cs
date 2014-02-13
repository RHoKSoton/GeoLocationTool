// FuzzyMatch.cs

namespace GeoLocationTool
{
    using System.Collections.Generic;
    using System.Linq;
    using DuoVia.FuzzyStrings;

    // using FuzzyString;

    /// <summary>
    /// Provides suggested matches using fuzzy matching
    /// </summary>
    internal class FuzzyMatch
    {
        #region Fields

        private readonly GeoLocationData geoLocationData;

        #endregion Fields

        #region Constructors

        public FuzzyMatch(GeoLocationData geoLocationData)
        {
            this.geoLocationData = geoLocationData;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the level 1 suggestions for the given location name.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        public List<FuzzyResult> GetLevel1Suggestions(string level1)
        {
            IList<string> locationList = geoLocationData.Level1LocationNames();
            return Suggestions(level1, locationList);          
        }

        /// <summary>
        /// Gets the level 2 suggestions for the given location names.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <param name="level2">The level 2 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        public List<FuzzyResult> GetLevel2Suggestions(string level1, string level2)
        {
            IList<string> locationList = geoLocationData.Level2LocationNames(level1);
            return Suggestions(level2, locationList);
        }

        /// <summary>
        /// Gets the level 3 suggestions for the given location names.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <param name="level2">The level 2 location name.</param>
        /// <param name="level3">The level 3 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        public List<FuzzyResult> GetLevel3Suggestions(
            string level1,
            string level2,
            string level3)
        {
            IList<string> locationList = geoLocationData.Level3LocationNames(
                level1,
                level2);
            return Suggestions(level3, locationList);
        }

        private static List<FuzzyResult> Suggestions(
            string level,
            IEnumerable<string> locationList)
        {
            List<FuzzyResult> matches = new List<FuzzyResult>();
            foreach (string location in locationList)
            {
                double coefficient = level.FuzzyMatch(location);
                    matches.Add(new FuzzyResult(location, coefficient));
            }
            return matches.OrderByDescending(p => p.Coefficient).ToList();
        }

        #endregion Methods

        #region Other

        // this is the code for the other posible fuzzy match library, 'FuzzyString', it
        // can be removed if we stick with the current library.
        //List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();
        // // Choose which algorithms should weigh in for the comparison
        //options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
        //options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
        //options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
        // // Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
        //const FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Strong;
        //foreach (string location in geoLocationData.Level1List())
        //{
        //    var coeficient = location.LevenshteinDistance(input);
        //    matches.Add(new FuzzyResult(location,coeficient));
        //    // Get strong matches
        //    //if (location.ApproximatelyEquals(input, options, tolerance))
        //    //{
        //    //    matches.Add(location);
        //    //}
        //}

        #endregion Other
    }
}