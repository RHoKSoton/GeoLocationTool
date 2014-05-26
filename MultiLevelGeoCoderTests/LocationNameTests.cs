// LocationNameTests.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Excercises the location names class
    /// </summary>
    [TestClass]
    public class LocationNameTests
    {
        #region Methods

        /// <summary>
        /// When gazetteer contains records with entries containing leading or trailing spaces
        /// Then list should exclude the leading and trailing spaces
        /// </summary>
        [TestMethod]
        public void Level1AllLocationNames_LeadingAndTrailingSpacesRemoved()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());
            // act
            IList<string> result = locationNames.Level1AllLocationNames();
            // assert
            // no leading or trailing spaces
            Assert.IsFalse(result.Any(x => x.StartsWith(" ")));
            Assert.IsFalse(result.Any(x => x.EndsWith(" ")));
        }

        /// <summary>
        /// When gazetteer contains records with empty string alternate entries
        /// Then list should exclude the empty strings
        /// </summary>
        [TestMethod]
        public void Level1AllLocationNames_NoBlankOrNullReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());
            // act
            IList<string> result = locationNames.Level1AllLocationNames();
            // assert
            // no null or empty strings
            Assert.IsFalse(result.Contains(""));
            Assert.IsFalse(result.Contains(null));
        }

        /// <summary>
        /// Given an alternate level 1 name (P1A)
        /// When gazetteer contains records with main and alternate (P1 and P1A) at level1, 
        /// Then should return the main level 2 names plus the alt level 2 names
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_AltLevel1Name_MainAndAltNamesReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());
            // act
            IList<string> result = locationNames.Level2AllLocationNames("P1A");
            // assert
            // expected T1,T2, T1A, T2A
            Assert.AreEqual(4, result.Count);
            List<string> expected = new List<string> {"T1", "T2", "T1A", "T2A"};
            IEnumerable<string> dif = result.Except(expected);
            Assert.AreEqual(0, dif.Count());
        }

        /// <summary>
        /// Given an main level 1 name (P1)
        /// When gazetteer contains records with main and alternate (P1 and P1A) at level1, 
        /// Then should return the main level 2 names plus the alt level 2 names
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_MainLevel1Name_MainAndAltNamesReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());
            // act
            IList<string> result = locationNames.Level2AllLocationNames("P1");
            // assert
            // expected T1,T2, T1A, T2A
            Assert.AreEqual(4, result.Count);
            List<string> expected = new List<string> {"T1", "T2", "T1A", "T2A"};
            IEnumerable<string> dif = result.Except(expected);
            Assert.AreEqual(0, dif.Count());
        }

        /// <summary>
        /// Given an alternate level 1 name (P1A)
        /// When gazetteer contains records with empty string alternate entries
        /// Then list should exclude the empty strings
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_NoBlankOrNullReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());
            // act
            IList<string> result = locationNames.Level2AllLocationNames("P1A");
            // assert
            // no null or empty strings
            Assert.IsFalse(result.Contains(""));
            Assert.IsFalse(result.Contains(null));
        }

        /// <summary>
        /// When the search names are same except for the casing 
        /// Then the returned level 2 name list should be the same
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_SearchNamesCaseDiffToGaz_ListIsSame()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // act
            IList<string> result1 = locationNames.Level2AllLocationNames("P1A");
            IList<string> result2 = locationNames.Level2AllLocationNames("p1a");

            // assert
            // expected that the results are the same
            Assert.AreEqual(result1.Count, result2.Count);
            IEnumerable<string> dif = result1.Except(result2);
            Assert.AreEqual(0, dif.Count());
        }

        /// <summary>
        /// Given an alt level 1 name (P2A) and an alt level 2 name (T2A)
        /// When gazetteer contains records with main and alternate (P1 and P1A) at level1 and 
        /// main and alternate (T2 and T2A) at level2
        /// Then should return the main level 3 names plus the alt level 3 names
        /// </summary>
        [TestMethod]
        public void Level3AllLocationNames_AltLevel1And2Names_MainAndAltNamesReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // act
            IList<string> result = locationNames.Level3AllLocationNames("P2A", "T2A");

            // assert
            // expected V1,V2, V3, V1A
            Assert.AreEqual(4, result.Count);
            List<string> expected = new List<string> {"V1", "V2", "V3", "V1A"};
            IEnumerable<string> dif = result.Except(expected);
            Assert.AreEqual(0, dif.Count());
        }

        /// <summary>
        /// Given an main level 1 name (P2) and a main level 2 name (T2)
        /// When gazetteer contains records with main and alternate (P1 and P1A) at level1 and 
        /// main and alternate (T2 and T2A) at level2
        /// Then should return the main level 3 names plus the alt level 3 names
        /// </summary>
        [TestMethod]
        public void Level3AllLocationNames_MainLevel1And2Names_MainAndAltNamesReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // act
            IList<string> result = locationNames.Level3AllLocationNames("P2", "T2");

            // assert
            // expected V1,V2, V3, V1A
            Assert.AreEqual(4, result.Count);
            List<string> expected = new List<string> {"V1", "V2", "V3", "V1A"};
            IEnumerable<string> dif = result.Except(expected);
            Assert.AreEqual(0, dif.Count());
        }

        /// <summary>
        /// Given an alt level 1  and 2 names (P1A and T1A) 
        /// When gazetteer contains records with empty string alternate entries
        /// Then list should exclude the empty strings
        /// </summary>
        [TestMethod]
        public void Level3AllLocationNames_NoBlankOrNullReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // act
            IList<string> result = locationNames.Level3AllLocationNames("P1A", "T1A");

            // assert
            // no null or empty strings
            Assert.IsFalse(result.Contains(""));
            Assert.IsFalse(result.Contains(null));
        }

        /// <summary>
        /// When the search names are same except for the casing 
        /// Then the returned level 3 name list should be the same
        /// </summary>
        [TestMethod]
        public void Level3AllLocationNames_SearchNamesCaseDiffToGaz_ListIsSame()
        {
            // arrange
            LocationNames locationNames = new LocationNames(
                GazetteerTestData.TestData1());

            // act
            IList<string> result1 = locationNames.Level3AllLocationNames("P2A", "T2A");
            IList<string> result2 = locationNames.Level3AllLocationNames("p2a", "t2a");

            // assert
            // expected that the results are the same
            Assert.AreEqual(result1.Count, result2.Count);
            IEnumerable<string> dif = result1.Except(result2);
            Assert.AreEqual(0, dif.Count());
        }

        #endregion Methods
    }
}