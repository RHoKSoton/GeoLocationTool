// LocationCodesTestsWithSavedMatchedNames.cs

namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Exercises the LocationCodes class, in particular it tests that the 
    /// correct codes are applied to a given location using the gazetteer data
    /// and existing matched names
    /// </summary>
    [TestClass]
    public class LocationCodesTestsWithSavedMatchedNames
    {
        #region Fields

        private readonly string[] codes1 = {"1", "10", "100"};
        private readonly string[] codes2 = {"2", "20", "200"};

        //correct data
        private readonly string[] names1 = {"P1", "T1", "V1"};
        private readonly string[] names2 = {"P2", "T2", "V2"};

        #endregion Fields

        #region Methods

        /// <summary>
        /// Given a location with level 1, 2 and 3 names present 
        /// when level 1 and 2 names are correct but level 3 is not
        /// and there is a previous match for the level 3 name
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetCodes_Leve1And2CorrectAndLevel3HasSavedMatch_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2
            GazetteerTestData gazetteerTestData = GazetteerTestData();

            // input data - level 3 miss-spelt
            string[] inputNames = {"P1", "T1", "V1x"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubLevel3(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerTestData.GadmList(),
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
        /// Given a location with level 1, 2 and 3 names present
        /// when level 1 and 3 names are correct but level 2 is not correct
        /// (i.e. not in the gazetteer)
        /// and there is a previous match for the level 2 name
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetCodes_Leve1And3CorrectAndLevel2HasSavedMatch_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2
            GazetteerTestData gazetteerTestData = GazetteerTestData();

            // input data - level 2 miss-spelt
            string[] inputNames = {"P1", "T1x", "V1"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            //saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubLevel2(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerTestData.GadmList(),
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
        /// Given a location with level 1, 2 and 3 names 
        /// when level 1 name is incorrect, level 2 and 3 are correct
        /// and there is a previous match for the level 1 name
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetCodes_Level1HasSavedMatchAndLevel2And3AreCorrect_AllCodesAdded(
            )
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2
            GazetteerTestData gazetteerTestData = GazetteerTestData();

            // input data - level 1 miss-spelt
            string[] inputNames = {"P1x", "T1", "V1"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubLevel1(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerTestData.GadmList(),
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
        /// Given a location with level 1, 2 and 3 names 
        /// when level 1 and level2 names are incorrect, level 3 is correct
        /// and there is a previous match for the level 1 and level2 names
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetCodes_Level1And2HaveSavedMatchAndLevel3IsCorrect_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2
            GazetteerTestData gazetteerTestData = GazetteerTestData();

            // input data - level 1 miss-spelt
            string[] inputNames = {"P1x", "T1x", "V1"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubLevel2(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerTestData.GadmList(),
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
        /// Given a location with level 1, 2 and 3 names 
        /// when level 1, 2 and 3 names are incorrect
        /// and there is a previous match for the level 1, level2 and level3 names
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetCodes_Level1And2And3HaveSavedmatch_AllCodesAdded()
        {
            // Arrange
            // gazetteer data - contains codes for names1 and names2
            GazetteerTestData gazetteerTestData = GazetteerTestData();

            // input data - level 1 miss-spelt
            string[] inputNames = {"P1x", "T1x", "V1x"};
            Location location = new Location(
                inputNames[0],
                inputNames[1],
                inputNames[2]);

            // saved matches
            MatchProviderStub matchProviderStub = MatchProviderStubLevel3(inputNames);

            LocationCodes locationCodes = new LocationCodes(
                gazetteerTestData.GadmList(),
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


        private GazetteerTestData GazetteerTestData()
        {
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1);
            gazetteerTestData.AddLine(names2, codes2);
            return gazetteerTestData;
        }

        private MatchProviderStub MatchProviderStubLevel1(string[] inputNames)
        {
            // database contains saved record for level 1 alternate name
            MatchProviderTestData matchProviderTestData = new MatchProviderTestData();
            matchProviderTestData.AddLevel1(inputNames, names1);

            MatchProviderStub matchProviderStub =
                new MatchProviderStub(matchProviderTestData);
            matchProviderStub.Alternate = inputNames;
            matchProviderStub.Actual = names1;
            return matchProviderStub;
        }

        private MatchProviderStub MatchProviderStubLevel2(string[] inputNames)
        {
            // database contains saved records for level 2 alternate name
            MatchProviderTestData matchProviderTestData = new MatchProviderTestData();
            matchProviderTestData.AddLevel1(inputNames, names1);
            matchProviderTestData.AddLevel2(inputNames, names1);

            MatchProviderStub matchProviderStub =
                new MatchProviderStub(matchProviderTestData);
            matchProviderStub.Alternate = inputNames;
            matchProviderStub.Actual = names1;
            return matchProviderStub;
        }

        private MatchProviderStub MatchProviderStubLevel3(string[] inputNames)
        {
            // database contains saved record for level 3 alternate name
            MatchProviderTestData matchProviderTestData = new MatchProviderTestData();
            matchProviderTestData.AddLevel1(inputNames, names1);
            matchProviderTestData.AddLevel2(inputNames, names1);
            matchProviderTestData.AddLevel3(inputNames, names1);

            MatchProviderStub matchProviderStub =
                new MatchProviderStub(matchProviderTestData);
            matchProviderStub.Alternate = inputNames;
            matchProviderStub.Actual = names1;
            return matchProviderStub;
        }

        #endregion Methods
    }
}