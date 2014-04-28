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

        private readonly IEnumerable<GazetteerRecord> gazzetteerData;
        private readonly MatchedNamesCache matchedNamesCache;
        private readonly IMatchProvider matchProvider;
        private readonly GazetteerDataDictionaries dictionary;

        #endregion Fields

        #region Constructors

        public LocationCodes(
            IEnumerable<GazetteerRecord> gazzetteerData,
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
        /// <param name="useMatchedNamesCache">If true, uses the cache of matched names</param>
        /// <returns>The location with the codes</returns>
        public CodedLocation GetCodes(
            Location location,
            bool useMatchedNamesCache = false)
        {
            CodedLocation codedLocation = new CodedLocation(location);
            GetLevel1Code(codedLocation, useMatchedNamesCache);

            if (codedLocation.GeoCode1 != null)
            {
                GetLevel2Code(codedLocation, useMatchedNamesCache);
                if (codedLocation.GeoCode2 != null)
                {
                    GetLevel3Code(codedLocation, useMatchedNamesCache);
                }
            }

            return codedLocation;
        }

        public void RefreshMatchedNamesCache()
        {
            matchedNamesCache.Refresh();
        }

        private void GetLevel1Code(CodedLocation location, bool useCache)
        {
            Level1UsingGazetteer(location);

            if (location.GeoCode1 == null)
            {
                Level1UsingMatchedName(location, useCache);
            }
        }

        private void GetLevel2Code(CodedLocation location, bool useCache)
        {
            Level2UsingGazetteer(location);
            if (location.GeoCode2 == null)
            {
                Level2UsingMatchedName(location, useCache);
            }
        }

        private void GetLevel3Code(CodedLocation location, bool useCache)
        {
            Level3UsingGazetteer(location);
            if (location.GeoCode3 == null)
            {
                Level3UsingMatchedName(location, useCache);
            }
        }

        private void Level1UsingGazetteer(CodedLocation location)
        {
            if (string.IsNullOrEmpty(location.Name1))
            {
                return;
            }


            location.GeoCode1 = dictionary.GetLevel1Code(
                location.Name1);



        }

        private void Level1UsingMatchedName(CodedLocation location, bool useCache)
        {
            if (string.IsNullOrEmpty(location.Name1))
            {
                return;
            }

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
                Level1UsingGazetteer(location);
            }
        }

        private void Level2UsingGazetteer(CodedLocation location)
        {
            if (string.IsNullOrEmpty(location.Name2))
            {
                return;
            }


            location.GeoCode2 = dictionary.GetLevel2Code(
                location.GeoCode1.Name,
                location.Name2);



        }

        private void Level2UsingMatchedName(CodedLocation location, bool useCache)
        {
            if (string.IsNullOrEmpty(location.Name2))
            {
                return;
            }

            // get the matched name and try again
            Level2Match match;
            if (useCache)
            {
                match = matchedNamesCache.Level2Match(
                    location.GeoCode1.Name,
                    location.Name2);
            }
            else
            {
                IEnumerable<Level2Match> nearMatches =
                    matchProvider.GetMatches(location.Name2, location.GeoCode1.Name);
                //  note there should only ever be one actual name for the given alt name
                // todo we need to ensure that there is only one name posibility in the database
                match = nearMatches.FirstOrDefault();
            }

            if (match != null)
            {
                // try with the matched name
                location.Name2 = match.Level2;
                Level2UsingGazetteer(location);
            }
        }

        private void Level3UsingGazetteer(CodedLocation location)
        {
            if (string.IsNullOrEmpty(location.Name3))
            {
                return;
            }

            location.GeoCode3 = dictionary.GetLevel3Code(
                location.GeoCode1.Name,
                location.GeoCode2.Name,
                location.Name3);

        }

        private void Level3UsingMatchedName(CodedLocation location, bool useCache)
        {
            if (string.IsNullOrEmpty(location.Name3))
            {
                return;
            }

            // get the matched name and try again
            Level3Match match;
            if (useCache)
            {
                match = matchedNamesCache.Level3Match(
                    location.GeoCode1.Name,
                    location.GeoCode2.Name,
                    location.Name3);
            }
            else
            {
                IEnumerable<Level3Match> nearMatches =
                    matchProvider.GetMatches(
                        location.Name3,
                        location.GeoCode1.Name,
                        location.GeoCode2.Name);
                //  note there should only ever be one actual name for the given alt name
                // todo we need to ensure that there is only one name posibility in the database
                match = nearMatches.FirstOrDefault();
            }

            if (match != null)
            {
                // try with the matched name
                location.Name3 = match.Level3;
                Level3UsingGazetteer(location);
            }
        }

        #endregion Methods
    }
}