// MatchProviderTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;
    using MultiLevelGeoCoder.Model;
    using Rhino.Mocks;

    /// <summary>
    /// Provides Match Provider stubs containing name matches
    /// </summary>
    internal class MatchProviderTestData
    {
        #region Fields

        private readonly List<Level1Match> level1Matches = new List<Level1Match>();
        private readonly List<Level2Match> level2Matches = new List<Level2Match>();
        private readonly List<Level3Match> level3Matches = new List<Level3Match>();

        #endregion Fields

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

        public void AddLevel1(string[] match, string[] actual)
        {
            // todo refactor the Match Provider to seperate the logic from the data access
            // todo remove this logic from the test
            var matched = level1Matches.FirstOrDefault(x => x.AltLevel1 == match[0]);
            if (matched !=null)
            {
                level1Matches.Remove(matched);
            }
            Level1Match level1Match = new Level1Match();
            level1Match.Level1 = actual[0];
            level1Match.AltLevel1 = match[0];
            level1Matches.Add(level1Match);
        }

        public void AddLevel2(string[] match, string[] actual)
        {
            Level2Match level2Match = new Level2Match();
            level2Match.Level1 = actual[0];
            level2Match.Level2 = actual[1];
            level2Match.AltLevel2 = match[1];
            level2Matches.Add(level2Match);
        }

        public void AddLevel3(string[] match, string[] actual)
        {
            Level3Match level3Match = new Level3Match();
            level3Match.Level1 = actual[0];
            level3Match.Level2 = actual[1];
            level3Match.Level3 = actual[2];
            level3Match.AltLevel3 = match[2];
            level3Matches.Add(level3Match);
        }

        public IMatchProvider Data()
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();
            matchStub.Stub(x => x.GetAllLevel1())
                .Return(AllLevel1());
            matchStub.Stub(
                x => x.GetAllLevel2())
                .Return(AllLevel2());
            matchStub.Stub(
                x =>
                    x.GetAllLevel3())
                .Return(AllLevel3());
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

        private IEnumerable<Level1Match> AllLevel1()
        {
            return level1Matches;
        }

        private IEnumerable<Level2Match> AllLevel2()
        {
            return level2Matches;
        }

        private IEnumerable<Level3Match> AllLevel3()
        {
            return level3Matches;
        }

        #endregion Methods
    }
}