// LocationCodes.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Holds the list of location names/codes to match against
    /// Provides the location codes where there are exact matches
    /// Provides lists of location names for each level 
    /// </summary>
    public class LocationCodes
    {
        #region Fields

        private readonly IEnumerable<Gadm> gazzetteerData;

        #endregion Fields

        #region Constructors

        public LocationCodes(IEnumerable<Gadm> gazzetteerData)
        {
            this.gazzetteerData = gazzetteerData;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the location codes for the given location. Adds them to the given location.
        /// </summary>
        /// <param name="location">The location.</param>
        public Location GetLocationCodes(Location location)
        {
            // todo return a CodedLocation?
            Location location1 = location;
            location1.Level2Code = null;
            location1.Level1Code = null;
            location1.Level3Code = null;

            var level1 = Level1Match(location1);
            if (level1 != null)
            {
                location.Level1Code = level1.ID_1;
                Gadm level2 = Level2Match(location1);
                if (level2 != null)
                {
                    location1.Level2Code = level1.ID_2;
                    Gadm level3 = Level3Match(location1);
                    if (level3 != null)
                    {
                        location1.Level3Code = level3.ID_3;
                    }
                }
            }

            //  todo if not all the codes have been found then try the alternate code list
            // 
            return location1;


        }

        /// <summary>
        /// Lists the Level 1 location names.
        /// </summary>
        /// <returns>List of location names</returns>
        public IList<string> Level1LocationNames()
        {
            var levelList = gazzetteerData.Select(l => l.NAME_1);
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
                            n.NAME_1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase))
                .Select(l => l.NAME_2);

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
                            n.NAME_1,
                            level1Name,
                            StringComparison.OrdinalIgnoreCase) &&
                        String.Equals(
                            n.NAME_2,
                            level2Name,
                            StringComparison.OrdinalIgnoreCase))
                .Select(l => l.NAME_3);
            return levelList.Distinct().OrderBy(i => i).ToList();
        }

        public void RefreshAltCodeList()
        {
            // todo recreate the alt name code list
        }

        private Gadm Level1Match(Location location)
        {
            // just match level 1
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Level1.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            return firstOrDefault;
        }

        private Gadm Level2Match(Location location)
        {
            // must match level 1 and 2
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Level1.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_2,
                        location.Level2.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            return firstOrDefault;
        }

        private Gadm Level3Match(Location location)
        {
            // must match all three levels
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Level1.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_2,
                        location.Level2.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_3,
                        location.Level3.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            return firstOrDefault;
        }

        #endregion Methods
    }
}