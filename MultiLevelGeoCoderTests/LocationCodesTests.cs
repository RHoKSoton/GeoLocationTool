// LocationCodesTests.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;
    using MultiLevelGeoCoder.Model;
    using Rhino.Mocks;

    /// <summary>
    /// Exercises the LocationCodes class, in particular it tests that the 
    /// correct codes are matched to a given location using only the gazetteer data
    /// </summary>
    [TestClass]
    public class LocationCodesTests
    {
        #region Fields

        // test input location data
        private const string code1 = "1";
        private const string code2 = "15";
        private const string code3 = "150";
        private const string name1 = "TestProvince";
        private const string name2 = "TestTown";
        private const string name3 = "TestVillage";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Given a location with level 1, 2 and 3 names 
        /// when level1 and 2 name matches but level3 does not
        /// then only level1 and 2 code is added
        /// </summary>
        [TestMethod]
        public void
            GetLocationCodes_Leve1And2MatchAndLevel3NoMatch_Level1And2CodeOnlyAdded()
        {
            // Arrange
            // create location input, level 1, 2  and 3
            Location location = new Location(name1, name2, "SomeName");

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location at level 2 and 3 only
            var gazzetteerData = CreateGazetteerData();
            gazzetteerData.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2,
                    ID_3 = code3,
                    NAME_3 = name3,
                });
            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // no codes added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(code2, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        /// <summary>
        /// Given a location with level 1, 2 and 3 names 
        /// when level1 name matches but level2 does not
        /// then only a level1 code is added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Leve1MatchAndLevel2NoMatch_Level1CodeOnlyAdded()
        {
            // Arrange
            // create location input, level 1, 2  and 3
            Location location = new Location(name1, "SomeName", name3);

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location at level 2 and 3 only
            var gazzetteerData = CreateGazetteerData();
            gazzetteerData.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2,
                    ID_3 = code3,
                    NAME_3 = name3,
                });
            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // no codes added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(null, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        /// <summary>
        /// Given a location containing only level 1, 2 and 3 names
        /// when both the level 1, 2 and 3 names match a record (case insensitive)
        /// then the correct level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1And2And3Match_Level1And2And3CodesAdded()
        {
            // Arrange
            // create location input, level 1, 2  and 3
            Location location = new Location(name1, name2, name3.ToLower());

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location
            var gazzetteerData = CreateGazetteerData();
            gazzetteerData.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2.ToUpper(),
                    ID_3 = code3,
                    NAME_3 = name3,
                });

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // correct codes added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(code2, location.Level2Code);
            Assert.AreEqual(code3, location.Level3Code);
        }

        /// <summary>
        /// Given a location containing only level 1 and 2 names
        /// when both the level 1 and 2 names match a record (case insensitive)
        /// then the correct level 1 and 2 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1and2Match_CodeAddedLevel1And2()
        {
            // Arrange
            // create location input, level 1 and 2 only
            Location location = new Location(name1.ToUpper(), name2.ToLower());

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location
            var gazzetteerData = CreateGazetteerData();
            gazzetteerData.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2.ToUpper(),
                    ID_3 = code3,
                    NAME_3 = name3,
                });

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // correct codes added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(code2, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        /// <summary>
        /// Given a location containing only a level 1 name
        /// when the level 1 names matches a record (case insensitive)
        /// then the correct level 1 code is added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1Match_CodeAddedLevel1()
        {
            // Arrange
            // create location input, level 1 only
            Location location = new Location(name1.ToLower());

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location
            var gazzetteerData = CreateGazetteerData();
            gazzetteerData.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2,
                    ID_3 = code3,
                    NAME_3 = name3,
                });

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // correct codes added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(null, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        /// <summary>
        /// Given a location with level 1, 2 and 3 names 
        /// when level1 name does not match a the level1 name on any record 
        /// then no codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1NoMatch_NoCodesAdded()
        {
            // Arrange
            // create location input, level 1, 2  and 3
            Location location = new Location("SomeName", name2, name3);

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location at level 2 and 3 only
            var gazzetteerData = CreateGazetteerData();
            gazzetteerData.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name2,
                    ID_2 = code2,
                    NAME_2 = name2,
                    ID_3 = code3,
                    NAME_3 = name3,
                });
            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // no codes added
            Assert.AreEqual(null, location.Level1Code);
            Assert.AreEqual(null, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        /// <summary>
        /// Given a location with level 1, 2 and 3 names 
        /// when level1 and 2 name matches but level3 does not
        /// then only level1 and 2 code is added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_NoMatchAtLevel1AndHasExistingCodes_CodesRemoved()
        {
            // Arrange
            // create location input, level 1, 2  and 3
            Location location = new Location("SomeName", name2, name3);
            location.Level1Code = "1";
            location.Level2Code = "22";
            location.Level3Code = "150";

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location at level 2 and 3 only
            var gazzetteerData = CreateGazetteerData();
            gazzetteerData.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2,
                    ID_3 = code3,
                    NAME_3 = name3,
                });

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // no codes added
            Assert.AreEqual(null, location.Level1Code);
            Assert.AreEqual(null, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        private static IList<Gadm> CreateGazetteerData()
        {
            List<Gadm> gadmList = new List<Gadm>();

            gadmList.Add(
                new Gadm
                {
                    ID_1 = "1",
                    NAME_1 = "Abra",
                    ID_2 = "18",
                    NAME_2 = "Pidigan",
                    ID_3 = "188",
                    NAME_3 = "Alinaya",
                    VARNAME_3 = ""
                });

            gadmList.Add(
                new Gadm
                {
                    ID_1 = "1",
                    NAME_1 = "Abra",
                    ID_2 = "17",
                    NAME_2 = "PeÃ±arrubia",
                    ID_3 = "181",
                    NAME_3 = "Malamsit",
                    VARNAME_3 = "Pau-Malamsit"
                });

            gadmList.Add(
                new Gadm
                {
                    ID_1 = "9",
                    NAME_1 = "Basilan",
                    ID_2 = "132",
                    NAME_2 = "Tipo-Tipo",
                    ID_3 = "3018",
                    NAME_3 = "Tipo-Tipo Proper",
                    VARNAME_3 = "Poblacion"
                });
            return gadmList;
        }

        private static INearMatchesProvider NearMatchesProviderWithNoRecords()
        {
            INearMatchesProvider nearMatchesStub =
                MockRepository.GenerateStub<INearMatchesProvider>();
            nearMatchesStub.Stub(x => x.GetActualMatches(Arg<string>.Is.Anything))
                .Return(new List<Level1NearMatch>());
            nearMatchesStub.Stub(
                x => x.GetActualMatches(Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                .Return(new List<Level2NearMatch>());
            nearMatchesStub.Stub(
                x =>
                    x.GetActualMatches(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything))
                .Return(new List<Level3NearMatch>());
            return nearMatchesStub;
        }

        #endregion Methods
    }
}