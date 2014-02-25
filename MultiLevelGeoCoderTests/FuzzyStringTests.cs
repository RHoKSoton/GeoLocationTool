// FuzzyStringTests.cs

namespace MultiLevelGeoCoderTests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlServerCe;
    using System.IO;
    using System.Linq;
    using System.Text;
    using DuoVia.FuzzyStrings;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FuzzyStringTests
    {
        [TestMethod]
        public void Simple()
        {
            //Given
            string toto = "toto";
            string titi = "titi";
            double totoScore = "toto".FuzzyMatch(toto);
            double titiScore = "toto".FuzzyMatch(titi);
            Assert.IsTrue(totoScore > titiScore);
        }

        [TestMethod]
        public void ExactSubstring()
        {
            //Given
            string test = "test";
            string titi = "titi";
            double testScore = "test toto".FuzzyMatch(test);
            double titiScore = "test toto".FuzzyMatch(titi);
            Assert.IsTrue(testScore > titiScore);
        }
    }
}