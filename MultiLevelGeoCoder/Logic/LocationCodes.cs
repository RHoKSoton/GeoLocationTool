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
    /// Provides the location codes where there are matches
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
        /// Gets the location codes.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The location with the codes</returns>
        public CodedLocation GetLocationCodes(Location location)
        {
            CodedLocation codedLocation = new CodedLocation(location);

            codedLocation.GeoCode1 = GetLevel1Code(location);
            if (codedLocation.GeoCode1 != null)
            {
                codedLocation.GeoCode2 = GetLevel2Code(location);
                if (codedLocation.GeoCode2 != null)
                {
                    codedLocation.GeoCode3 = GetLevel3Code(location);
                }
            }

            return codedLocation;
        }

        ///// <summary>
        ///// Lists the Level 1 location names.
        ///// </summary>
        ///// <returns>List of location names</returns>
        //public IList<string> Level1LocationNames()
        //{
        //    var levelList = gazzetteerData.Select(l => l.NAME_1);
        //    return levelList.Distinct().OrderBy(i => i).ToList();
        //}
        ///// <summary>
        ///// Lists the Level 2 location names for the given level 1.
        ///// </summary>
        ///// <param name="level1Name">Name of the level 1 location.</param>
        ///// <returns>List of location names.</returns>
        //public IList<string> Level2LocationNames(string level1Name)
        //{
        //    var levelList = gazzetteerData
        //        .Where(
        //            n =>
        //                String.Equals(
        //                    n.NAME_1,
        //                    level1Name,
        //                    StringComparison.OrdinalIgnoreCase))
        //        .Select(l => l.NAME_2);
        //    return levelList.Distinct().OrderBy(i => i).ToList();
        //}
        ///// <summary>
        ///// Lists the Level 3 location names for the given level 1 and 2.
        ///// </summary>
        ///// <param name="level1Name">Name of the level 1 location.</param>
        ///// <param name="level2Name">Name of the level 2 location.</param>
        ///// <returns>List of location names.</returns>
        //public IList<string> Level3LocationNames(string level1Name, string level2Name)
        //{
        //    var levelList = gazzetteerData
        //        .Where(
        //            n =>
        //                String.Equals(
        //                    n.NAME_1,
        //                    level1Name,
        //                    StringComparison.OrdinalIgnoreCase) &&
        //                String.Equals(
        //                    n.NAME_2,
        //                    level2Name,
        //                    StringComparison.OrdinalIgnoreCase))
        //        .Select(l => l.NAME_3);
        //    return levelList.Distinct().OrderBy(i => i).ToList();
        //}
        private GeoCode GetLevel1Code(Location location)
        {
            return Level1UsingGazetteer(location) ??
                   Level1UsingMatchedName(location);
        }

        private GeoCode GetLevel2Code(Location location)
        {
            return Level2UsingGazetteer(location) ??
                   Level2UsingMatchedName(location);
        }

        private GeoCode GetLevel3Code(Location location)
        {
            return Level3UsingGazetteer(location) ??
                   Level3UsingMatchedName(location);
        }

        private GeoCode Level1UsingGazetteer(Location location)
        {
            GeoCode geoCode = null;
            // just match level 1
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Name1,
                        StringComparison.OrdinalIgnoreCase))
                select record;

            Gadm firstOrDefault = matchRecords.FirstOrDefault();
            if (firstOrDefault != null)
            {
                geoCode = new GeoCode(firstOrDefault.ID_1, firstOrDefault.NAME_1);
            }

            return geoCode;
        }

        private GeoCode Level1UsingMatchedName(Location location)
        {
            GeoCode record = null;
            // get the matched name and try again
            IEnumerable<Level1Match> matches =
                nearMatchesProvider.GetActualMatches(location.Name1);
            //  note there should only ever be one actual name for the given alt name
            // todo we need to ensure that there is only one name posibility in the database
            Level1Match match = matches.FirstOrDefault();
            if (match != null)
            {
                // try with the matched name
                location.Name1 = match.Level1;
                record = Level1UsingGazetteer(location);
            }

            return record;
        }

        private GeoCode Level2UsingGazetteer(Location location)
        {
            GeoCode geoCode = null;
            // must match level 1 and 2
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Name1.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_2,
                        location.Name2.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            if (firstOrDefault != null)
            {
                geoCode = new GeoCode(firstOrDefault.ID_2, firstOrDefault.NAME_2);
            }
            return geoCode;
        }

        private GeoCode Level2UsingMatchedName(Location location)
        {
            GeoCode record = null;
            // get the matched name and try again
            IEnumerable<Level2Match> nearMatches =
                nearMatchesProvider.GetActualMatches(location.Name2, location.Name1);
            //  note there should only ever be one actual name for the given alt name
            // todo we need to ensure that there is only one name posibility in the database
            Level2Match match = nearMatches.FirstOrDefault();
            if (match != null)
            {
                // try with the matched name
                location.Name2 = match.Level2;
                record = Level2UsingGazetteer(location);
            }
            return record;
        }

        private GeoCode Level3UsingGazetteer(Location location)
        {
            GeoCode geoCode = null;
            // must match all three levels
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Name1.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_2,
                        location.Name2.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_3,
                        location.Name3.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            if (firstOrDefault != null)
            {
                geoCode = new GeoCode(firstOrDefault.ID_3, firstOrDefault.NAME_3);
            }
            return geoCode;
        }

        private GeoCode Level3UsingMatchedName(Location location)
        {
            GeoCode record = null;
            // get the matched name and try again
            IEnumerable<Level3Match> nearMatches =
                nearMatchesProvider.GetActualMatches(
                    location.Name3,
                    location.Name1,
                    location.Name2);
            //  note there should only ever be one actual name for the given alt name
            // todo we need to ensure that there is only one name posibility in the database
            Level3Match match = nearMatches.FirstOrDefault();
            if (match != null)
            {
                // try with the matched name
                location.Name3 = match.Level3;
                record = Level3UsingGazetteer(location);
            }
            return record;
        }

        #endregion Methods
    }
}