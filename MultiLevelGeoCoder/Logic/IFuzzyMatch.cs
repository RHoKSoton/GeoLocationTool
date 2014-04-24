// IFuzzyMatch.cs

namespace MultiLevelGeoCoder.Logic
{
    using System.Collections.Generic;

    public interface IFuzzyMatch
    {
        /// <summary>
        /// The level 1 suggestions for the given location name.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        List<MatchResult> Level1Suggestions(string level1);

        /// <summary>
        /// The level 2 suggestions for the given location names.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <param name="level2">The level 2 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        List<MatchResult> Level2Suggestions(string level1, string level2);

        /// <summary>
        /// The level 3 suggestions for the given location names.
        /// </summary>
        /// <param name="level1">The level 1 location name.</param>
        /// <param name="level2">The level 2 location name.</param>
        /// <param name="level3">The level 3 location name.</param>
        /// <returns>List of suggested locations and their coeficient.</returns>
        List<MatchResult> Level3Suggestions(
            string level1,
            string level2,
            string level3);
    }
}