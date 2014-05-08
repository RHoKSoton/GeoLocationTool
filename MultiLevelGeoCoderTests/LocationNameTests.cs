using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MultiLevelGeoCoder.Logic;
    using Rhino.Mocks.Constraints;

    [TestClass]
    public class LocationNameTests
    {

        // Test Gazetteer Data
        private readonly string[] names1 = { "P1", "T1", "V1" };
        private readonly string[] names2 = { "P1", "T1", "V2" };
        private readonly string[] names3 = { "P2", "T2", "V1" };
        private readonly string[] names4 = { "P2", "T2", "V2" };
       
        private readonly string[] names5 = { "P2", "T2", "V3" };
        private readonly string[] names6 = { "P1", "T2", "V1" };
        private readonly string[] names7 = { "P1", "T2", "V4" };
        private readonly string[] names8 = { "P2", "T1", "V2" };

        // we dont care about the codes for these tests
        private readonly string[] codes0 = { "1", "10", "100" };
       
        private readonly string[] altNames1 = { "P1A", "T1A", "V1A" };
        private readonly string[] altNames3 = { "P2A", "T2A", "V1A" };
        private readonly string[] altNames4 = { "P1A", "T2A", "V1A" };


        /// <summary>
        /// given P1
        /// when gaz contains four records with P1 at level1, containing two unique level2 names, 
        /// two records having alt level 2 names, both unique
        /// then should return the two main level 2 names plus the two alt level 2 names
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_GazContainsMainAndAltNames_MainAndAltReturned()
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
        /// given P1A
        /// when gaz contains four records with P1 or P1A at level1, containing two unique level2 names, 
        /// two records having alt level 2 names, both unique
        /// then should return the two main level 2 names plus the two alt level 2 names
        /// </summary>
        [TestMethod]
        public void Level2AllLocationNames_Level1AltGiven_MainAndAltReturned()
        {
            // arrange 
            LocationNames locationNames = new LocationNames(TestGazData1());
            // act
            IList<string> result = locationNames.Level2AllLocationNames("P1A");
            // assert
            // expected T1,T2, T1A, T2A
            Assert.AreEqual(4, result.Count);
            List<string> expected = new List<string> { "T1", "T2", "T1A", "T2A" };
            IEnumerable<string> dif = result.Except(expected);
            Assert.AreEqual(0, dif.Count());
        }

        /// <summary>
        /// given P2 and T2
        /// when gaz contains two records with P2 at level1 and T2 at level2, 
        /// containing three unique level3 names, 
        /// one record having an alt level 3 name
        /// then should return the three main level 3 names plus the alt level 3 name
        /// </summary>
        [TestMethod]
        public void Level3AllLocationNames_GazContainsMainAndAltNames_MainAndAltReturned()
        {
            // arrange 
            LocationNames locationNames = new LocationNames(TestGazData1());

            // act
            IList<string> result = locationNames.Level3AllLocationNames("P2","T2");

            // assert
            // expected V1,V2, V3, V1A
            Assert.AreEqual(4, result.Count);
            List<string> expected = new List<string> { "V1", "V2", "V3", "V1A" };
            IEnumerable<string> dif = result.Except(expected);
            Assert.AreEqual(0, dif.Count());
        }

        /// <summary>
        /// given P2A and T2A
        /// when gaz contains two records with P2 at level1 and T2 at level2, 
        /// containing three unique level3 names, 
        /// one record having an alt level 3 name
        /// then should return the three main level 3 names plus the alt level 3 name
        /// </summary>
        [TestMethod]
        public void Level3AllLocationNames_Level1And2AltGiven_MainAndAltReturned()
        {
            // arrange 
            LocationNames locationNames = new LocationNames(TestGazData1());

            // act
            IList<string> result = locationNames.Level3AllLocationNames("P2A", "T2");

            // assert
            // expected V1,V2, V3, V1A
            Assert.AreEqual(4, result.Count);
            List<string> expected = new List<string> { "V1", "V2", "V3", "V1A" };
            IEnumerable<string> dif = result.Except(expected);
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
    }
}
