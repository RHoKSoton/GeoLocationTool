// MatchProviderTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;
    using MultiLevelGeoCoder.Model;
    using Rhino.Mocks;

    /// <summary>
    /// Provided Near Match Provider stubs returning gazetteer test data
    /// </summary>
    internal static class MatchProviderTestData
    {
        #region Methods

        public static IMatchProvider MatchProviderLevel1(string matchedName, Gadm gadm)
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();
            matchStub.Stub(x => x.GetMatches(matchedName))
                .Return(Level1List(gadm, matchedName));
            matchStub.Stub(
                x => x.GetMatches(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything))
                .Return(new List<Level2Match>()); // empty list
            matchStub.Stub(
                x =>
                    x.GetMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3Match>()); // empty list
            return matchStub;
        }

        public static IMatchProvider MatchProviderLevel2(string matchedName, Gadm gadm)
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();
            matchStub.Stub(
                x => x.GetMatches(
                    Arg<string>.Is.Anything))
                .Return(new List<Level1Match>()); // empty list
            matchStub.Stub(
                x => x.GetMatches(matchedName, gadm.NAME_1))
                .Return(Level2List(gadm, matchedName));
            matchStub.Stub(
                x =>
                    x.GetMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3Match>()); // empty list
            return matchStub;
        }

        public static IMatchProvider MatchProviderLevel3(string matchedName, Gadm gadm)
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();
            matchStub.Stub(
                x => x.GetMatches(
                    Arg<string>.Is.Anything))
                .Return(new List<Level1Match>()); // empty list
            matchStub.Stub(
                x => x.GetMatches(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything))
                .Return(new List<Level2Match>()); // empty list
            matchStub.Stub(
                x =>
                    x.GetMatches(
                        matchedName,
                        gadm.NAME_1,
                        gadm.NAME_2))
                .Return(Level3List(gadm, matchedName));
            return matchStub;
        }

        public static IMatchProvider MatchProviderWithNoRecords()
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();
            matchStub.Stub(x => x.GetMatches(Arg<string>.Is.Anything))
                .Return(new List<Level1Match>()); // empty list
            matchStub.Stub(
                x => x.GetMatches(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                .Return(new List<Level2Match>()); // empty list
            matchStub.Stub(
                x =>
                    x.GetMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3Match>()); // empty list
            return matchStub;
        }

        private static IEnumerable<Level1Match> Level1List(Gadm gadm, string matchedName)
        {
            List<Level1Match> list = new List<Level1Match>();
            Level1Match record = new Level1Match();
            record.Level1 = gadm.NAME_1;
            record.AltLevel1 = matchedName;
            list.Add(record);

            return list;
        }

        private static IEnumerable<Level2Match> Level2List(Gadm gadm, string matchedName)
        {
            List<Level2Match> list = new List<Level2Match>();
            Level2Match record = new Level2Match();
            record.Level1 = gadm.NAME_1;
            record.Level2 = gadm.NAME_2;
            record.AltLevel2 = matchedName;
            list.Add(record);

            return list;
        }

        private static IEnumerable<Level3Match> Level3List(Gadm gadm, string matchedName)
        {
            List<Level3Match> list = new List<Level3Match>();
            Level3Match record = new Level3Match();
            record.Level1 = gadm.NAME_1;
            record.Level2 = gadm.NAME_2;
            record.Level3 = gadm.NAME_3;
            record.AltLevel3 = matchedName;
            list.Add(record);

            return list;
        }

        #endregion Methods
    }
}