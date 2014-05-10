// MatchedNamesSaveTests.cs

namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;
    using Rhino.Mocks;

    /// <summary>
    /// Exercises the MatchedNames class SaveMatch method which does the
    /// saving of a match between an input location and a gazetteer 
    /// location
    /// </summary>
    [TestClass]
    public class MatchedNamesSaveTests
    {
        #region Methods

        // Given a matched input to save
        // When input values are the same as the matched gazetteer values except case differs
        // Then save is not called (no exception thrown) (Rule4)
        [TestMethod]
        public void SaveMatch_InputAndMatchAreSameButDifferentCase_NotSaved()
        {
            // Arrange
            const string inputName1 = "P1";
            const string inputName2 = "T1";
            const string inputName3 = "V1";
            const string gazName1 = "p1"; // same as input, case different
            const string gazName2 = "t1"; // same as input, case different
            const string gazName3 = "v1"; // same as input, case different

            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData2.TestData1());

            //match provider
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // no saves called
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasNotCalled(
                x =>
                    x.SaveMatchLevel2(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything));
            mock.AssertWasNotCalled(
                x =>
                    x.SaveMatchLevel3(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything));
        }

        // Given a matched input to save
        // When input values are the same as the matched gazzeteer values
        // Then save is not called (no exception thrown) (Rule4)
        [TestMethod]
        public void SaveMatch_InputAndMatchAreSame_NotSaved()
        {
            // Arrange
            const string inputName1 = "P1";
            const string inputName2 = "T1";
            const string inputName3 = "V1";
            const string gazName1 = "P1"; // same as input
            const string gazName2 = "T1"; // same as input
            const string gazName3 = "V1"; // same as input

            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData2.TestData1());

            //match provider
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // no saves called
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasNotCalled(
                x =>
                    x.SaveMatchLevel2(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything));
            mock.AssertWasNotCalled(
                x =>
                    x.SaveMatchLevel3(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything));
        }

        // Given a matched input to save
        // When input values and matched gazzeteer values are an acceptable match at all three levels
        // Then save is called for all three matched values
        [TestMethod]
        public void SaveMatch_InputAndMatchAreValid_MatchSaved()
        {
            // todo review this test
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
        // When input level 1 name aleady exists as an alternate name in the gazzeteer
        // Then a NameInGazetteerException is thrown (Rule 8)
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_InputLevel1ExistsInGazAsAlt_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1A"; // in gaz
            const string inputName2 = "T1A";
            const string inputName3 = "V1A";
            const string gazName1 = "P2"; // not allowed
            const string gazName2 = "T1";
            const string gazName3 = "V1";

            SaveMatchWithGazetteerTestData1(
                inputName1,
                inputName2,
                inputName3,
                gazName1,
                gazName2,
                gazName3);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When input level 1 name aleady exists in the gazeteer as a main entry
        // Then a NameInGazetteerException is thrown (Rule 8)
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_InputLevel1ExistsInGazAsMain_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1"; // in gaz
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P2"; // not allowed
            const string gazName2 = "T1";
            const string gazName3 = "V1";

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData2.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 2 value that is in the gazetteer
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer- must have correct matched level 1
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When level 2 name aleady exists as an alternate name in the gazzeteer
        // Then a NameInGazetteerException is thrown (Rule 8)
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_InputLevel2ExistsInGazAsAlt_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1A";
            const string inputName2 = "T1A"; // in gaz
            const string inputName3 = "V1A";
            const string gazName1 = "P1";
            const string gazName2 = "T2"; // not allowed
            const string gazName3 = "V1";

            SaveMatchWithGazetteerTestData1(
                inputName1,
                inputName2,
                inputName3,
                gazName1,
                gazName2,
                gazName3);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When input level 2 name aleady exists in the gazeteer as a main entry
        // Then a NameInGazetteerException is thrown (Rule 8)
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_InputLevel2ExistsInGazAsMain_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1"; // in gaz
            const string inputName3 = "V1x";
            const string gazName1 = "P1";
            const string gazName2 = "T2"; // not allowed
            const string gazName3 = "V1";

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData2.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 2 value that is in the gazetteer
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer- must have correct matched level 1
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When input level 3 name aleady exists in the gazeteer as an alt.
        // Then a NameInGazetteerException is thrown (Rule 8)
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_InputLevel3ExistsInGazAsAlt_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1A";
            const string inputName2 = "T1A";
            const string inputName3 = "V1A"; // in gaz
            const string gazName1 = "P1";
            const string gazName2 = "T1";
            const string gazName3 = "V2"; // not allowed

            SaveMatchWithGazetteerTestData1(
                inputName1,
                inputName2,
                inputName3,
                gazName1,
                gazName2,
                gazName3);

            // Assert
            // exception thrown
        }

        // Given a matched input to save
        // When input level 3 name aleady exists in the gazeteer as a main entry
        // Then a NameInGazetteerException is thrown (Rule 8)
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_InputLevel3ExistsInGazAsMain_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1"; // in gaz
            const string gazName1 = "P1";
            const string gazName2 = "T1";
            const string gazName3 = "V2"; // not allowed

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData2.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input containing level 3 value that is in the gazetteer
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer- must have correct matched level 1 and 2
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        private static void SaveMatchWithGazetteerTestData1(
            string inputName1,
            string inputName2,
            string inputName3,
            string gazName1,
            string gazName2,
            string gazName3)
        {
            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData2.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);
        }

        #endregion Methods

        #region Other

        // todo - MatchedNames.SaveMatch tests to show that input with less that three levels is saved
        // todo - MatchedNames.SaveMatch tests to show that Incomplete Location exception is thrown when  args invalid
        // todo tests for cases 1,2,3,5,6,7, rest of 8,and 9

        #endregion Other
    }
}