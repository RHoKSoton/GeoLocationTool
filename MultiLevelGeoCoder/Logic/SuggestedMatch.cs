// SuggestedMatch.cs

namespace MultiLevelGeoCoder.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using DuoVia.FuzzyStrings;

    /// <summary>
    /// Provides suggested matches using fuzzy matching
    /// </summary>
    public class SuggestedMatch : ISuggestedMatch
    {
        #region Fields

        private readonly GazetteerNames gazetteerNames;

        #endregion Fields

        #region Constructors

        internal SuggestedMatch(GazetteerNames gazetteerNames)
        {
            this.gazetteerNames = gazetteerNames;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The level 1 suggestions for the given location name.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        public List<MatchResult> Level1Suggestions(string level1)
        {
            IList<string> locationList = gazetteerNames.Level1AllLocationNames();
            return Suggestions(level1, locationList);
        }

        /// <summary>
        /// The level 2 suggestions for the given location names.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <param name="level2">The level 2 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        public List<MatchResult> Level2Suggestions(string level1, string level2)
        {
            IList<string> locationList = gazetteerNames.Level2AllLocationNames(level1);
            return Suggestions(level2, locationList);
        }

        /// <summary>
        /// The level 3 suggestions for the given location names.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <param name="level2">The level 2 location name.</param>
        /// <param name="level3">The level 3 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        public List<MatchResult> Level3Suggestions(
            string level1,
            string level2,
            string level3)
        {
            IList<string> locationList = gazetteerNames.Level3AllLocationNames(
                level1,
                level2);
            return Suggestions(level3, locationList);
        }

        private static List<MatchResult> Suggestions(
            string level,
            IEnumerable<string> locationList)
        {
            List<MatchResult> matches = new List<MatchResult>();
            foreach (string location in locationList)
            {
                double coefficient = level.FuzzyMatch(location);
                matches.Add(new MatchResult(location, coefficient));
            }
            return matches.OrderByDescending(p => p.Coefficient).ToList();
        }

        #endregion Methods
    }
}