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
    internal static class NearMatchProviderTestData
    {
        #region Methods

        public static INearMatchesProvider NearMatchesProviderLevel1(string altName)
        {
            INearMatchesProvider nearMatchesStub =
                MockRepository.GenerateStub<INearMatchesProvider>();
            nearMatchesStub.Stub(x => x.GetActualMatches(altName))
                .Return(Level1List(altName));
            nearMatchesStub.Stub(
                x => x.GetActualMatches(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything))
                .Return(new List<Level2NearMatch>()); // empty list
            nearMatchesStub.Stub(
                x =>
                    x.GetActualMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3NearMatch>()); // empty list
            return nearMatchesStub;
        }

        public static INearMatchesProvider NearMatchesProviderLevel2(string altName)
        {
            INearMatchesProvider nearMatchesStub =
                MockRepository.GenerateStub<INearMatchesProvider>();
            nearMatchesStub.Stub(
                x => x.GetActualMatches(
                    Arg<string>.Is.Anything))
                .Return(new List<Level1NearMatch>()); // empty list
            nearMatchesStub.Stub(
                x => x.GetActualMatches(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                .Return(Level2List(altName));
            nearMatchesStub.Stub(
                x =>
                    x.GetActualMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3NearMatch>()); // empty list
            return nearMatchesStub;
        }

        public static INearMatchesProvider NearMatchesProviderLevel3(string altName)
        {
            INearMatchesProvider nearMatchesStub =
                MockRepository.GenerateStub<INearMatchesProvider>();
            nearMatchesStub.Stub(
                x => x.GetActualMatches(
                    Arg<string>.Is.Anything))
                .Return(new List<Level1NearMatch>()); // empty list
            nearMatchesStub.Stub(
                x => x.GetActualMatches(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything))
                .Return(new List<Level2NearMatch>()); // empty list
            nearMatchesStub.Stub(
                x =>
                    x.GetActualMatches(
                        altName,
                        GazetteerTestData.name1,
                        GazetteerTestData.name2))
                .Return(Level3List(altName));
            return nearMatchesStub;
        }

        public static INearMatchesProvider NearMatchesProviderWithNoRecords()
        {
            INearMatchesProvider nearMatchesStub =
                MockRepository.GenerateStub<INearMatchesProvider>();
            nearMatchesStub.Stub(x => x.GetActualMatches(Arg<string>.Is.Anything))
                .Return(new List<Level1NearMatch>()); // empty list
            nearMatchesStub.Stub(
                x => x.GetActualMatches(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                .Return(new List<Level2NearMatch>()); // empty list
            nearMatchesStub.Stub(
                x =>
                    x.GetActualMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3NearMatch>()); // empty list
            return nearMatchesStub;
        }

        private static IEnumerable<Level1NearMatch> Level1List(string altName)
        {
            List<Level1NearMatch> list = new List<Level1NearMatch>();
            Level1NearMatch record = new Level1NearMatch();
            record.Level1 = GazetteerTestData.name1;
            record.NearMatch = altName;
            list.Add(record);

            return list;
        }

        private static IEnumerable<Level2NearMatch> Level2List(string altName)
        {
            List<Level2NearMatch> list = new List<Level2NearMatch>();
            Level2NearMatch record = new Level2NearMatch();
            record.Level1 = GazetteerTestData.name1;
            record.Level2 = GazetteerTestData.name2;
            record.NearMatch = altName;
            list.Add(record);

            return list;
        }

        private static IEnumerable<Level3NearMatch> Level3List(string altName)
        {
            List<Level3NearMatch> list = new List<Level3NearMatch>();
            Level3NearMatch record = new Level3NearMatch();
            record.Level1 = GazetteerTestData.name1;
            record.Level2 = GazetteerTestData.name2;
            record.Level3 = GazetteerTestData.name3;
            record.NearMatch = altName;
            list.Add(record);

            return list;
        }

        #endregion Methods
    }
}