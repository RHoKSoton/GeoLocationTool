// MatchedNamesSaveTests.cs

namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;
    using Rhino.Mocks;

    /// <summary>
    /// Excercises the MatchedNames class SaveMatch method which does the
    /// saving of a match between an input location and a gazetteer 
    /// location
    /// </summary>
    [TestClass]
    public class MatchedNamesSaveTests
    {
        #region Methods

        // Given a matched input to save
        // When input level 1 name aleady exists as an alternate name in the gazzeteer
        // Then an exception is thrown
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_AltLevel1Exists_ExceptionThrown()
        {
            // Arrange
            const string name = "P1";
            const string altName = "P1A";

            // gazetteer data
            string[] names1 = {name, "T1", "V1"};
            string[] codes1 = {"1", "10", "100"};
            string[] altNames1 = {altName, "", ""};
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1, altNames1);
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 1 value that is in the gazetteer
            Location inputLocation = new Location(altName, "T1x", "V1x");

            // match from gazetteer- can have any complete values for this test
            Location gazetteerLocation = new Location("X", "Y", "Z");

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When level 1 has been sucessfully matched but
        // input level 2 name aleady exists as an alternate name in the gazzeteer
        // Then an exception is thrown
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_AltLevel2Exists_ExceptionThrown()
        {
            // Arrange
            const string name1 = "P1";
            const string name2 = "T1";
            const string altName2 = "T1A";

            // gazetteer data
            string[] names1 = {name1, name2, "V1"};
            string[] codes1 = {"1", "10", "100"};
            string[] altNames1 = {"", altName2, ""};
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1, altNames1);
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 2 value that is in the gazetteer
            Location inputLocation = new Location("P1x", altName2, "V1x");

            // match from gazetteer- must have correct matched level 1
            Location gazetteerLocation = new Location(name1, "Y", "Z");

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When level 1 and 2 has been sucessfully matched but
        // input level 3 name aleady exists in the gazzeteer.
        // Then an exception is thrown
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_AltLevel3Exists_ExceptionThrown()
        {
            // Arrange
            const string name1 = "P1";
            const string name2 = "T1";
            const string name3 = "V1";
            const string altName3 = "V1A";

            // gazetteer data
            string[] names1 = {name1, name2, name3};
            string[] codes1 = {"1", "10", "100"};
            string[] altNames1 = {"", "", altName3};
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1, altNames1);
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 3 value that is in the gazetteer
            Location inputLocation = new Location("P1x", "T1x", altName3);

            // match from gazetteer- must have correct matched level 1 and 2
            Location gazetteerLocation = new Location(name1, name2, "Z");

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given an input to gazetteer match to save
        // When input values are the same as the matched gazzeteer values
        // Then save is not called (no exception thrown)
        [TestMethod]
        public void SaveMatch_InputAndMatchAreSame_NotSaved()
        {
            // Arrange
            const string name1 = "P1";
            const string name2 = "T1";
            const string name3 = "V1";

            // gazetteer data
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());

            //match provider
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input and match containing the same values
            Location inputLocation = new Location(name1, name2, name3);
            Location gazetteerLocation = new Location(name1, name2, name3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        // Given an input to gazetteer match to save
        // When input values and matched gazzeteer values are an acceptable match at all three levels
        // Then save is called for all three matched values
        [TestMethod]
        public void SaveMatch_InputAndMatchAreValid_MatchSaved()
        {
            // Arrange
            const string name1 = "P1";
            const string name2 = "T1";
            const string name3 = "V1";
            string[] names1 = {name1, name2, name3};
            string[] codes1 = {"1", "10", "100"};

            // gazetteer data
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1);
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());

            //match provider
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location("P1x", "T1x", "V1x");

            // match from gazetteer
            Location gazetteerLocation = new Location(name1, name2, name3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasCalled(x => x.SaveMatchLevel1("P1x", name1));
            mock.AssertWasCalled(x => x.SaveMatchLevel2("T1x", name1, name2));
            mock.AssertWasCalled(x => x.SaveMatchLevel3("V1x", name1, name2, name3));
        }

        // Given a matched input to save
        // When input level 1 name aleady exists in the gazzeteer
        // Then an exception is thrown
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_Level1Exists_ExceptionThrown()
        {
            // Arrange
            const string name = "P1";

            // gazetteer data
            string[] names1 = {name, "T1", "V1"};
            string[] codes1 = {"1", "10", "100"};
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1);
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 1 value that is in the gazetteer
            Location inputLocation = new Location(name, "T1x", "V1x");

            // match from gazetteer- can have any complete values for this test
            Location gazetteerLocation = new Location("X", "Y", "Z");

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When level 1 has been sucessfully matched but
        // input level 2 name aleady exists in the gazzeteer
        // Then an exception is thrown
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_Level2Exists_ExceptionThrown()
        {
            // Arrange
            const string name1 = "P1";
            const string name2 = "T1";

            // gazetteer data
            string[] names1 = {name1, name2, "V1"};
            string[] codes1 = {"1", "10", "100"};
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1);
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 2 value that is in the gazetteer
            Location inputLocation = new Location("P1x", name2, "V1x");

            // match from gazetteer- must have correct matched level 1
            Location gazetteerLocation = new Location(name1, "Y", "Z");

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When level 1 and 2 has been sucessfully matched but
        // input level 3 name aleady exists in the gazzeteer.
        // Then an exception is thrown
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_Level3Exists_ExceptionThrown()
        {
            // Arrange
            const string name1 = "P1";
            const string name2 = "T1";
            const string name3 = "V1";

            // gazetteer data
            string[] names1 = {name1, name2, name3};
            string[] codes1 = {"1", "10", "100"};
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1);
            LocationNames locationNames = new LocationNames(gazetteerTestData.GadmList());
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 3 value that is in the gazetteer
            Location inputLocation = new Location("P1x", "T1x", name3);

            // match from gazetteer- must have correct matched level 1 and 2
            Location gazetteerLocation = new Location(name1, name2, "Z");

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        #endregion Methods

        #region Other
      
        // todo - MatchedNames.SaveMatch tests to show that input with less that three levels is saved
        // todo - MatchedNames.SaveMatch tests to show that Incomplete Location exception is thrown when  args invalid

        #endregion Other
    }
}