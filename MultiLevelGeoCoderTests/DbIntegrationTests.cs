// DbIntegrationTests.cs

namespace MultiLevelGeoCoderTests
{
    using System;
    using System.Data.Common;
    using System.Data.SqlServerCe;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.DataAccess;

    [TestClass]
    public class DbIntegrationTests
    {
        private string dbLocation = @"GeoLocationToolTest.sdf";
        private DbConnection connection;
        private int maxLength = 255;

        [TestInitialize]
        public void Setup()
        {
            if (File.Exists(dbLocation))
                File.Delete(dbLocation);
        }

        [TestCleanup]
        public void Cleanup()
        {
            connection.Close();
        }

        [TestMethod]
        public void InsertAndGetFromFreshDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.SaveMatch("near", "actual");
            provider.SaveMatch("near", "actual2");
            var matches = provider.GetActualMatches("near");

            //Then
            Assert.AreEqual(2, matches.Count());
            Assert.IsTrue(matches.All(x => x.NearMatch == "near"));
        }

        [TestMethod]
        public void InsertDuplicatedMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.SaveMatch("near", "actual");
            provider.SaveMatch("near", "actual");
            var matches = provider.GetActualMatches("near");

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual("near", match.NearMatch);
            Assert.AreEqual("actual", match.Location1);
            Assert.AreEqual(2, match.Weight);
        }

        [TestMethod]
        public void InsertMaxLengthMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);
            string veryLong = new String('a', maxLength);

            //When
            provider.SaveMatch(veryLong, veryLong);
            var matches = provider.GetActualMatches(veryLong);

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual(veryLong, match.NearMatch);
            Assert.AreEqual(veryLong, match.Location1);
        }

        [TestMethod]
        [ExpectedException(typeof (SqlCeException))]
        public void InsertTooLongMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);
            string tooLong = new String('a', maxLength + 1);

            //When
            provider.SaveMatch(tooLong, tooLong);

            var matches = provider.GetActualMatches(tooLong);

            //Then exception
        }

        [TestMethod]
        public void InsertSpecialCharactersMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);
            string specialCharacters =
                "%ùéèôçà六书/六書形声字/形聲字абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            int length = specialCharacters.Length;

            //When
            provider.SaveMatch(specialCharacters, specialCharacters);
            var matches = provider.GetActualMatches(specialCharacters);

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual(specialCharacters.Length, match.NearMatch.Length);
            Assert.AreEqual(specialCharacters, match.NearMatch);
            Assert.AreEqual(specialCharacters, match.Location1);
        }

        [TestMethod]
        public void InsertToFreshDBAndGetFromExistingDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.SaveMatch("near", "actual");
            provider.SaveMatch("near", "actual3");
            connection.Close();

            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            provider = new NearMatchesProvider(connection);
            var matches = provider.GetActualMatches("near");
            connection.Close();

            //Then
            Assert.AreEqual(2, matches.Count());
            Assert.IsTrue(matches.All(x => x.NearMatch == "near"));
        }

        [TestMethod]
        public void InsertToFreshDBAndGetFromDeletedDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.SaveMatch("near", "actual");
            provider.SaveMatch("near", "actual3");
            connection.Close();
            File.Delete(dbLocation);

            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            provider = new NearMatchesProvider(connection);
            var matches = provider.GetActualMatches("near");
            connection.Close();

            //Then
            Assert.AreEqual(0, matches.Count());
        }

        [TestMethod]
        public void InsertAndGetLevel2FromFreshDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.SaveMatch("near", "location1", "location2");
            var matches = provider.GetActualMatches("near", "location1");

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual("location2", match.Location2);
        }

        [TestMethod]
        public void InsertAndGetLevel3FromFreshDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.SaveMatch("near", "location1", "location2", "location3");
            var matches = provider.GetActualMatches("near", "location1", "location2");

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual("location3", match.Location3);
            Assert.AreEqual(1, match.Weight);
        }
    }
}