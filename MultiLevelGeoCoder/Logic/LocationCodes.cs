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

        public static bool useDictionaries = true; // for performance testing

        //todo remove after testing
        public static bool UseGazetteerFirst = false; // for testing

        private readonly IEnumerable<Gadm> gazzetteerData;
        private readonly MatchedNamesCache matchedNamesCache;
        private readonly IMatchProvider matchProvider;
        private readonly GazetteerDataDictionaries dictionary;

        private Dictionary<string, GeoCode> level1Dictionary;
        private Dictionary<string, GeoCode> level2Dictionary;
        private Dictionary<string, GeoCode> level3Dictionary;

        #endregion Fields

        #region Constructors

        public LocationCodes(
            IEnumerable<Gadm> gazzetteerData,
            IMatchProvider matchProvider)
        {
            this.gazzetteerData = gazzetteerData;
            this.matchProvider = matchProvider;
            matchedNamesCache = new MatchedNamesCache(matchProvider);
            dictionary = new GazetteerDataDictionaries(this.gazzetteerData);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the location codes.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="useCache">Uses the cache of matched names if true</param>
        /// <returns>The location with the codes</returns>
        public CodedLocation GetCodes(Location location, bool useCache = false)
        {
            CodedLocation codedLocation = new CodedLocation(location);

            codedLocation.GeoCode1 = GetLevel1Code(location, useCache);
            if (codedLocation.GeoCode1 != null)
            {
                codedLocation.GeoCode2 = GetLevel2Code(location, useCache);
                if (codedLocation.GeoCode2 != null)
                {
                    codedLocation.GeoCode3 = GetLevel3Code(location, useCache);
                }
            }

            return codedLocation;
        }

        public void RefreshMatchedNamesCache()
        {
            matchedNamesCache.Refresh();
        }

        private GeoCode GetLevel1Code(Location location, bool useCache)
        {
            if (UseGazetteerFirst)
            {
                return Level1UsingGazetteer(location) ??
                       Level1UsingMatchedName(location, useCache);
            }

            return Level1UsingMatchedName(location, useCache) ??
                   Level1UsingGazetteer(location);
        }

        private GeoCode GetLevel2Code(Location location, bool useCache)
        {
            if (UseGazetteerFirst)
            {
                return
                    Level2UsingGazetteer(location) ??
                    Level2UsingMatchedName(location, useCache);
            }

            return Level2UsingMatchedName(location, useCache) ??
                   Level2UsingGazetteer(location);
        }

        private GeoCode GetLevel3Code(Location location, bool useCache)
        {
            if (UseGazetteerFirst)
            {
                return Level3UsingGazetteer(location) ??
                       Level3UsingMatchedName(location, useCache);
            }
            return Level3UsingMatchedName(location, useCache) ??
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
                geoCode = dictionary.GetLevel1Code(
                    location.Name1);

                return geoCode;
            }

            // just match level 1
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.Name1,
                        location.Name1,
                        StringComparison.OrdinalIgnoreCase))
                select record;

            Gadm firstOrDefault = matchRecords.FirstOrDefault();
            if (firstOrDefault != null)
            {
                geoCode = new GeoCode(firstOrDefault.Id1, firstOrDefault.Name1);
            }

            return geoCode;
        }

        private GeoCode Level1UsingMatchedName(Location location, bool useCache)
        {
            if (string.IsNullOrEmpty(location.Name1))
            {
                return null;
            }

            GeoCode record = null;

            // get the matched name and try again
            Level1Match match;
            if (useCache)
            {
                match = matchedNamesCache.Level1Match(location.Name1);
            }
            else
            {
                IEnumerable<Level1Match> matches =
                    matchProvider.GetMatches(location.Name1);
                //  note there should only ever be one actual name for the given alt name
                // todo we need to ensure that there is only one name possibility in the database
                match = matches.FirstOrDefault();
            }

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
                geoCode = dictionary.GetLevel2Code(
                    location.Name1,
                    location.Name2);

                return geoCode;
            }

            // must match level 1 and 2
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.Name1,
                        location.Name1.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.Name2,
                        location.Name2.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            if (firstOrDefault != null)
            {
                geoCode = new GeoCode(firstOrDefault.Id2, firstOrDefault.Name2);
            }
            return geoCode;
        }

        private GeoCode Level2UsingMatchedName(Location location, bool useCache)
        {
            if (string.IsNullOrEmpty(location.Name2))
            {
                return null;
            }

            GeoCode record = null;

            // get the matched name and try again
            Level2Match match;
            if (useCache)
            {
                match = matchedNamesCache.Level2Match(location.Name1, location.Name2);
            }
            else
            {
                IEnumerable<Level2Match> nearMatches =
                    matchProvider.GetMatches(location.Name2, location.Name1);
                //  note there should only ever be one actual name for the given alt name
                // todo we need to ensure that there is only one name posibility in the database
                match = nearMatches.FirstOrDefault();
            }

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
                geoCode = dictionary.GetLevel3Code(
                    location.Name1,
                    location.Name2,
                    location.Name3);

                return geoCode;
            }
            // must match all three levels
            var matchRecords = from record in gazzetteerData
                where
                    (String.Equals(
                        record.Name1,
                        location.Name1.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.Name2,
                        location.Name2.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.Name3,
                        location.Name3.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            if (firstOrDefault != null)
            {
                geoCode = new GeoCode(firstOrDefault.Id3, firstOrDefault.Name3);
            }
            return geoCode;
        }

        private GeoCode Level3UsingMatchedName(Location location, bool useCache)
        {
            if (string.IsNullOrEmpty(location.Name3))
            {
                return null;
            }

            GeoCode record = null;

            // get the matched name and try again
            Level3Match match;
            if (useCache)
            {
                match = matchedNamesCache.Level3Match(
                    location.Name1,
                    location.Name2,
                    location.Name3);
            }
            else
            {
                IEnumerable<Level3Match> nearMatches =
                    matchProvider.GetMatches(
                        location.Name3,
                        location.Name1,
                        location.Name2);
                //  note there should only ever be one actual name for the given alt name
                // todo we need to ensure that there is only one name posibility in the database
                match = nearMatches.FirstOrDefault();
            }

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