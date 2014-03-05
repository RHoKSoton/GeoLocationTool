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
    /// correct codes are applied to a given location using only the gazetteer data
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
            Location location = new Location(name1, name2, "SomeName");

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against
            var gazzetteerData = CreateGazetteerData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // code 1 and 2 only are added, no level 3 code added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(code2, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
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
            Location location = new Location(name1, "SomeName", name3);

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against
            var gazzetteerData = CreateGazetteerData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            //  level 1 codes added only
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(null, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        /// <summary>
        /// Given a location containing level 1, 2 and 3 names
        /// when all the level 1, 2 and 3 names are correct (case insensitive)
        /// and there are no previous saved name matches
        /// then level 1, 2 and 3 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1And2And3Correct_Level1And2And3CodesAdded()
        {
            // Arrange
            // Create location input, containing correct level 1, 2 and
            // level 3 (i.e. all are in the gazetteer)
            Location location = new Location(name1, name2, name3.ToLower());

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location
            var gazzetteerData = CreateGazetteerData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // all codes added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(code2, location.Level2Code);
            Assert.AreEqual(code3, location.Level3Code);
        }

        /// <summary>
        /// Given a location containing only level 1 and 2 names
        /// when both the level 1 and 2 names are correct (case insensitive)
        /// and there are no previous saved name matches
        /// then the level 1 and 2 codes are added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1and2Correct_Level1And2CodeAdded()
        {
            // Arrange
            // Create location input, containing correct level 1 and 2,(i.e. in the gazetteer)
            // no level 3 supplied
            Location location = new Location(name1.ToUpper(), name2.ToLower());

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location
            var gazzetteerData = CreateGazetteerData();

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // level 1 and 2 codes added, no level 3
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(code2, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
        }

        /// <summary>
        /// Given a location containing only a level 1 name
        /// when the level 1 name is correct (case insensitive)
        /// and there are no previous saved name matches
        /// then the level 1 code  only is added
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_Level1Correct_Level1CodeAdded()
        {
            // Arrange
            // Create location input, containing correct level 1,
            // no level 2 or 3 supplied
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
            // level 1 code only added
            Assert.AreEqual(code1, location.Level1Code);
            Assert.AreEqual(null, location.Level2Code);
            Assert.AreEqual(null, location.Level3Code);
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
            Location location = new Location("SomeName", name2, name3);

            // database contains no saved records
            INearMatchesProvider nearMatchesProviderWithNoRecords =
                NearMatchesProviderWithNoRecords();

            // create gazetteer data to match against - add a record
            // containing a match to the location at level 2 and 3 only
            var gazzetteerData = CreateGazetteerData();

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
        /// Given a location with existing codes
        /// when no codes are added
        /// then any existing codes are removed
        /// </summary>
        [TestMethod]
        public void GetLocationCodes_NoCodedToAddAndHasExistingCodes_CodesRemoved()
        {
            // Arrange
            // Create location input, containing incorrect level 1,
            // level  2 and 3 are correct (i.e. in the gazetteer)
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

            LocationCodes locationCodes = new LocationCodes(
                gazzetteerData,
                nearMatchesProviderWithNoRecords);

            // Act
            locationCodes.GetLocationCodes(location);

            // Assert
            // no codes present
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

            gadmList.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2,
                    ID_3 = code3,
                    NAME_3 = name3,
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