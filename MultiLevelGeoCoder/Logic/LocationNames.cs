﻿// LocationNames.cs

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

        public static bool IsInLevel1Names(string inputName1, LocationNames locationNames)
        {
            if (String.IsNullOrEmpty(inputName1))
            {
                return false;
            }

            // check gazetteer names list
            return locationNames.Level1AllLocationNames()
                .Contains(
                    inputName1,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        public static bool IsInLevel2Names(
            string inputName2,
            string gazetteerName1,
            LocationNames locationNames)
        {
            if (String.IsNullOrEmpty(inputName2))
            {
                return false;
            }

            // check gazetteer names list
            return locationNames.Level2AllLocationNames(gazetteerName1)
                .Contains(
                    inputName2,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        public static bool IsInLevel3Names(
            string inputName3,
            string gazetteerName1,
            string gazetteerName2,
            LocationNames locationNames)
        {
            if (String.IsNullOrEmpty(inputName3))
            {
                return false;
            }

            // check  gazetteer names list
            return locationNames.Level3AllLocationNames(gazetteerName1, gazetteerName2)
                .Contains(
                    inputName3,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Lists the Level 1 location names.
        /// </summary>
        /// <returns>List of location names</returns>
        public IList<string> Level1AllLocationNames()
        {
            var locationNames = Level1MainLocationNames();
            locationNames.AddRange(Level1AltLocationNames());
            return locationNames.Distinct().OrderBy(i => i).ToList();
        }

        public List<string> Level1MainLocationNames()
        {
            var levelList = gazzetteerData.Select(l => l.Name1);
            var locationNames = levelList.Distinct().OrderBy(i => i).ToList();
            return locationNames;
        }

        /// <summary>
        /// Lists the Level 2 location names for the given level 1.
        /// </summary>
        /// <param name="level1Name">Name of the level 1 location.</param>
        /// <returns>List of location names.</returns>
        public IList<string> Level2AllLocationNames(string level1Name)
        {
            List<string> locationNames = Level2MainLocationNames(level1Name);
            locationNames.AddRange(Level2AltLocationNames(level1Name));
            return locationNames.Distinct().OrderBy(i => i).ToList();
        }

        public List<string> Level2MainLocationNames(string level1Name)
        {
            var levelList = gazzetteerData
                .Where(
                    n =>
                        String.Equals(
                            n.Name1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase))
                .Select(l => l.Name2);

            var locationNames = levelList.Distinct().OrderBy(i => i).ToList();
            return locationNames;
        }

        /// <summary>
        /// Lists the Level 3 location names for the given level 1 and 2.
        /// </summary>
        /// <param name="level1Name">Name of the level 1 location.</param>
        /// <param name="level2Name">Name of the level 2 location.</param>
        /// <returns>List of location names.</returns>
        public IList<string> Level3AllLocationNames(string level1Name, string level2Name)
        {
            var locationNames = Level3MainLocationNames(level1Name, level2Name);
            locationNames.AddRange(Level3AltLocationNames(level1Name, level2Name));
            return locationNames.Distinct().OrderBy(i => i).ToList();
        }

        public List<string> Level3MainLocationNames(string level1Name, string level2Name)
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
            var locationNames = levelList.Distinct().OrderBy(i => i).ToList();
            return locationNames;
        }

        private IEnumerable<string> Level1AltLocationNames()
        {
            var levelList = gazzetteerData.Select(l => l.AltName1);
            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        private IEnumerable<string> Level2AltLocationNames(string level1Name)
        {
            var levelList = gazzetteerData
                .Where(
                    n =>
                        String.Equals(
                            n.Name1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase))
                .Select(l => l.AltName2);

            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        private IEnumerable<string> Level3AltLocationNames(
            string level1Name,
            string level2Name)
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
                .Select(l => l.AltName3);
            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        #endregion Methods
    }
}