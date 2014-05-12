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

        private readonly List<GazetteerRecord> gazzetteerData;

        #endregion Fields

        #region Constructors

        public LocationNames(List<GazetteerRecord> locationList)
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

        public string GetMainLevel1(string level1)
        {
            string name = IsLevel1MainName(level1)
                ? level1
                : Level1MainName(level1);
            return name;
        }

        public string GetMainLevel2(string level1, string level2)
        {
            string name = IsLevel2MainName(level1, level2)
                ? level2
                : Level2MainName(level1, level2);
            return name;
        }

        public string GetMainLevel3(string level1, string level2, string level3)
        {
            string name = IsLevel3MainName(level1, level2, level3)
                ? level3
                : Level3MainName(level1, level2, level3);
            return name;
        }

        public bool IsInLevel2Names(
            string inputName2,
            string gazetteerName1)
        {
            if (String.IsNullOrEmpty(inputName2))
            {
                return false;
            }

            // check gazetteer names list
            return Level2AllLocationNames(gazetteerName1)
                .Contains(
                    inputName2,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        public bool IsInLevel3Names(
            string inputName3,
            string gazetteerName1,
            string gazetteerName2)
        {
            if (String.IsNullOrEmpty(inputName3))
            {
                return false;
            }

            // check  gazetteer names list
            return Level3AllLocationNames(gazetteerName1, gazetteerName2)
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
            List<string> locationNames = new List<string>();
            // use the level 1 main name in the search
            string name = IsLevel1MainName(level1Name)
                ? level1Name
                : Level1MainName(level1Name);
            if (!string.IsNullOrEmpty(name))
            {
                locationNames = Level2MainLocationNames(name);
                locationNames.AddRange(Level2AltLocationNames(name));
            }

            return locationNames.Distinct().OrderBy(i => i).ToList();
        }

        public List<string> Level2MainLocationNames(string level1Name)
        {
            // all level 2 names where the given name is in level 1 names
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
            //use the main names in the search, not the alt names
            string level1 = IsLevel1MainName(level1Name)
                ? level1Name
                : Level1MainName(level1Name);
            string level2 = IsLevel2MainName(level1, level2Name)
                ? level2Name
                : Level2MainName(level1, level2Name);

            List<string> locationNames = Level3MainLocationNames(level1, level2);
            locationNames.AddRange(Level3AltLocationNames(level1, level2));
            return locationNames.Distinct().OrderBy(i => i).ToList();
        }

        public List<string> Level3MainLocationNames(string level1Name, string level2Name)
        {
            // all level 3 names where the given level 1 name is in level 1 name and
            // the given level 2 name is in level 2 name
            var levelList = gazzetteerData
                .Where(
                    n =>
                        (String.Equals(
                            n.Name1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase)) &&
                        (String.Equals(
                            n.Name2,
                            level2Name,
                            StringComparison.OrdinalIgnoreCase)))
                .Select(l => l.Name3);
            var locationNames = levelList.Distinct().OrderBy(i => i).ToList();
            return locationNames;
        }

        private bool IsLevel1MainName(string level1Name)
        {
            if (String.IsNullOrEmpty(level1Name))
            {
                return false;
            }

            // check gazetteer names list
            return Level1MainLocationNames()
                .Contains(
                    level1Name,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        private bool IsLevel2MainName(string level1Name, string level2Name)
        {
            if (String.IsNullOrEmpty(level1Name) || String.IsNullOrEmpty(level2Name))
            {
                return false;
            }

            // check gazetteer names list
            return Level2MainLocationNames(level1Name)
                .Contains(
                    level2Name,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        private bool IsLevel3MainName(
            string level1Name,
            string level2Name,
            string level3Name)
        {
            if (String.IsNullOrEmpty(level1Name) ||
                String.IsNullOrEmpty(level2Name) ||
                String.IsNullOrEmpty(level3Name))
            {
                return false;
            }

            // check gazetteer names list
            return Level3MainLocationNames(level1Name, level2Name)
                .Contains(
                    level3Name,
                    StringComparer.InvariantCultureIgnoreCase);
        }

        private IEnumerable<string> Level1AltLocationNames()
        {
            var levelList =
                gazzetteerData.Where(x => !String.IsNullOrEmpty(x.AltName1))
                    .Select(l => l.AltName1);
            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        private string Level1MainName(string level1AltName)
        {
            // the main name that corresponds to the given alt name
            return gazzetteerData.Where(
                x => String.Compare(
                    level1AltName,
                    x.AltName1,
                    StringComparison.OrdinalIgnoreCase) == 0)
                .Select(l => l.Name1).FirstOrDefault();
        }

        private IEnumerable<string> Level2AltLocationNames(string level1Name)
        {
            // all alt level 2 names where the given name is in level 1 name
            var levelList = gazzetteerData
                .Where(
                    n =>
                        String.Equals(
                            n.Name1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase))
                .Where(x => !string.IsNullOrEmpty(x.AltName2)).Select(l => l.AltName2);

            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        private string Level2MainName(string level1Name, string level2AltName)
        {
            // the main name that corresponds to the given alt name
            return
                gazzetteerData.Where(
                    x =>
                        (String.Compare(
                            level1Name,
                            x.Name1,
                            StringComparison.OrdinalIgnoreCase) == 0) &&
                        (String.Compare(
                            level2AltName,
                            x.AltName2,
                            StringComparison.OrdinalIgnoreCase) == 0))
                    .Select(l => l.Name2).FirstOrDefault();
        }

        private IEnumerable<string> Level3AltLocationNames(
            string level1Name,
            string level2Name)
        {
            // all level alt 3 names where the given level 1 name is in level 1 name column and
            // the given level 2 name is in level 2 name column
            var levelList = gazzetteerData
                .Where(
                    n =>
                        (String.Equals(
                            n.Name1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase)) &&
                        (String.Equals(
                            n.Name2,
                            level2Name,
                            StringComparison.OrdinalIgnoreCase)))
                .Where(x => !string.IsNullOrEmpty(x.AltName3)).Select(l => l.AltName3);
            var locationNames = levelList.Distinct().OrderBy(i => i).ToList();
            return locationNames;
        }

        private string Level3MainName(
            string level1Name,
            string level2Name,
            string level3AltName)
        {
            // the main name that corresponds to the given alt name
            return
                gazzetteerData.Where(
                    x =>
                        (String.Compare(
                            level1Name,
                            x.Name1,
                            StringComparison.OrdinalIgnoreCase) == 0) &&
                        (String.Compare(
                            level2Name,
                            x.Name2,
                            StringComparison.OrdinalIgnoreCase) == 0) &&
                        (String.Compare(
                            level3AltName,
                            x.AltName3,
                            StringComparison.OrdinalIgnoreCase) == 0))
                    .Select(l => l.Name3).FirstOrDefault();
        }

        #endregion Methods
    }
}