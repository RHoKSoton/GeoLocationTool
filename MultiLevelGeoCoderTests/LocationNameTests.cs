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
        #region Fields

        // Test Gazetteer Data
        private readonly string[] altNames1 = {"P1A", "T1A", "V1A"};
        private readonly string[] altNames3 = {"P2A", "T2A", "V1A"};
        private readonly string[] altNames4 = {"P1A", "T2A", "V1A"};

        // we dont care about the codes for these tests
        private readonly string[] codes0 = {"1", "10", "100"};
  
        private readonly string[] names1 = {"P1", "T1", "V1"};
        private readonly string[] names2 = {"P1", "T1", "V2"};
        private readonly string[] names3 = {"P2", "T2", "V1"};
        private readonly string[] names4 = {"P2", "T2", "V2"};
        private readonly string[] names5 = {"P2", "T2", "V3"};
        private readonly string[] names6 = {"P1", "T2", "V1"};
        private readonly string[] names7 = {"P1", "T2", "V4"};
        private readonly string[] names8 = {"P2", "T1", "V2"};

        #endregion Fields

        #region Methods

        /// <summary>
        /// Given an alternate level 1 name (P1A)
        /// When gazetteer contains records with main and alternate (P1 and P1A) at level1, 
        /// Then should return the main level 2 names plus the alt level 2 names
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_AltLevel1Name_MainAndAltNamesReturned()
        {
            // arrange
            LocationNames locationNames = new LocationNames(TestGazData1());
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
            LocationNames locationNames = new LocationNames(TestGazData1());
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
        /// When the search names are same except for the casing 
        /// Then the returned level 2 name list should be the same
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_SearchNamesCaseDiffToGaz_ListIsSame()
        {
            // arrange
            LocationNames locationNames = new LocationNames(TestGazData1());

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
            LocationNames locationNames = new LocationNames(TestGazData1());

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
            LocationNames locationNames = new LocationNames(TestGazData1());

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
        /// When the search names are same except for the casing 
        /// Then the returned level 3 name list should be the same
        /// </summary>
        [TestMethod]
        public void Level3AllLocationNames_SearchNamesCaseDiffToGaz_ListIsSame()
        {
            // arrange
            LocationNames locationNames = new LocationNames(TestGazData1());

            // act
            IList<string> result1 = locationNames.Level3AllLocationNames("P2A", "T2A");
            IList<string> result2 = locationNames.Level3AllLocationNames("p2a", "t2a");

            // assert
            // expected that the results are the same
            Assert.AreEqual(result1.Count, result2.Count);
            IEnumerable<string> dif = result1.Except(result2);
            Assert.AreEqual(0, dif.Count());
        }

        private List<GazetteerRecord> TestGazData1()
        {
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            // P1, T1, V1, P1A, T1A, V1A
            gazetteerTestData.AddLine(names1, codes0, altNames1);
            // P1, T1, V2
            gazetteerTestData.AddLine(names2, codes0);
            // P2, T2, V1, P2A, T2A, V1A
            gazetteerTestData.AddLine(names3, codes0, altNames3);
            // P2, T2, V2,
            gazetteerTestData.AddLine(names4, codes0);
            // P2, T2, V3,
            gazetteerTestData.AddLine(names5, codes0);
            // P1, T2, V1, P1A, T2A, V1A,
            gazetteerTestData.AddLine(names6, codes0, altNames4);
            // P1, T2, V4
            gazetteerTestData.AddLine(names7, codes0);
            // P2, T1, V2
            gazetteerTestData.AddLine(names8, codes0);

            return gazetteerTestData.GadmList();
        }

        #endregion Methods
    }
}