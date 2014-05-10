// LocationCodesTestsWithGazetteerAltNames.cs

namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Exercises the LocationCodes class, in particular it tests that the 
    /// correct codes are generated for a given location using only the gazetteer data
    /// and where that gazetteer data contains columns for alternate names
    /// </summary>
    [TestClass]
    public class LocationCodesTestsWithGazetteerAltNames
    {
        #region Fields

        //correct data
        private readonly string[] codes1 = {"1", "10", "100"};
        private readonly string[] codes2 = {"2", "20", "200"};
        private readonly string[] names1 = {"P1", "T1", "V1"};
        private readonly string[] names2 = {"P2", "T2", "V2"};

        #endregion Fields

        #region Methods

        /// <summary>
        /// Given a location containing an alternative level 1 and 2 names and
        /// correct level 3 name
        /// when the gazeteer contains the alternate names for level 1 and 2
        /// then all codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1And2GazetteerAltName_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2 and
            // an alternate name for name 1 and 2
            string[] altNames = {"P1A", "T1A", null};
            GazetteerRecords gazetteerRecords = GazetteerTestData(altNames);
            //
            gazetteerRecords.AddLine(names2, codes2);

            // input data - Level 1 and 2 contains alt spelling, the rest are spelt correctly
            string[] inputNames = {"P1A", "T1A", "V1"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // no saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubEmpty(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerRecords.GadmList(),
                matchProviderStub.MatchProvider());

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
            Assert.AreEqual(codes1[1], codedLocation.GeoCode2.Code);
            Assert.AreEqual(names1[1], codedLocation.GeoCode2.Name);
            Assert.AreEqual(codes1[2], codedLocation.GeoCode3.Code);
            Assert.AreEqual(names1[2], codedLocation.GeoCode3.Name);
        }

        /// <summary>
        /// Given a location containing an alternative level 1 name and
        /// correct level 2 and 3 names
        /// when the gazeteer contains the alternate name for level1
        /// then all codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1GazetteerAltName_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2 and
            // an alternate name for name1
            string[] altNames = {"P1A", null, null};
            GazetteerRecords gazetteerRecords = GazetteerTestData(altNames);
            //
            gazetteerRecords.AddLine(names2, codes2);

            // input data - Level1 contains alt spelling, the rest are spelt correctly
            string[] inputNames = {"P1A", "T1", "V1"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // no saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubEmpty(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerRecords.GadmList(),
                matchProviderStub.MatchProvider());

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
            Assert.AreEqual(codes1[1], codedLocation.GeoCode2.Code);
            Assert.AreEqual(names1[1], codedLocation.GeoCode2.Name);
            Assert.AreEqual(codes1[2], codedLocation.GeoCode3.Code);
            Assert.AreEqual(names1[2], codedLocation.GeoCode3.Name);
        }

        /// <summary>
        /// Given a location containing an alternative level 2 and 3 names and
        /// correct level 1 name
        /// when the gazeteer contains the alternate names for level 2 and 3
        /// then all codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level2And3GazetteerAltName_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2 and
            // alternate names for name 1 and 2
            string[] altNames = {null, "T1A", "V1A"};
            GazetteerRecords gazetteerRecords = GazetteerTestData(altNames);
            gazetteerRecords.AddLine(names2, codes2);

            // input data - Level 2 and 3 contains alt spelling, the rest are spelt correctly
            string[] inputNames = {"P1", "T1A", "V1A"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // no saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubEmpty(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerRecords.GadmList(),
                matchProviderStub.MatchProvider());

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
            Assert.AreEqual(codes1[1], codedLocation.GeoCode2.Code);
            Assert.AreEqual(names1[1], codedLocation.GeoCode2.Name);
            Assert.AreEqual(codes1[2], codedLocation.GeoCode3.Code);
            Assert.AreEqual(names1[2], codedLocation.GeoCode3.Name);
        }

        /// <summary>
        /// Given a location containing an alternative level 2 name and
        /// correct level 1 and 3 names
        /// when the gazeteer contains the alternate name for level 2
        /// then all codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level2GazetteerAltName_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2 and
            // an alternate name for name 2
            string[] altNames = {null, "T1A", null};
            GazetteerRecords gazetteerRecords = GazetteerTestData(altNames);
            //
            gazetteerRecords.AddLine(names2, codes2);

            // input data - Level 2 contains alt spelling, the rest are spelt correctly
            string[] inputNames = {"P1", "T1A", "V1"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // no saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubEmpty(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerRecords.GadmList(),
                matchProviderStub.MatchProvider());

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
            Assert.AreEqual(codes1[1], codedLocation.GeoCode2.Code);
            Assert.AreEqual(names1[1], codedLocation.GeoCode2.Name);
            Assert.AreEqual(codes1[2], codedLocation.GeoCode3.Code);
            Assert.AreEqual(names1[2], codedLocation.GeoCode3.Name);
        }

        /// <summary>
        /// Given a location containing an alternative level 3 name and
        /// correct level 1 and 2 names
        /// when the gazeteer contains the alternate name for level 3
        /// then all codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level3GazetteerAltName_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2 and
            // an alternate name for name 3
            string[] altNames = {null, null, "V1A"};
            GazetteerRecords gazetteerRecords = GazetteerTestData(altNames);
            //
            gazetteerRecords.AddLine(names2, codes2);

            // input data - Level3 contains alt spelling, the rest are spelt correctly
            string[] inputNames = {"P1", "T1", "V1A"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // no saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubEmpty(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerRecords.GadmList(),
                matchProviderStub.MatchProvider());

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // code 1, 2 and 3 codes added
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
            Assert.AreEqual(codes1[1], codedLocation.GeoCode2.Code);
            Assert.AreEqual(names1[1], codedLocation.GeoCode2.Name);
            Assert.AreEqual(codes1[2], codedLocation.GeoCode3.Code);
            Assert.AreEqual(names1[2], codedLocation.GeoCode3.Name);
        }

        private GazetteerRecords GazetteerTestData(string[] altNames)
        {
            GazetteerRecords gazetteerRecords = new GazetteerRecords();
            gazetteerRecords.AddLine(names1, codes1, altNames);
            gazetteerRecords.AddLine(names2, codes2, altNames);
            return gazetteerRecords;
        }

        private MatchProviderStub MatchProviderStubEmpty(string[] inputNames)
        {
            // database contains no saved records
            MatchProviderTestData matchProviderTestData = new MatchProviderTestData();
            MatchProviderStub matchProviderStub =
                new MatchProviderStub(matchProviderTestData);
            matchProviderStub.Alternate = inputNames;
            matchProviderStub.Actual = names1;
            return matchProviderStub;
        }

        #endregion Methods
    }
}