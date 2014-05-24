// DbIntegrationTests.cs

namespace MultiLevelGeoCoderTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlServerCe;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Model;

    [TestClass]
    public class DbIntegrationTests
    {
        #region Fields

        private DbConnection connection;
        private string dbLocation = @"GeoLocationToolTest.sdf";
        private int maxLength = 255;

        #endregion Fields

        #region Methods

        [TestCleanup]
        public void Cleanup()
        {
            connection.Close();
        }

        /// <summary>
        /// Given a match is saved for a given input location
        /// with a given casing for the input value
        /// Then the record can be retrieved using an alternative casing
        /// </summary>
        [TestMethod]
        public void GetMatches_InputWithDifferentCase_RecordRetrieved()
        {
            // Arrange
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);
            provider.SaveMatchLevel1("Near", "actual");

            // Act
            List<Level1Match> matchesLowerCase =
                provider.GetMatches("near").ToList();

            // Assert
            Assert.AreEqual(1, matchesLowerCase.Count());
            Assert.AreEqual("Near", matchesLowerCase.Single().AltLevel1);
            Assert.AreEqual("actual", matchesLowerCase.Single().Level1);
        }

        [TestMethod]
        public void InsertAndGetFromFreshDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);

            //When
            provider.SaveMatchLevel1("near", "actual");
            var matches = provider.GetMatches("near").ToList();

            //Then
            Assert.AreEqual(1, matches.Count());
            Assert.IsTrue(matches.All(x => x.AltLevel1 == "near"));
        }

        [TestMethod]
        public void InsertAndGetLevel2FromFreshDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);

            //When
            provider.SaveMatchLevel2("near", "level1", "level2");
            var matches = provider.GetMatches("near", "level1");

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual("level2", match.Level2);
        }

        [TestMethod]
        public void InsertAndGetLevel3FromFreshDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);

            //When
            provider.SaveMatchLevel3("near", "level1", "level2", "level3");
            var matches = provider.GetMatches("near", "level1", "level2");

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual("level3", match.Level3);
        }

        [TestMethod]
        public void InsertMaxLengthMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);
            string veryLong = new String('a', maxLength);

            //When
            provider.SaveMatchLevel1(veryLong, veryLong);
            var matches = provider.GetMatches(veryLong);

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual(veryLong, match.AltLevel1);
            Assert.AreEqual(veryLong, match.Level1);
        }

        [TestMethod]
        public void InsertSpecialCharactersMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);
            string specialCharacters =
                "%ùéèôçà六书/六書形声字/形聲字абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            int length = specialCharacters.Length;

            //When
            provider.SaveMatchLevel1(specialCharacters, specialCharacters);
            var matches = provider.GetMatches(specialCharacters);

            //Then
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual(specialCharacters.Length, match.AltLevel1.Length);
            Assert.AreEqual(specialCharacters, match.AltLevel1);
            Assert.AreEqual(specialCharacters, match.Level1);
        }

        [TestMethod]
        public void InsertToFreshDBAndGetFromDeletedDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);

            //When
            provider.SaveMatchLevel1("near", "actual");
            provider.SaveMatchLevel1("near", "actual3");
            connection.Close();
            File.Delete(dbLocation);

            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            provider = new MatchProvider(connection);
            var matches = provider.GetMatches("near");
            connection.Close();

            //Then
            Assert.AreEqual(0, matches.Count());
        }

        [TestMethod]
        public void InsertToFreshDBAndGetFromExistingDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);

            //When
            provider.SaveMatchLevel1("near", "actual");
            connection.Close();

            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            provider = new MatchProvider(connection);
            var matches = provider.GetMatches("near").ToList();
            connection.Close();

            //Then
            Assert.AreEqual(1, matches.Count());
            Assert.IsTrue(matches.All(x => x.Level1 == "actual"));
        }

        [TestMethod]
        [ExpectedException(typeof (SqlCeException))]
        public void InsertTooLongMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);
            string tooLong = new String('a', maxLength + 1);

            //When
            provider.SaveMatchLevel1(tooLong, tooLong);

            var matches = provider.GetMatches(tooLong);

            //Then exception
        }

        //[TestMethod]
        //public void HighestWeightFirst()
        //{
        //    //Given
        //    connection = DBHelper.GetDbConnection(dbLocation);
        //    connection.InitializeDB();
        //    INearMatchesProvider provider = new NearMatchesProvider(connection);
        //    //When
        //    provider.SaveMatchLevel1("near", "actual");
        //    provider.SaveMatchLevel1("near", "actual2");
        //    provider.SaveMatchLevel1("near", "actual2");
        //    var matches = provider.GetActualMatches("near");
        //    //Then
        //    Assert.AreEqual(2, matches.Count());
        //    var match1 = matches.First();
        //    var match2 = matches.Last();
        //    Assert.AreEqual("near", match1.AltLevel1);
        //    Assert.AreEqual("actual2", match1.Level1);
        //    Assert.AreEqual(2, match1.Weight);
        //    Assert.AreEqual("near", match2.AltLevel1);
        //    Assert.AreEqual("actual", match2.Level1);
        //    Assert.AreEqual(1, match2.Weight);
        //}
        /// <summary>
        /// Given a match is saved for a given input location
        /// When an existing match already exists for that location
        /// Then the existing match is overwriten
        /// </summary>
        [TestMethod]
        public void SaveMatchLevel1_InsertDuplicateMatch_RecordOverwritten()
        {
            // Arrange
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);

            // Act
            provider.SaveMatchLevel1("input", "match_x");
            provider.SaveMatchLevel1("input", "match_y");
            IEnumerable<Level1Match> matches = provider.GetMatches("input").ToList();

            // Asset
            Assert.AreEqual(1, matches.Count());
            var match = matches.Single();
            Assert.AreEqual("input", match.AltLevel1);
            Assert.AreEqual("match_y", match.Level1);
        }

        /// <summary>
        /// Given a match is saved for a given input location
        /// When an existing match already exists for that location 
        /// with a different casing  for the input value
        /// Then the existing match is overwritten
        /// </summary>
        [TestMethod]
        public void SaveMatchLevel1_InsertWithDifferentCase_RecordOverwritten()
        {
            // Arrange
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            IMatchProvider provider = new MatchProvider(connection);

            // Act
            provider.SaveMatchLevel1("near", "actual");
            provider.SaveMatchLevel1("Near", "actual");
            List<Level1Match> matchesUpperCase =
                provider.GetMatches("NEAR").ToList();
            List<Level1Match> matchesLowerCase =
                provider.GetMatches("near").ToList();

            // Assert
            Assert.AreEqual(1, matchesUpperCase.Count());
            Assert.AreEqual(1, matchesLowerCase.Count());
            Assert.AreEqual(
                matchesUpperCase.First().MatchId,
                matchesLowerCase.First().MatchId);
        }

        [TestInitialize]
        public void Setup()
        {
            if (File.Exists(dbLocation))
                File.Delete(dbLocation);
        }

        #endregion Methods
    }
}