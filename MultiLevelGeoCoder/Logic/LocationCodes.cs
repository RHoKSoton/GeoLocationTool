// LocationCodes.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using Model;

    /// <summary>
    /// Provides the location codes where there are matches in the gazetteer data
    /// using either the given names or the previously matched names in the database
    /// Contains the main search algorithm.
    /// </summary>
    internal class LocationCodes
    {
        #region Fields

        private readonly IEnumerable<Gadm> gazzetteerData;
        private readonly IMatchProvider matchProvider;
        private Dictionary<string, GeoCode> level1Dictionary = null;
        private Dictionary<string, GeoCode> level2Dictionary = null;
        private Dictionary<string, GeoCode> level3Dictionary = null;
        public static bool useDictionaries = true;
        
        #endregion Fields

        #region Comparers
        class CompareLevel1 : IEqualityComparer<Gadm>
        {
            public bool Equals(Gadm x, Gadm y)
            {
                return x.NAME_1 == y.NAME_1;
            }
            public int GetHashCode(Gadm codeh)
            {
                return codeh.NAME_1.GetHashCode();
            }
        }

        class CompareLevel2 : IEqualityComparer<Gadm>
        {
            public bool Equals(Gadm x, Gadm y)
            {
                return x.NAME_1 == y.NAME_1 && x.NAME_2 == y.NAME_2;
            }
            public int GetHashCode(Gadm codeh)
            {
                return (codeh.NAME_2 + codeh.NAME_1).GetHashCode();
            }
        }
        #endregion

        #region Constructors

        public LocationCodes(
            IEnumerable<Gadm> gazzetteerData,
            IMatchProvider matchProvider)
        {
            this.gazzetteerData = gazzetteerData;
            this.matchProvider = matchProvider;
            level1Dictionary = gazzetteerData.Distinct(new CompareLevel1()).ToDictionary(x => x.NAME_1.Trim().ToLower(), x => new GeoCode(x.ID_1, x.NAME_1));
            level2Dictionary = gazzetteerData.Distinct(new CompareLevel2()).ToDictionary(x => x.NAME_1.Trim().ToLower() + x.NAME_2.Trim().ToLower(), x => new GeoCode(x.ID_2, x.NAME_2));
            level3Dictionary = gazzetteerData.ToDictionary(x => x.NAME_1.Trim().ToLower() + x.NAME_2.Trim().ToLower() + x.NAME_3.Trim().ToLower(), x => new GeoCode(x.ID_3, x.NAME_3));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the location codes.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The location with the codes</returns>
        public CodedLocation GetCodes(Location location)
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

        private GeoCode GetLevel1Code(Location location)
        {
            return Level1UsingMatchedName(location) ??
                   Level1UsingGazetteer(location);
        }

        private GeoCode GetLevel2Code(Location location)
        {
            return Level2UsingMatchedName(location) ??
                   Level2UsingGazetteer(location);
        }

        private GeoCode GetLevel3Code(Location location)
        {
            return Level3UsingMatchedName(location) ??
                   Level3UsingGazetteer(location);
        }

        private GeoCode Level1UsingGazetteer(Location location)
        {
            if (string.IsNullOrEmpty(location.Name1))
            {
                return null;
            }

            GeoCode geoCode = null;

            if (useDictionaries)
            {
                level1Dictionary.TryGetValue(location.Name1.Trim().ToLower(), out geoCode);
                return geoCode;
            }

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
            if (string.IsNullOrEmpty(location.Name1))
            {
                return null;
            }

            GeoCode record = null;
            // get the matched name and try again
            IEnumerable<Level1Match> matches =
                matchProvider.GetMatches(location.Name1);
            //  note there should only ever be one actual name for the given alt name
            // todo we need to ensure that there is only one name possibility in the database
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
            if (string.IsNullOrEmpty(location.Name2))
            {
                return null;
            }

            GeoCode geoCode = null;

            if (useDictionaries)
            {
                level2Dictionary.TryGetValue(location.Name1.Trim().ToLower() + location.Name2.Trim().ToLower(), out geoCode);
                return geoCode;
            }

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
            if (string.IsNullOrEmpty(location.Name2))
            {
                return null;
            }

            GeoCode record = null;
            // get the matched name and try again
            IEnumerable<Level2Match> nearMatches =
                matchProvider.GetMatches(location.Name2, location.Name1);
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
            if (string.IsNullOrEmpty(location.Name3))
            {
                return null;
            }

            GeoCode geoCode = null;

            if (useDictionaries)
            {
                level3Dictionary.TryGetValue(location.Name1.Trim().ToLower() + location.Name2.Trim().ToLower() + location.Name3.Trim().ToLower(), out geoCode);
                return geoCode;
            }
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
            if (string.IsNullOrEmpty(location.Name3))
            {
                return null;
            }

            GeoCode record = null;

            // get the matched name and try again
            IEnumerable<Level3Match> nearMatches =
                matchProvider.GetMatches(
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