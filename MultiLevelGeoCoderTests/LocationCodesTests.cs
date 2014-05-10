// LocationCodesTests.cs

namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Exercises the LocationCodes class, in particular it tests that the 
    /// correct codes are generated for a given location using only the gazetteer data
    /// </summary>
    [TestClass]
    public class LocationCodesTests
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
            // gazetteer data - contains codes for names1 and names2
            GazetteerRecords gazetteerRecords = GazetteerTestData();

            // input data - level 3 miss-spelt
            string[] inputNames = {"P1", "T1", "V1x"};
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
            // code 1 and 2 only are added, no level 3 code added
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
            Assert.AreEqual(codes1[1], codedLocation.GeoCode2.Code);
            Assert.AreEqual(names1[1], codedLocation.GeoCode2.Name);
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
            // gazetteer data - contains codes for names1 and names2
            GazetteerRecords gazetteerRecords = GazetteerTestData();

            // input data - level 2 miss-spelt
            string[] inputNames = {"P1", "T1x", "V1"};
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
            //  level 1 codes added only
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
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
            // gazetteer data - contains codes for names1 and names2
            GazetteerRecords gazetteerRecords = GazetteerTestData();

            // input data - all spelt correctly
            string[] inputNames = {"P1", "T1", "V1"};
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
        /// Given a location containing only level 1 and 2 names
        /// when both the level 1 and 2 names are correct 
        /// then the level 1 and 2 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1and2Correct_Level1And2CodeAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2
            GazetteerRecords gazetteerRecords = GazetteerTestData();

            // input data - no level 3 supplied
            string[] inputNames = {"P1", "T1", null};
            Location location = new Location(
                inputNames[0],
                inputNames[1]);

            // no saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubEmpty(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerRecords.GadmList(),
                matchProviderStub.MatchProvider());

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // level 1 and 2 codes added, no level 3
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
            Assert.AreEqual(codes1[1], codedLocation.GeoCode2.Code);
            Assert.AreEqual(names1[1], codedLocation.GeoCode2.Name);
            Assert.AreEqual(null, codedLocation.GeoCode3);
        }

        /// <summary>
        /// Given a location containing only a level 1 name
        /// when the level 1 name is correct 
        /// then the level 1 code only is added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1Correct_Level1CodeAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2
            GazetteerRecords gazetteerRecords = GazetteerTestData();

            // input data - no level 3 supplied
            string[] inputNames = {"P1", null, null};
            Location location = new Location(
                inputNames[0]);

            // no saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubEmpty(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerRecords.GadmList(),
                matchProviderStub.MatchProvider());

            // Act
            CodedLocation codedLocation = locationCodes.GetCodes(location);

            // Assert
            // level 1 code only added
            Assert.AreEqual(codes1[0], codedLocation.GeoCode1.Code);
            Assert.AreEqual(names1[0], codedLocation.GeoCode1.Name);
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
            // gazetteer data - contains codes for names1 and names2
            GazetteerRecords gazetteerRecords = GazetteerTestData();

            // input data - level 2 miss-spelt
            string[] inputNames = {"P1x", "T1", "V1"};
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
            // no codes added
            Assert.AreEqual(null, codedLocation.GeoCode1);
            Assert.AreEqual(null, codedLocation.GeoCode2);
            Assert.AreEqual(null, codedLocation.GeoCode3);
        }

        private GazetteerRecords GazetteerTestData()
        {
            GazetteerRecords gazetteerRecords = new GazetteerRecords();
            gazetteerRecords.AddLine(names1, codes1);
            gazetteerRecords.AddLine(names2, codes2);
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