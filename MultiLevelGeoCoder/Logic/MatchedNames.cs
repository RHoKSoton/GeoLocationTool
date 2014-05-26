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

        private const int DefaultProbability = 2;
            // this is to indicate to the users that a match that has come from the db 

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
            return match != null
                ? new MatchResult(match.Level1, DefaultProbability)
                : null;
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
            return match != null
                ? new MatchResult(match.Level2, DefaultProbability)
                : null;
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
            return match != null
                ? new MatchResult(match.Level3, DefaultProbability)
                : null;
        }

        public void SaveMatch(
            Location inputLocation,
            Location gazetteerLocation,
            GazetteerLocationNames gazetteerLocationNames)
        {
            Validate(inputLocation);
            Validate(gazetteerLocation);
            MatchedName match = new MatchedName(inputLocation, gazetteerLocation);

            // Don't save alts to the db
            SubstituteMainForAltNames(match, gazetteerLocationNames);

            // Throw ex if the input already exists in the gazetteer
            ValidateMatch(match, gazetteerLocationNames);

            SaveMatchLevel1(match);
            SaveMatchLevel2(match);
            SaveMatchLevel3(match);
        }

        private static void SubstituteAltGazetteerName(
            Location location,
            GazetteerLocationNames gazetteerLocationNames)
        {
            if (!string.IsNullOrEmpty(location.Name1))
            {
                string main = gazetteerLocationNames.GetMainLevel1(location.Name1);
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
                    gazetteerLocationNames.GetMainLevel2(location.Name1, location.Name2);
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
                    gazetteerLocationNames.GetMainLevel3(
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
            GazetteerLocationNames gazetteerLocationNames)
        {
            if (!string.IsNullOrEmpty(location.Name1))
            {
                string main = gazetteerLocationNames.GetMainLevel1(location.Name1);
                if (!string.IsNullOrEmpty(main))
                {
                    location.Name1 = main;
                }
            }

            if (!string.IsNullOrEmpty(location.Name2))
            {
                string main = gazetteerLocationNames.GetMainLevel2(location.Name1, location.Name2);
                if (!string.IsNullOrEmpty(main))
                {
                    location.Name2 = main;
                }
            }

            if (!string.IsNullOrEmpty(location.Name3))
            {
                string main = gazetteerLocationNames.GetMainLevel3(
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
            GazetteerLocationNames gazetteerLocationNames,
            StringBuilder stringBuilder)
        {
            if (gazetteerLocationNames.IsInLevel1Names(match.InputLocation.Name1))
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
            GazetteerLocationNames gazetteerLocationNames,
            StringBuilder errorMessage)
        {
            if (gazetteerLocationNames.IsInLevel2Names(
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
            GazetteerLocationNames gazetteerLocationNames,
            StringBuilder errorMessage)
        {
            if (gazetteerLocationNames.IsInLevel3Names(
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

        private static void ValidateMatch(MatchedName match, GazetteerLocationNames gazetteerLocationNames)
        {
            StringBuilder errorMessage = new StringBuilder();

            // We do not allow saved matches for input names that already exist in the gazetteer
            // as this would result in conflicting information so throw ex if the input
            // is in the gazetteer and is not the same as the selection.
            ValidateLevel1Match(match, gazetteerLocationNames, errorMessage);
            ValidateLevel2Match(match, gazetteerLocationNames, errorMessage);
            ValidateLevel3Match(match, gazetteerLocationNames, errorMessage);

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
            GazetteerLocationNames gazetteerLocationNames)
        {
            SubstituteAltGazetteerName(match.GazetteerLocation, gazetteerLocationNames);
            SubstituteAltInputName(match.InputLocation, gazetteerLocationNames);
        }

        #endregion Methods
    }
}