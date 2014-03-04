// LocationCodes.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using Model;

    /// <summary>
    /// Holds the list of location names/codes to match against
    /// Provides the location codes where there are exact matches
    /// Provides lists of location names for each level 
    /// </summary>
    public class LocationCodes
    {
        #region Fields

        private readonly IEnumerable<Gadm> gazzetteerData;
        private readonly INearMatchesProvider nearMatchesProvider;

        #endregion Fields

        #region Constructors

        public LocationCodes(
            IEnumerable<Gadm> gazzetteerData,
            INearMatchesProvider nearMatchesProvider)
        {
            this.gazzetteerData = gazzetteerData;
            this.nearMatchesProvider = nearMatchesProvider;
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
            // todo do we need to keep the original names?
            // todo do we need a clone here?
            Location location1 = location;
            location1.Level2Code = null;
            location1.Level1Code = null;
            location1.Level3Code = null;

            Gadm level1 = FindLevel1(location1);
            if (level1 != null)
            {
                location1.Level1Code = level1.ID_1;
                Gadm level2 = FindLevel2(location1);
                if (level2 != null)
                {
                    location1.Level2Code = level1.ID_2;
                    Gadm level3 = FindLevel3(location1);
                    if (level3 != null)
                    {
                        location1.Level3Code = level3.ID_3;
                    }
                }
            }

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

        private Gadm AltLevel1Record(Location location)
        {
            Gadm record = null;
            // get the alternate name and try again
            IEnumerable<Level1NearMatch> nearMatches =
                nearMatchesProvider.GetActualMatches(location.Level1);
            //  note there should only ever be one actual name for the given alt name
            // todo we need to ensure that there is only one name posibility in the database
            Level1NearMatch nearMatch = nearMatches.FirstOrDefault();
            if (nearMatch != null)
            {
                // try with the actual name
                location.Level1 = nearMatch.Level1;
                record = Level1Record(location);
            }

            return record;
        }

        private Gadm AltLevel2Record(Location location)
        {
            Gadm record = null;
            // get the alternate name and try again
            IEnumerable<Level2NearMatch> nearMatches =
                nearMatchesProvider.GetActualMatches(location.Level2, location.Level1);
            //  note there should only ever be one actual name for the given alt name
            // todo we need to ensure that there is only one name posibility in the database
            Level2NearMatch nearMatch = nearMatches.FirstOrDefault();
            if (nearMatch != null)
            {
                // try with the actual name
                location.Level2 = nearMatch.Level2;
                record = Level2Record(location);
            }
            return record;
        }

        private Gadm AltLevel3Record(Location location)
        {
            Gadm record = null;
            // get the alternate name and try again
            IEnumerable<Level3NearMatch> nearMatches =
                nearMatchesProvider.GetActualMatches(
                    location.Level3,
                    location.Level1,
                    location.Level2);
            //  note there should only ever be one actual name for the given alt name
            // todo we need to ensure that there is only one name posibility in the database
            Level3NearMatch nearMatch = nearMatches.FirstOrDefault();
            if (nearMatch != null)
            {
                // try with the actual name
                location.Level3 = nearMatch.Level3;
                record = Level3Record(location);
            }
            return record;
        }

        private Gadm FindLevel1(Location location)
        {
            Gadm gadm = Level1Record(location);
            if (gadm == null)
            {
                gadm = AltLevel1Record(location);
            }
            return gadm;
        }

        private Gadm FindLevel2(Location location)
        {
            Gadm gadm = Level2Record(location);
            if (gadm == null)
            {
                gadm = AltLevel2Record(location);
            }

            return gadm;
        }

        private Gadm FindLevel3(Location location)
        {
            Gadm gadm = Level3Record(location);
            if (gadm == null)
            {
                gadm = AltLevel3Record(location);
            }
            return gadm;
        }

        private Gadm Level1Record(Location location)
        {
            // just match level 1
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Level1.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            Gadm firstOrDefault = matchRecords.FirstOrDefault();
            return firstOrDefault;
        }

        private Gadm Level2Record(Location location)
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

        private Gadm Level3Record(Location location)
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