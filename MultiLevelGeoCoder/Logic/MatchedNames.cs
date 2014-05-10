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
            // todo refactor this Save method
            // throw ex if input is invalid
            inputLocation.Validate();

            MatchedName match = new MatchedName(inputLocation, gazetteerLocation);

            // todo move to matchedName
            bool hasLevel1 = (!string.IsNullOrEmpty(inputLocation.Name1)) &&
                             !string.IsNullOrEmpty(gazetteerLocation.Name1);
            bool hasLevel2 = (!string.IsNullOrEmpty(inputLocation.Name2)) &&
                             !string.IsNullOrEmpty(gazetteerLocation.Name2);
            bool hasLevel3 = (!string.IsNullOrEmpty(inputLocation.Name3)) &&
                             !string.IsNullOrEmpty(gazetteerLocation.Name3);

            // todo move to matched name
            // if input and gazetteer are the same don't save, just exit
            if (inputLocation.Equals(gazetteerLocation))
            {
                return;
            }

            // level1
            if (hasLevel1)
            {
                // Get the main values for both the input and the gaz selection in
                // in case it is an alt and use that instead,
                // as we don't save alts to the db
                string mainGaz1 =
                    locationNames.GetMainLevel1(match.GazetteerLocation.Name1);
                if (mainGaz1 == null)
                {
                    throw new ArgumentException(
                        "All matches must be selected from data in the gazetteer",
                        "gazetteerLocation");
                }
                match.GazetteerLocation.Name1 = mainGaz1;

                string mainInput1 = locationNames.GetMainLevel1(match.InputLocation.Name1);

                if (mainInput1 == null)
                {
                    // the input value is not in the gazetteer,
                    // however if input and gazetteer value is the same, don't save
                    if (AreNotTheSame(
                        match.InputLocation.Name1,
                        match.GazetteerLocation.Name1))
                    {
                        SaveMatchLevel1(
                            match.InputLocation.Name1,
                            match.GazetteerLocation.Name1);
                        // todo review this as should not change the input?
                        match.InputLocation.Name1 = match.GazetteerLocation.Name1;
                    }
                }
                else
                {
                    // The input value is in the gazetteer,
                    if (AreNotTheSame(
                        match.InputLocation.Name1,
                        match.GazetteerLocation.Name1))
                    {
                        // We do not allow saved matches for existing names as this
                        // would result in conflicting information so throw ex if the input
                        // is not the same as the selection.
                        string message = string.Format(
                            "Input name is already in the gazetteer, cannot save match: {0} ",
                            match.InputLocation.Name1);
                        throw new NameInGazetteerException(message);
                    }

                    // use the main input value for the lower levels
                    match.InputLocation.Name1 = mainInput1;
                }

                // level 2
                if (hasLevel2)
                {
                    // Get the main values for both the input and the gaz selection in
                    // case it is an alt and use that instead,
                    // we don't save alts to the db
                    string mainGaz2 = locationNames.GetMainLevel2(
                        match.GazetteerLocation.Name1,
                        match.GazetteerLocation.Name2);
                    if (mainGaz2 == null)
                    {
                        throw new ArgumentException(
                            "All matches must be selected from data in the gazetteer",
                            "gazetteerLocation");
                    }
                    match.GazetteerLocation.Name2 = mainGaz2;

                    string mainInput2 =
                        locationNames.GetMainLevel2(
                            match.InputLocation.Name1,
                            match.InputLocation.Name2);

                    if (mainInput2 == null)
                    {
                        // the input value is not in the gazetteer,
                        // however if input and gazetteer value is the same, don't save
                        if (AreNotTheSame(
                            match.InputLocation.Name2,
                            match.GazetteerLocation.Name2))
                        {
                            SaveMatchLevel2(
                                match.InputLocation.Name2,
                                match.GazetteerLocation.Name1,
                                match.GazetteerLocation.Name2);
                            // todo review this as should not change the input?
                            match.InputLocation.Name2 = match.GazetteerLocation.Name2;
                        }
                    }
                    else
                    {
                        // The input value is in the gazetteer,
                        if (AreNotTheSame(
                            match.InputLocation.Name2,
                            match.GazetteerLocation.Name2))
                        {
                            // We do not allow saved matches for existing names as this
                            // would result in conflicting information so throw ex if the input
                            // is not the same as the selection.
                            string message = string.Format(
                                "Input name is already in the gazetteer, cannot save match: {0} ",
                                match.InputLocation.Name2);
                            throw new NameInGazetteerException(message);
                        }

                        // use the main input value for the lower levels
                        match.InputLocation.Name2 = mainInput2;
                    }


                    // level 3
                    if (hasLevel3)
                    {
                        // Get the main values for both the input and the gaz selection in
                        // case it is an alt and use that instead,
                        // we don't save alts to the db
                        string mainGaz3 = locationNames.GetMainLevel3(
                            match.GazetteerLocation.Name1,
                            match.GazetteerLocation.Name2,
                            match.GazetteerLocation.Name3);
                        if (mainGaz3 == null)
                        {
                            throw new ArgumentException(
                                "All matches must be selected from data in the gazetteer",
                                "gazetteerLocation");
                        }
                        match.GazetteerLocation.Name3 = mainGaz3;

                        string mainInput3 =
                            locationNames.GetMainLevel3(
                                match.InputLocation.Name1,
                                match.InputLocation.Name2,
                                match.InputLocation.Name3);

                        if (mainInput3 == null)
                        {
                            // the input value is not in the gazetteer,
                            // however if input and gazetteer value is the same, don't save
                            if (AreNotTheSame(
                                match.InputLocation.Name3,
                                match.GazetteerLocation.Name3))
                            {
                                SaveMatchLevel3(
                                    inputLocation.Name3,
                                    gazetteerLocation.Name1,
                                    gazetteerLocation.Name2,
                                    gazetteerLocation.Name3);
                            }
                        }
                        else
                        {
                            // The input value is in the gazetteer,
                            if (AreNotTheSame(
                                match.InputLocation.Name3,
                                match.GazetteerLocation.Name3))
                            {
                                // We do not allow saved matches for existing names as this
                                // would result in conflicting information so throw ex if the input
                                // is not the same as the selection.
                                string message = string.Format(
                                    "Input name is already in the gazetteer, cannot save match: {0} ",
                                    match.InputLocation.Name3);
                                throw new NameInGazetteerException(message);
                            }

                            // use the main input value for the lower levels
                            match.InputLocation.Name3 = mainInput3;
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


        //todo review wether this is needed
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