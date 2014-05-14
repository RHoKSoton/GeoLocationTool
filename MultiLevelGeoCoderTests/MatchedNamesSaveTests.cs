// MatchedNamesSaveTests.cs

namespace MultiLevelGeoCoderTests
{
    using System;
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

        // Given a matched input to save (Rule 6)
        // When input values are the same as the matched gazzeteer values
        // Then save is not called (no exception thrown)
        [TestMethod]
        public void SaveMatch_InputAndMatchAllAreSame_NotSaved()
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
                GazetteerTestData.TestData1());

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
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 6)
        // When input values are the same as the matched gazetteer values except case differs
        // Then save is not called (no exception thrown)
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
                GazetteerTestData.TestData1());

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
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 1)
        // When input values and matched gazzeteer values are an valid match at all levels
        // Then save is called for all matched values
        [TestMethod]
        public void SaveMatch_InputAndMatchAreValid_MatchSaved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1"; // main value in gaz
            const string gazName2 = "T1"; // main value in gaz
            const string gazName3 = "V1"; // main value in gaz

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            mock.AssertWasCalled(x => x.SaveMatchLevel1(inputName1, gazName1));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, gazName1, gazName2));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, gazName1, gazName2, gazName3));
        }

        // Given a matched input to save (Rule 1)
        // When input values and matched gazzeteer values are an valid match at all levels
        // and input only contains level 1
        // Then save is called for all matched values
        [TestMethod]
        public void SaveMatch_InputContains1ValidLevel_MatchSaved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string gazName1 = "P1"; // main value in gaz

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            //match provider
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasCalled(x => x.SaveMatchLevel1(inputName1, gazName1));
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel2(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything));
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel3(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything));
        }

        // Given a matched input to save (Rule 1)
        // When input values and matched gazzeteer values are an valid match at all levels
        // and input only contains two levels
        // Then save is called for all matched values
        [TestMethod]
        public void SaveMatch_InputContains2ValidLevels_MatchSaved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string gazName1 = "P1"; // main value in gaz
            const string gazName2 = "T1"; // main value in gaz

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            //match provider
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasCalled(x => x.SaveMatchLevel1(inputName1, gazName1));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, gazName1, gazName2));
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel3(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything));
        }

        // Given a matched input to save (Rule 7)
        // When input level 1 and 2 value is the same as the matched value
        // Then save is called for only level 3 matched values
        [TestMethod]
        public void SaveMatch_InputLevel1And2AndMatchAreSame_Level3Saved()
        {
            // Arrange
            const string inputName1 = "P1";
            const string inputName2 = "T1";
            const string inputName3 = "V1x";
            const string gazName1 = "P1"; // same
            const string gazName2 = "T1"; // same
            const string gazName3 = "V1"; // valid

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel2(
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, gazName1, gazName2, gazName3));
        }

        // Given a matched input to save (Rule 7)
        // When input level 1 value is the same as the matched value
        // Then save is called for only level 2 and 3 matched values
        [TestMethod]
        public void SaveMatch_InputLevel1AndMatchAreSame_Level2And3Saved()
        {
            // Arrange
            const string inputName1 = "P1";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1"; // same
            const string gazName2 = "T1"; // valid
            const string gazName3 = "V1"; // valid

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, gazName1, gazName2));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, gazName1, gazName2, gazName3));
        }

        // Given a matched input to save (Rule 11)
        // When input level 1 name aleady exists as an alternate name in the gazzeteer
        // Then a NameInGazetteerException is thrown
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

        // Given a matched input to save (Rule 10)
        // When input level 1 name aleady exists in the gazeteer as a main entry
        // Then a NameInGazetteerException is thrown
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
                GazetteerTestData.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save (Rule 9)
        // When the input is a gazetteer alt for the selected match only case differs
        // Then it should not be saved, lower level valid selections should be saved
        [TestMethod]
        public void SaveMatch_InputLevel1IsAltEquivalentToMatchCaseDiffers_Level2And3Saved
            ()
        {
            // Arrange
            const string inputName1 = "p1a";
            const string inputName2 = "t1x";
            const string inputName3 = "v1x";
            const string gazName1 = "P1"; // ignore as is equivalent
            const string gazName2 = "T1"; // valid match
            const string gazName3 = "V1"; // valid match

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, gazName1, gazName2));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, gazName1, gazName2, gazName3));
        }

        // Given a matched input to save (Rule 9)
        // When the input is a gazetteer alt for the selected match
        // Then it should not be saved, lower level valid selections should be saved
        [TestMethod]
        public void SaveMatch_InputLevel1IsAltEquivalentToMatch_Level2And3Saved()
        {
            // Arrange
            const string inputName1 = "P1A";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1"; // ignore as is equivalent
            const string gazName2 = "T1"; // valid match
            const string gazName3 = "V1"; // valid match

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, gazName1, gazName2));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, gazName1, gazName2, gazName3));
        }

        // Given a matched input to save (Rule 2)
        // When input level 1 is blank
        // Then an IncompleteLocationException is thrown
        [TestMethod]
        [ExpectedException(typeof (IncompleteLocationException))]
        public void SaveMatch_InputLevel1IsBlank_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = ""; // invalid
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1";
            const string gazName2 = "T1";
            const string gazName3 = "V1";

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer-
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save (Rule 11)
        // When input level 2 name aleady exists as an alternate name in the gazzeteer
        // Then a NameInGazetteerException is thrown
        [TestMethod]
        [ExpectedException(typeof (NameInGazetteerException))]
        public void SaveMatch_InputLevel2ExistsInGazAsAlt_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1A";
            const string inputName2 = "T1A"; // in gaz
            const string inputName3 = "V1A";
            const string gazName1 = "P1"; // equivalent
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

        // Given a matched input to save (Rule 10)
        // When input level 2 name aleady exists in the gazeteer as a main entry
        // Then a NameInGazetteerException is thrown
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
                GazetteerTestData.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save (Rule 10)
        // When input level 2 name aleady exists in the gazeteer as a main entry
        // Then save is not called (exception also thrown, see separate test)
        [TestMethod]
        public void SaveMatch_InputLevel2ExistsInGazAsMain_NotSaved()
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
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            try
            {
                matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);
            }
            catch (Exception)
            {
                // ignore the ex for this test
            }

            // Assert
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 3)
        // When input level 2 value is blank
        // Then save is only called for level1
        [TestMethod]
        public void SaveMatch_InputLevel2IsBlank_OnlyLevel1Saved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "";
            const string inputName3 = "V1x";
            const string gazName1 = "P1";
            const string gazName2 = "T1";
            const string gazName3 = "V1";

            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            // only level 1 save called
            mock.AssertWasCalled(
                x => x.SaveMatchLevel1(inputName1, gazName1));
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

        // Given a matched input to save (Rule 11)
        // When input level 3 name aleady exists in the gazeteer as an alt.
        // Then a NameInGazetteerException is thrown
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

        // Given a matched input to save (Rule 11)
        // When input level 3 name aleady exists in the gazeteer as an alt.
        // Then save is not called (exception also thrown, see separate test)
        [TestMethod]
        public void SaveMatch_InputLevel3ExistsInGazAsAlt_NotSaved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1A"; // in gaz
            const string gazName1 = "P1";
            const string gazName2 = "T1";
            const string gazName3 = "V2"; // not allowed

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            try
            {
                matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);
            }
            catch (Exception)
            {
                //ignore the exception for this test
            }

            // Assert
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 10)
        // When input level 3 name aleady exists in the gazeteer as a main entry
        // Then a NameInGazetteerException is thrown
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
                GazetteerTestData.TestData1());

            // no existing saved matches
            MatchedNames matchedNames = new MatchedNames(MatchProviderStub.EmptyStub());

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            // exception thrown
        }

        // Given a matched input to save (Rule 10)
        // When input level 3 name aleady exists in the gazeteer as a main entry
        // Then save is not called (exception also thrown, see separate test)
        [TestMethod]
        public void SaveMatch_InputLevel3ExistsInGazAsMain_NotSaved()
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
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match from gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            try
            {
                matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);
            }
            catch (Exception)
            {
                //ignore the exception for this test
            }

            // Assert
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 3)
        // When input level 3 value is blank
        // Then save is only called for level 1 and 2
        [TestMethod]
        public void SaveMatch_InputLevel3IsBlank_OnlyLevel1And2Saved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "";
            const string gazName1 = "P1";
            const string gazName2 = "T1";
            const string gazName3 = "V1";

            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            // level 1 and 2 save called
            mock.AssertWasCalled(
                x => x.SaveMatchLevel1(inputName1, gazName1));
            mock.AssertWasCalled(
                x =>
                    x.SaveMatchLevel2(
                        inputName2,
                        gazName1,
                        gazName2));
            mock.AssertWasNotCalled(
                x =>
                    x.SaveMatchLevel3(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything));
        }

        // Given a matched input to save (Rule 12)
        // When the selected match is not in the gazetteer.
        // Then an Argumentxception is thrown
        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void SaveMatch_MatchDataNotInGaz_ExceptionThrown()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1U"; // not in gaz
            const string gazName2 = "T1U"; // not in gaz
            const string gazName3 = "V1U"; // not in gaz

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

        // Given a matched input to save (Rule 8)
        // When the selected match is an alt value in the gazetteer for the input
        // Then it should not be saved
        [TestMethod]
        public void SaveMatch_MatchIsAltEquivalentToInputCaseDiffers_NotSaved()
        {
            // Arrange
            const string inputName1 = "p1";
            const string inputName2 = "t1";
            const string inputName3 = "v1";
            const string gazName1 = "P1A"; // ignore as is equivalent
            const string gazName2 = "T1A"; // ignore as is equivalent
            const string gazName3 = "V1A"; // ignore as is equivalent

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match to gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 8)
        // When the selected match is an alt value in the gazetteer for the input
        // Then it should not be saved
        [TestMethod]
        public void SaveMatch_MatchIsAltEquivalentToInput_NotSaved()
        {
            // Arrange
            const string inputName1 = "P1";
            const string inputName2 = "T1";
            const string inputName3 = "V1";
            const string gazName1 = "P1A"; // ignore as is equivalent
            const string gazName2 = "T1A"; // ignore as is equivalent
            const string gazName3 = "V1A"; // ignore as is equivalent

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match to gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 13)
        // When the matched gazeteer values are in gazetteer as alt names
        // Then save is called using the main value as the match and not the alt value.
        [TestMethod]
        public void SaveMatch_MatchIsAltValue_MatchSavedUsingMainValue()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1A"; // in gaz as alt value
            const string gazName2 = "T1A"; // in gaz as alt value
            const string gazName3 = "V1A"; // in gaz as alt value
            const string mainName1 = "P1"; // main value in gaz
            const string mainName2 = "T1"; // main value in gaz
            const string mainName3 = "V1"; // main value in gaz

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            mock.AssertWasCalled(x => x.SaveMatchLevel1(inputName1, mainName1));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, mainName1, mainName2));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, mainName1, mainName2, mainName3));
        }

        // Given a matched input to save (Rule 4)
        // When selected match values are all blank
        // Then save is not called (no exception thrown)
        [TestMethod]
        public void SaveMatch_MatchIsBlank_NotSaved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "";
            const string gazName2 = "";
            const string gazName3 = "";

            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            AssertNoSavesCalled(mock);
        }

        // Given a matched input to save (Rule 8)
        // When the selected match is a gazetteer alt equivalent for the input (except for the case)
        // Then it should not be saved, valid lower level matched should be saved
        [TestMethod]
        public void SaveMatch_MatchLevel1IsAltEquivalentToInputCaseDiffers_Level2And3Saved
            ()
        {
            // Arrange
            const string inputName1 = "p1";
            const string inputName2 = "t1x";
            const string inputName3 = "v1x";
            const string gazName1 = "P1A"; // ignore as is equivalent
            const string gazName2 = "T1A"; // valid
            const string gazName3 = "V1A"; // valid
            const string mainName1 = "P1"; // main value in gaz
            const string mainName2 = "T1"; // main value in gaz
            const string mainName3 = "V1"; // main value in gaz

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match to gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, mainName1, mainName2));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, mainName1, mainName2, mainName3));
        }

        // Given a matched input to save (Rule 8)
        // When the selected match is a gazetteer alt equivalent for the input
        // Then it should not be saved, valid lower level matched should be saved
        [TestMethod]
        public void SaveMatch_MatchLevel1IsAltEquivalentToInput_Level2And3Saved()
        {
            // Arrange
            const string inputName1 = "P1";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1A"; // ignore as is equivalent
            const string gazName2 = "T1A"; // valid
            const string gazName3 = "V1A"; // valid
            const string mainName1 = "P1"; // main value in gaz
            const string mainName2 = "T1"; // main value in gaz
            const string mainName3 = "V1"; // main value in gaz

            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // no existing saved matches
            var mock = MockRepository.GenerateMock<IMatchProvider>();
            MatchedNames matchedNames = new MatchedNames(mock);

            // input
            Location inputLocation = new Location(inputName1, inputName2, inputName3);

            // match to gazetteer
            Location gazetteerLocation = new Location(gazName1, gazName2, gazName3);

            // Act
            matchedNames.SaveMatch(inputLocation, gazetteerLocation, locationNames);

            // Assert
            mock.AssertWasNotCalled(
                x => x.SaveMatchLevel1(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mock.AssertWasCalled(x => x.SaveMatchLevel2(inputName2, mainName1, mainName2));
            mock.AssertWasCalled(
                x => x.SaveMatchLevel3(inputName3, mainName1, mainName2, mainName3));
        }

        // Given a matched input to save (Rule 4)
        // When selected match level 2 value is blank
        // Then only level 1 saved
        [TestMethod]
        public void SaveMatch_MatchLevel2IsBlank_OnlyLevel1Saved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1";
            const string gazName2 = "";
            const string gazName3 = "";

            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            // only level 1 save called
            mock.AssertWasCalled(
                x => x.SaveMatchLevel1(inputName1, gazName1));
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

        // Given a matched input to save (Rule 4)
        // When selected match level 2 value is blank
        // Then only level 1 saved
        [TestMethod]
        public void SaveMatch_MatchLevel3IsBlank_OnlyLevel1And2Saved()
        {
            // Arrange
            const string inputName1 = "P1x";
            const string inputName2 = "T1x";
            const string inputName3 = "V1x";
            const string gazName1 = "P1";
            const string gazName2 = "T1";
            const string gazName3 = "";

            // Arrange
            // gazetteer data - use test data 1
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

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
            // only level 1 and 2 save called
            mock.AssertWasCalled(
                x => x.SaveMatchLevel1(inputName1, gazName1));
            mock.AssertWasCalled(
                x =>
                    x.SaveMatchLevel2(
                        inputName2,
                        gazName1,
                        gazName2));
            mock.AssertWasNotCalled(
                x =>
                    x.SaveMatchLevel3(
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything,
                        Arg<string>.Is.Anything));
        }

        private static void AssertNoSavesCalled(IMatchProvider mock)
        {
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
                GazetteerTestData.TestData1());

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
    }
}