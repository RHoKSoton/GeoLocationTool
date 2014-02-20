using GeoLocationTool.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

namespace GeoLocationToolTests
{
    [TestClass]
    public class DbIntegrationTests
    {
        private string dbLocation = @"GeoLocationTool.sdf";
        private DbConnection connection;

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
            provider.InsertMatch("near", "actual");
            provider.InsertMatch("near", "actual2");
            var matches = provider.GetActualMatches("near");

            //Then
            Assert.AreEqual(2, matches.Count());
            Assert.IsTrue(matches.All(x => x.Near == "near"));
        }

        [TestMethod]
        public void InsertDuplicatedMatch()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.InsertMatch("near", "actual");
            provider.InsertMatch("near", "actual");
            var matches = provider.GetActualMatches("near");

            //Then
            Assert.AreEqual(2, matches.Count());
            Assert.IsTrue(matches.All(x => x.Near == "near"));
            Assert.IsTrue(matches.All(x => x.Actual == "actual"));
        }

        [TestMethod]
        public void InsertToFreshDBAndGetFromExistingDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.InsertMatch("near", "actual");
            provider.InsertMatch("near", "actual3");
            connection.Close();

            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            provider = new NearMatchesProvider(connection);
            var matches = provider.GetActualMatches("near");
            connection.Close();

            //Then
            Assert.AreEqual(2, matches.Count());
            Assert.IsTrue(matches.All(x => x.Near == "near"));
        }

        [TestMethod]
        public void InsertToFreshDBAndGetFromDeletedDB()
        {
            //Given
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            INearMatchesProvider provider = new NearMatchesProvider(connection);

            //When
            provider.InsertMatch("near", "actual");
            provider.InsertMatch("near", "actual3");
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
    }
}
