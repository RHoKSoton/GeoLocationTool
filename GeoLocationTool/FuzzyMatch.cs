// FuzzyMatch.cs

namespace GeoLocationTool
{
    using System.Collections.Generic;
    using System.Linq;
    using DuoVia.FuzzyStrings;
    // using FuzzyString;

    /// <summary>
    /// Provides probable matches using fuzzy matching - in progress
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

        public List<FuzzyResult> GetBarangaySuggestions(string input)
        {
            // todo remove method duplication
            List<FuzzyResult> matches = new List<FuzzyResult>();
            //List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            //// Choose which algorithms should weigh in for the comparison
            //options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            //options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            //options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);

            //// Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
            //const FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Strong;

            foreach (string location in geoLocationData.Level3List())
            {
                // Get strong matches
                //if (source.ApproximatelyEquals(target, options, tolerance))
                //{
                //    matches.Add(source);
                //}

                bool isEqual = input.FuzzyEquals(location);
                double coefficient = input.FuzzyMatch(location);
                if (isEqual)
                {
                    matches.Add(new FuzzyResult(location, coefficient));
                }
            }
            return matches.OrderByDescending(p => p.Coefficient).ToList();
        }

        public List<FuzzyResult> GetMunicipalitySuggestions(string input)
        {
            // todo remove method duplication
            List<FuzzyResult> matches = new List<FuzzyResult>();
            //List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            //// Choose which algorithms should weigh in for the comparison
            //options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            //options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            //options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);

            //// Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
            //const FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Strong;

            foreach (string location in geoLocationData.Level2List())
            {
                // Get strong matches
                //if (source.ApproximatelyEquals(target, options, tolerance))
                //{
                //    matches.Add(source);
                //}

                bool isEqual = input.FuzzyEquals(location);
                double coefficient = input.FuzzyMatch(location);
                //if (string.Equals(location,"boac",StringComparison.OrdinalIgnoreCase))Debugger.Break();
                //if (isEqual)
                //{
                matches.Add(new FuzzyResult(location, coefficient));
                //}
            }
            return matches.OrderByDescending(p => p.Coefficient).ToList();
        }

        public List<FuzzyResult> GetProvinceSuggestions(string input)
        {
            List<FuzzyResult> matches = new List<FuzzyResult>();

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

            foreach (string location in geoLocationData.Level1List())
            {
                bool isEqual = input.FuzzyEquals(location);
                double coefficient = input.FuzzyMatch(location);
                if (isEqual)
                {
                    matches.Add(new FuzzyResult(location, coefficient));
                }
            }
            return matches.OrderByDescending(p => p.Coefficient).ToList();
        }

        #endregion Methods
    }
}