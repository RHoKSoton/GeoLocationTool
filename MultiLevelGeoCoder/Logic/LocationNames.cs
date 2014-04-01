// LocationNames.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides lists of gazetteer location names for each level 
    /// </summary>
    internal class LocationNames
    {
        #region Fields

        private readonly List<Gadm> gazzetteerData;

        #endregion Fields

        #region Constructors

        public LocationNames(List<Gadm> locationList)
        {
            gazzetteerData = locationList;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Lists the Level 1 location names.
        /// </summary>
        /// <returns>List of location names</returns>
        public IList<string> Level1LocationNames()
        {
            var levelList = gazzetteerData.Select(l => l.Name1);
            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        /// <summary>
        /// Lists the Level 2 location names for the given level 1.
        /// </summary>
        /// <param name="level1Name">Name of the level 1 location.</param>
        /// <returns>List of location names.</returns>
        public IList<string> Level2LocationNames(string level1Name)
        {
            var levelList = gazzetteerData
                .Where(
                    n =>
                        String.Equals(
                            n.Name1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase))
                .Select(l => l.Name2);

            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        /// <summary>
        /// Lists the Level 3 location names for the given level 1 and 2.
        /// </summary>
        /// <param name="level1Name">Name of the level 1 location.</param>
        /// <param name="level2Name">Name of the level 2 location.</param>
        /// <returns>List of location names.</returns>
        public IList<string> Level3LocationNames(string level1Name, string level2Name)
        {
            var levelList = gazzetteerData
                .Where(
                    n =>
                        String.Equals(
                            n.Name1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase) &&
                        String.Equals(
                            n.Name2,
                            level2Name,
                            StringComparison.OrdinalIgnoreCase))
                .Select(l => l.Name3);
            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        #endregion Methods
    }
}