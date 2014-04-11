// MatchedNames.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using Model;

    /// <summary>
    ///  Gets and saves matched names, ie the match between a miss-spelt name
    ///  and a name in the gazetteer.
    /// </summary>
    internal class MatchedNames
    {
        #region Fields

        private readonly IMatchProvider matchProvider;

        #endregion Fields

        #region Constructors

        public MatchedNames(IMatchProvider matchProvider)
        {
            this.matchProvider = matchProvider;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<MatchResult> GetSavedMatchLevel1(string level1)
        {
            //todo change return type to single result

            List<MatchResult> result = new List<MatchResult>();
            Level1Match match = matchProvider.GetMatches(level1).FirstOrDefault();
            if (match != null)
            {
                result.Add(new MatchResult(match.Level1, match.Weight));
            }
            return result;
        }

        public IEnumerable<MatchResult> GetSavedMatchLevel2(string level2, string level1)
        {
            // todo change return type to single result

            List<MatchResult> result = new List<MatchResult>();
            Level2Match match = matchProvider.GetMatches(level2, level1).FirstOrDefault();
            if (match != null)
            {
                result.Add(new MatchResult(match.Level2, match.Weight));
            }
            return result;
        }

        public IEnumerable<MatchResult> GetSavedMatchLevel3(
            string level3,
            string level1,
            string level2)
        {
            // todo change return type to single result

            List<MatchResult> result = new List<MatchResult>();
            Level3Match match =
                matchProvider.GetMatches(level3, level1, level2).FirstOrDefault();
            if (match != null)
            {
                result.Add(new MatchResult(match.Level3, match.Weight));
            }
            return result;
        }

        public void SaveMatch(
            Location inputLocation,
            Location gazetteerLocation,
            LocationNames locationNames)
        {
            bool hasLevel1 = !string.IsNullOrEmpty(inputLocation.Name1);
            bool hasLevel2 = !string.IsNullOrEmpty(inputLocation.Name2);
            bool hasLevel3 = !string.IsNullOrEmpty(inputLocation.Name3);

            // throw ex if  locations are not complete
            CheckLocationsAreComplete(inputLocation, gazetteerLocation);

            // if input and gazetteer are the same don't save, just exit
            if (inputLocation.Equals(gazetteerLocation))
            {
                return;
            }

            // throw if the input is already in the gazetteer as we do not allow saved matches for existing names
            // if input and gazetteer value is the same, dont save

            // level1
            if (hasLevel1)
            {
                if (AreNotTheSame(inputLocation.Name1, gazetteerLocation.Name1))
                {
                    CheckLevel1NotInGazetteer(inputLocation, locationNames);
                    SaveMatchLevel1(inputLocation.Name1, gazetteerLocation.Name1);
                }

                // level 2
                if (hasLevel2)
                {
                    if (AreNotTheSame(inputLocation.Name2, gazetteerLocation.Name2))
                    {
                        CheckLevel2NotInGazetteer(
                            inputLocation,
                            gazetteerLocation,
                            locationNames);
                        SaveMatchLevel2(
                            inputLocation.Name2,
                            gazetteerLocation.Name1,
                            gazetteerLocation.Name2);
                    }

                    // level 3
                    if (hasLevel3)
                    {
                        if (AreNotTheSame(inputLocation.Name3, gazetteerLocation.Name3))
                        {
                            CheckLevel3NotInGazetteer(
                                inputLocation,
                                gazetteerLocation,
                                locationNames);
                            SaveMatchLevel3(
                                inputLocation.Name3,
                                gazetteerLocation.Name1,
                                gazetteerLocation.Name2,
                                gazetteerLocation.Name3);
                        }
                    }
                }
            }
        }

        private static bool AreNotTheSame(string string1, string string2)
        {
            return
                !string.Equals(
                    string1,
                    string2,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        private static void CheckLevel1NotInGazetteer(
            Location inputLocation,
            LocationNames locationNames)
        {
            // if the suggestion/input combination exists in the gazetteer throw ex
            if (LocationNames.IsInLevel1Names(inputLocation.Name1, locationNames))
            {
                string message = string.Format(
                    "Name already in the gazetteer: {0} ",
                    inputLocation.Name1);
                throw new NameInGazetteerException(message);
            }
        }

        private static void CheckLevel2NotInGazetteer(
            Location inputLocation,
            Location gazetteerLocation,
            LocationNames locationNames)
        {
            if (LocationNames.IsInLevel2Names(
                inputLocation.Name2,
                gazetteerLocation.Name1,
                locationNames))
            {
                string message = string.Format(
                    "Name already in the gazetteer: {0} ",
                    inputLocation.Name2);
                throw new NameInGazetteerException(message);
            }
        }

        private static void CheckLevel3NotInGazetteer(
            Location inputLocation,
            Location gazetteerLocation,
            LocationNames locationNames)
        {
            if (LocationNames.IsInLevel3Names(
                inputLocation.Name3,
                gazetteerLocation.Name1,
                gazetteerLocation.Name2,
                locationNames))
            {
                string message = string.Format(
                    "Name already in the gazetteer: {0} ",
                    inputLocation.Name3);
                throw new NameInGazetteerException(message);
            }
        }

        private static void CheckLocationsAreComplete(
            Location inputLocation,
            Location gazetteerLocation)
        {
            inputLocation.Validate();
            gazetteerLocation.Validate();
        }

        private void SaveMatchLevel1(string alternateLevel1, string gazetteerLevel1)
        {
            matchProvider.SaveMatchLevel1(alternateLevel1, gazetteerLevel1);
        }

        private void SaveMatchLevel2(
            string alternateLevel2,
            string gazetteerLevel1,
            string gazetteerLevel2)
        {
            matchProvider.SaveMatchLevel2(
                alternateLevel2,
                gazetteerLevel1,
                gazetteerLevel2);
        }

        private void SaveMatchLevel3(
            string alternateLevel3,
            string gazetteerLevel1,
            string gazetteerLevel2,
            string gazetteerLevel3)
        {
            matchProvider.SaveMatchLevel3(
                alternateLevel3,
                gazetteerLevel1,
                gazetteerLevel2,
                gazetteerLevel3);
        }

        #endregion Methods
    }
}