// LocationCodesTests.cs

namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Exercises the LocationCodes class, in particular it tests that the 
    /// correct codes are generated for a given location using only the gazetteer data
    /// </summary>
    [TestClass]
    public class LocationCodesTests
    {
        #region Methods

        /// <summary>
        /// Given a location with level 1, 2 and 3 names present 
        /// when level 1 and 2 names are correct but level 3 is not
        /// and there are no previous saved name matches
        /// then only level 1 and 2 codes are added
        /// </summary>
        [TestMethod]
        public void
            GetLocationCodes_Leve1And2CorrectAndLevel3Incorrect_Level1And2CodeAddedOnly()
        {
            // Arrange
            // Create location input, containing three levels,
            // correct level 1 and 2, level 3 is incorrect (i.e. no record in the gazetteer)
            Location location = new Location(
                GazetteerTestData.Record1().NAME_1,
                GazetteerTestData.Record1().NAME_2,
                "SomeName");

            // database contains no saved records
            IMatchProvider matchProviderWithNoRecords =
                MatchProviderTestData.MatchProviderWithNoRecords();

            // create gazetteer data to match against
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1 and 2 only are added, no level 3 code added
            Assert.AreEqual(GazetteerTestData.Record1().ID_1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(
                GazetteerTestData.Record1().NAME_1,
                codedLocation.GeoCode1.Name);
            Assert.AreEqual(GazetteerTestData.Record1().ID_2, codedLocation.GeoCode2.Code);
            Assert.AreEqual(
                GazetteerTestData.Record1().NAME_2,
                codedLocation.GeoCode2.Name);
            Assert.AreEqual(null, codedLocation.GeoCode3);
        }

        /// <summary>
        /// Given a location with level 1, 2 and 3 names present
        /// when level 1 and 3 names are correct but level 2 is not
        /// and there are no previous saved name matches
        /// then only a level 1 code is added
        /// </summary>
        [TestMethod]
        public void
            GetLocationCodes_Leve1And3CorrectAndLevel2Incorrect_Level1CodeAddedOnly()
        {
            // Arrange
            // create location input, containing correct level 1 and 3,
            // level 2 is incorrect (i.e. not in the gazetteer)
            Gadm record1 = GazetteerTestData.Record1();
            Location location = new Location(
                record1.NAME_1,
                "SomeName",
                record1.NAME_3);

            // database contains no saved records
            IMatchProvider matchProviderWithNoRecords =
                MatchProviderTestData.MatchProviderWithNoRecords();

            // create gazetteer data to match against
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            //  level 1 codes added only
            Assert.AreEqual(record1.ID_1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(record1.NAME_1, codedLocation.GeoCode1.Name);
            Assert.AreEqual(null, codedLocation.GeoCode2);
            Assert.AreEqual(null, codedLocation.GeoCode3);
        }

        /// <summary>
        /// Given a location containing level 1, 2 and 3 names
        /// when all the level 1, 2 and 3 names are correct (case insensitive)
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1And2And3Correct_Level1And2And3CodesAdded()
        {
            // Arrange
            // Create location input, containing correct level 1, 2 and
            // level 3 (i.e. all are in the gazetteer)
            Gadm record1 = GazetteerTestData.Record1();
            Location location = new Location(
                record1.NAME_1,
                record1.NAME_2,
                record1.NAME_3.ToLower());

            // database contains no saved records
            IMatchProvider matchProviderWithNoRecords =
                MatchProviderTestData.MatchProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location
            var gazzetteerData = GazetteerTestData.TestData(record1);

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // all codes added
            Assert.AreEqual(record1.ID_1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(record1.NAME_1, codedLocation.GeoCode1.Name);
            Assert.AreEqual(record1.ID_2, codedLocation.GeoCode2.Code);
            Assert.AreEqual(record1.NAME_2, codedLocation.GeoCode2.Name);
            Assert.AreEqual(record1.ID_3, codedLocation.GeoCode3.Code);
            Assert.AreEqual(record1.NAME_3, codedLocation.GeoCode3.Name);
        }

        /// <summary>
        /// Given a location containing only level 1 and 2 names
        /// when both the level 1 and 2 names are correct (case insensitive)
        /// then the level 1 and 2 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1and2Correct_Level1And2CodeAdded()
        {
            // Arrange
            // Create location input, containing correct level 1 and 2,(i.e. in the gazetteer)
            // no level 3 supplied
            Gadm record1 = GazetteerTestData.Record1();
            Location location = new Location(
                record1.NAME_1.ToUpper(),
                record1.NAME_2.ToLower());

            // database contains no saved records
            IMatchProvider matchProviderWithNoRecords =
                MatchProviderTestData.MatchProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // level 1 and 2 codes added, no level 3
            Assert.AreEqual(record1.ID_1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(record1.NAME_1, codedLocation.GeoCode1.Name);
            Assert.AreEqual(record1.ID_2, codedLocation.GeoCode2.Code);
            Assert.AreEqual(record1.NAME_2, codedLocation.GeoCode2.Name);
            Assert.AreEqual(null, codedLocation.GeoCode3);
        }

        /// <summary>
        /// Given a location containing only a level 1 name
        /// when the level 1 name is correct (case insensitive)
        /// then the level 1 code  only is added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1Correct_Level1CodeAdded()
        {
            // Arrange
            // Create location input, containing correct level 1,
            // no level 2 or 3 supplied
            Gadm record1 = GazetteerTestData.Record1();
            Location location = new Location(record1.NAME_1.ToLower());

            // database contains no saved records
            IMatchProvider matchProviderWithNoRecords =
                MatchProviderTestData.MatchProviderWithNoRecords();

            // create gazetteer data to match against - adds a record
            // containing a match to the location
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // level 1 code only added
            Assert.AreEqual(record1.ID_1, codedLocation.GeoCode1.Code);
            Assert.AreEqual(record1.NAME_1, codedLocation.GeoCode1.Name);
            Assert.AreEqual(null, codedLocation.GeoCode2);
            Assert.AreEqual(null, codedLocation.GeoCode3);
        }

        /// <summary>
        /// Given a location with level 1, 2 and 3 names 
        /// when level 1 name is incorrect
        /// and there are no previous saved name matches
        /// then no codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1Incorrect_NoCodesAdded()
        {
            // Arrange
            // Create location input, containing incorrect level 1 (i.e. not in the gazetteer)
            // level  2 and 3 are correct
            Gadm record1 = GazetteerTestData.Record1();
            Location location = new Location(
                "SomeName",
                record1.NAME_2,
                record1.NAME_3);

            // database contains no saved records
            IMatchProvider matchProviderWithNoRecords =
                MatchProviderTestData.MatchProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location at level 2 and 3 only
            var gazzetteerData = GazetteerTestData.TestData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                matchProviderWithNoRecords);

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // no codes added
            Assert.AreEqual(null, codedLocation.GeoCode1);
            Assert.AreEqual(null, codedLocation.GeoCode2);
            Assert.AreEqual(null, codedLocation.GeoCode3);
        }

        #endregion Methods
    }
}