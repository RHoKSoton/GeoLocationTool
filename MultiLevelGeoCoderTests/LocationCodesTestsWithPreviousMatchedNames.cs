// LocationCodesTestsWithPreviousMatchedNames.cs

namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Exercises the LocationCodes class, in particular it tests that the 
    /// correct codes are applied to a given location using the gazetteer data
    /// and existing matched names
    /// </summary>
    [TestClass]
    public class LocationCodesTestsWithPreviousMatchedNames
    {
        #region Methods

        /// <summary>
        /// Given a location with level 1, 2 and 3 names present 
        /// when level 1 and 2 names are correct but level 3 is not
        /// and there is a previous match for the level 3 name
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Leve1And2CorrectAndLevel3Incorrect_AllCodesAdded()
        {
            // Arrange
            // Create location input, containing three levels,
            // correct level 1 and 2, level 3 is incorrect (i.e. no record in the gazetteer)
            Location location = new Location(
                GazetteerTestData.name1,
                GazetteerTestData.name2,
                "SomeName");

            // database contains saved record for level 3 alternate name
            IMatchProvider matchProviderWithNoRecords =
                NearMatchProviderTestData.NearMatchesProviderLevel3("SomeName");

            // create gazetteer data to match against
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(GazetteerTestData.code1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(GazetteerTestData.name1, codedLocation.GeoCode1.Name);
            Assert.AreEqual(GazetteerTestData.code2, codedLocation.GeoCode2.Code);
            Assert.AreEqual(GazetteerTestData.name2, codedLocation.GeoCode2.Name);
            Assert.AreEqual(GazetteerTestData.code3, codedLocation.GeoCode3.Code);
            Assert.AreEqual(GazetteerTestData.name3, codedLocation.GeoCode3.Name);
        }

        /// <summary>
        /// Given a location with level 1, 2 and 3 names present
        /// when level 1 and 3 names are correct but level 2 is not
        /// and there is a previous match for the level 2 name
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Leve1And3CorrectAndLevel2Incorrect_AllCodesAdded()
        {
            // Arrange
            // create location input, containing correct level 1 and 3,
            // level 2 is incorrect (i.e. not in the gazetteer)
            Location location = new Location(
                GazetteerTestData.name1,
                "SomeName",
                GazetteerTestData.name3);

            // database contains saved record
            IMatchProvider matchProviderWithNoRecords =
                NearMatchProviderTestData.NearMatchesProviderLevel2("SomeName");

            // create gazetteer data to match against
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(GazetteerTestData.code1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(GazetteerTestData.name1, codedLocation.GeoCode1.Name);
            Assert.AreEqual(GazetteerTestData.code2, codedLocation.GeoCode2.Code);
            Assert.AreEqual(GazetteerTestData.name2, codedLocation.GeoCode2.Name);
            Assert.AreEqual(GazetteerTestData.code3, codedLocation.GeoCode3.Code);
            Assert.AreEqual(GazetteerTestData.name3, codedLocation.GeoCode3.Name);
        }

        /// <summary>
        /// Given a location with level 1, 2 and 3 names 
        /// when level 1 name is incorrect
        /// and there is a previous match for the level 1 name
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1Incorrect_AllCodesAdded()
        {
            // Arrange
            // Create location input, containing incorrect level 1 (i.e. not in the gazetteer)
            // level  2 and 3 are correct
            Location location = new Location(
                "SomeName",
                GazetteerTestData.name2,
                GazetteerTestData.name3);

            // database contains saved record
            IMatchProvider matchProviderWithNoRecords =
                NearMatchProviderTestData.NearMatchesProviderLevel1("SomeName");

            // create gazetteer data to match against - add a record
            // containing a match to the location at level 2 and 3 only
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(GazetteerTestData.code1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(GazetteerTestData.name1, codedLocation.GeoCode1.Name);
            Assert.AreEqual(GazetteerTestData.code2, codedLocation.GeoCode2.Code);
            Assert.AreEqual(GazetteerTestData.name2, codedLocation.GeoCode2.Name);
            Assert.AreEqual(GazetteerTestData.code3, codedLocation.GeoCode3.Code);
            Assert.AreEqual(GazetteerTestData.name3, codedLocation.GeoCode3.Name);
        }

        #endregion Methods
    }
}