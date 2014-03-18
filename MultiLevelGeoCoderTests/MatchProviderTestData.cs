// NearMatchProviderTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Model;
    using Rhino.Mocks;

    /// <summary>
    /// Provided Near Match Provider stubs returning gazetteer test data
    /// </summary>
    internal static class MatchProviderTestData
    {
        #region Methods

        public static IMatchProvider NearMatchesProviderLevel1(string altName)
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();
            matchStub.Stub(x => x.GetMatches(altName))
                .Return(Level1List(altName));
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

        public static IMatchProvider NearMatchesProviderLevel2(string altName)
        {
            IMatchProvider matchStub =
                MockRepository.GenerateStub<IMatchProvider>();
            matchStub.Stub(
                x => x.GetMatches(
                    Arg<string>.Is.Anything))
                .Return(new List<Level1Match>()); // empty list
            matchStub.Stub(
                x => x.GetMatches(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                .Return(Level2List(altName));
            matchStub.Stub(
                x =>
                    x.GetMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3Match>()); // empty list
            return matchStub;
        }

        public static IMatchProvider NearMatchesProviderLevel3(string altName)
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
                        altName,
                        GazetteerTestData.name1,
                        GazetteerTestData.name2))
                .Return(Level3List(altName));
            return matchStub;
        }

        public static IMatchProvider NearMatchesProviderWithNoRecords()
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

        private static IEnumerable<Level1Match> Level1List(string altName)
        {
            List<Level1Match> list = new List<Level1Match>();
            Level1Match record = new Level1Match();
            record.Level1 = GazetteerTestData.name1;
            record.AltLevel1 = altName;
            list.Add(record);

            return list;
        }

        private static IEnumerable<Level2Match> Level2List(string altName)
        {
            List<Level2Match> list = new List<Level2Match>();
            Level2Match record = new Level2Match();
            record.Level1 = GazetteerTestData.name1;
            record.Level2 = GazetteerTestData.name2;
            record.AltLevel2 = altName;
            list.Add(record);

            return list;
        }

        private static IEnumerable<Level3Match> Level3List(string altName)
        {
            List<Level3Match> list = new List<Level3Match>();
            Level3Match record = new Level3Match();
            record.Level1 = GazetteerTestData.name1;
            record.Level2 = GazetteerTestData.name2;
            record.Level3 = GazetteerTestData.name3;
            record.AltLevel3 = altName;
            list.Add(record);

            return list;
        }

        #endregion Methods
    }
}