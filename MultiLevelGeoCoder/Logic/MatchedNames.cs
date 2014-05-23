// MatchedNames.cs

namespace MultiLevelGeoCoder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public MatchResult GetSavedMatchLevel1(string level1)
        {
            IEnumerable<Level1Match> matches = matchProvider.GetMatches(level1).ToList();
            int count = matches.Count();

            if (count > 1)
            {
                // there must only be a max of one saved match for any given input.
                var msg = string.Format(
                    "[{0}] matched names found for the input [{1}]",
                    count,
                    level1);
                throw new InvalidOperationException(msg);
            }

            Level1Match match = matches.FirstOrDefault();
            return match != null ? new MatchResult(match.Level1, match.Weight) : null;
        }

        public MatchResult GetSavedMatchLevel2(string level2, string level1)
        {
            IEnumerable<Level2Match> matches =
                matchProvider.GetMatches(level2, level1).ToList();
            int count = matches.Count();
            if (count > 1)
            {
                // there must only be a max of one saved match for any given input.
                var msg = string.Format(
                    "[{0}] matched names found for the input [{1}] [{2}",
                    count,
                    level1,
                    level2);
                throw new InvalidOperationException(msg);
            }
            Level2Match match = matches.FirstOrDefault();
            return match != null ? new MatchResult(match.Level2, match.Weight) : null;
        }

        public MatchResult GetSavedMatchLevel3(
            string level3,
            string level1,
            string level2)
        {
            IEnumerable<Level3Match> matches =
                matchProvider.GetMatches(level3, level1, level2).ToList();
            int count = matches.Count();

            if (count > 1)
            {
                // there must only be a max of one saved match for any given input.
                var msg = string.Format(
                    "[{0}] matched names found for the input [{1}] [{2}],[{3}]",
                    count,
                    level1,
                    level2,
                    level3);
                throw new InvalidOperationException(msg);
            }

            Level3Match match = matches.FirstOrDefault();
            return match != null ? new MatchResult(match.Level3, match.Weight) : null;
        }

        public void SaveMatch(
            Location inputLocation,
            Location gazetteerLocation,
            LocationNames locationNames)
        {
            Validate(inputLocation);
            Validate(gazetteerLocation);
            MatchedName match = new MatchedName(inputLocation, gazetteerLocation);

            // Don't save alts to the db
            SubstituteMainForAltNames(match, locationNames);

            // Throw ex if the input already exists in the gazetteer
            ValidateMatch(match, locationNames);

            SaveMatchLevel1(match);
            SaveMatchLevel2(match);
            SaveMatchLevel3(match);
        }

        private static void SubstituteAltGazetteerName(
            Location location,
            LocationNames locationNames)
        {
            if (!string.IsNullOrEmpty(location.Name1))
            {
                string main = locationNames.GetMainLevel1(location.Name1);
                if (main == null)
                {
                    throw new ArgumentException(
                        string.Format(
                            "Location match not in in the gazetteer[{0}",
                            location.Name1),
                        "location");
                }
                location.Name1 = main;
            }

            if (!string.IsNullOrEmpty(location.Name2))
            {
                string main =
                    locationNames.GetMainLevel2(location.Name1, location.Name2);
                if (main == null)
                {
                    throw new ArgumentException(
                        string.Format(
                            "Location match not in in the gazetteer[{0}",
                            location.Name2),
                        "location");
                }
                location.Name2 = main;
            }

            if (!string.IsNullOrEmpty(location.Name3))
            {
                string main =
                    locationNames.GetMainLevel3(
                        location.Name1,
                        location.Name2,
                        location.Name3);
                if (main == null)
                {
                    throw new ArgumentException(
                        string.Format(
                            "Location match not in in the gazetteer[{0}",
                            location.Name3),
                        "location");
                }
                location.Name3 = main;
            }
        }

        private static void SubstituteAltInputName(
            Location location,
            LocationNames locationNames)
        {
            if (!string.IsNullOrEmpty(location.Name1))
            {
                string main = locationNames.GetMainLevel1(location.Name1);
                if (!string.IsNullOrEmpty(main))
                {
                    location.Name1 = main;
                }
            }

            if (!string.IsNullOrEmpty(location.Name2))
            {
                string main = locationNames.GetMainLevel2(location.Name1, location.Name2);
                if (!string.IsNullOrEmpty(main))
                {
                    location.Name2 = main;
                }
            }

            if (!string.IsNullOrEmpty(location.Name3))
            {
                string main = locationNames.GetMainLevel3(
                    location.Name1,
                    location.Name2,
                    location.Name3);
                if (!string.IsNullOrEmpty(main))
                {
                    location.Name3 = main;
                }
            }
        }

        private static void Validate(Location location)
        {
            // throw ex if location is invalid
            location.Validate();
        }

        private static void ValidateLevel1Match(
            MatchedName match,
            LocationNames locationNames,
            StringBuilder stringBuilder)
        {
            if (locationNames.IsInLevel1Names(match.InputLocation.Name1))
            {
                if (match.Level1NotSame())
                {
                    // The input value is in the gazetteer but is not the same as the selected match
                    stringBuilder.Append(match.OriginalInput.Name1);
                }
            }
        }

        private static void ValidateLevel2Match(
            MatchedName match,
            LocationNames locationNames,
            StringBuilder errorMessage)
        {
            if (locationNames.IsInLevel2Names(
                match.InputLocation.Name2,
                match.InputLocation.Name1))
            {
                if (match.Level2NotSame())
                {
                    // The input value is in the gazetteer but is not the same as the selected match
                    errorMessage.Append(match.OriginalInput.Name2);
                }
            }
        }

        private static void ValidateLevel3Match(
            MatchedName match,
            LocationNames locationNames,
            StringBuilder errorMessage)
        {
            if (locationNames.IsInLevel3Names(
                match.InputLocation.Name3,
                match.InputLocation.Name1,
                match.InputLocation.Name2))
            {
                if (match.Level3NotSame())
                {
                    // The input value is in the gazetteer but is not the same as the selected match
                    errorMessage.Append(match.OriginalInput.Name3);
                }
            }
        }

        private static void ValidateMatch(MatchedName match, LocationNames locationNames)
        {
            StringBuilder errorMessage = new StringBuilder();

            // We do not allow saved matches for input names that already exist in the gazetteer
            // as this would result in conflicting information so throw ex if the input
            // is in the gazetteer and is not the same as the selection.
            ValidateLevel1Match(match, locationNames, errorMessage);
            ValidateLevel2Match(match, locationNames, errorMessage);
            ValidateLevel3Match(match, locationNames, errorMessage);

            if (errorMessage.Length > 0)
            {
                string message = string.Format(
                    "Input name is already in the gazetteer, cannot save match: {0} ",
                    errorMessage);
                throw new NameInGazetteerException(message);
            }
        }

        private void SaveMatchLevel1(MatchedName match)
        {
            // Don't save if the values are the same
            if (match.Level1NotSame())
            {
                matchProvider.SaveMatchLevel1(
                    match.InputLocation.Name1,
                    match.GazetteerLocation.Name1);
            }
        }

        private void SaveMatchLevel2(MatchedName match)
        {
            // Don't save if the values are the same
            if (match.Level2NotSame())
            {
                matchProvider.SaveMatchLevel2(
                    match.InputLocation.Name2,
                    match.GazetteerLocation.Name1,
                    match.GazetteerLocation.Name2);
            }
        }

        private void SaveMatchLevel3(MatchedName match)
        {
            // Don't save if the values are the same
            if (match.Level3NotSame())
            {
                matchProvider.SaveMatchLevel3(
                    match.InputLocation.Name3,
                    match.GazetteerLocation.Name1,
                    match.GazetteerLocation.Name2,
                    match.GazetteerLocation.Name3);
            }
        }

        private void SubstituteMainForAltNames(
            MatchedName match,
            LocationNames locationNames)
        {
            SubstituteAltGazetteerName(match.GazetteerLocation, locationNames);
            SubstituteAltInputName(match.InputLocation, locationNames);
        }

        #endregion Methods
    }
}