// GeoCoderCodeAllTests.cs

namespace MultiLevelGeoCoderTests
{
    using System.Data;
    using System.Data.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.Logic;
    using Rhino.Mocks;

    /// <summary>
    /// Exercises the GeoCoder class Code All method
    /// </summary>
    [TestClass]
    public class GeoCoderCodeAllTests
    {
        #region Methods

        /// <summary>
        /// Given input containing lines with either all correct names or all miss-spelt names
        /// When the correct names are in the gazetteer and the miss-spelt names are in the match provider
        /// Then the correct codes are applied for both correct and miss-spelt names
        /// </summary>
        [TestMethod]
        public void
            CodeAll_SavedMatchesForMissSpellings_CodesAddedForBothMisspeltAndCorrectNames(
            )
        {
            // arrange
            GeoCoder geoCoder =
                new GeoCoder(MockRepository.GenerateStub<IDbConnection>() as DbConnection);
            InputColumnNames inputColumnNames = InputColumnNames();
            GazetteerColumnNames gazetteerColumnNames = GazetteerColumnNames();

            // create input test data with
            // line 1, all names correct
            // line 2, all names correct
            // line 3, all names miss-spelt
            string[] names1 = {"P1", "T1", "V1"};
            string[] names2 = {"P2", "T2", "V2"};
            string[] names3 = {"P1x", "T1x", "V1x"};
            string[] codes1 = {"1", "10", "100"};
            string[] codes2 = {"2", "20", "200"};

            InputTestData inputTestData = new InputTestData();
            inputTestData.AddLine(names1);
            inputTestData.AddLine(names2);
            inputTestData.AddLine(names3);
            geoCoder.SetInputData(inputTestData.Data(inputColumnNames));
            geoCoder.SetInputColumns(inputColumnNames);

            // create gazetteer data
            GazetteerTestData gazetteerTestData = new GazetteerTestData();
            gazetteerTestData.AddLine(names1, codes1);
            gazetteerTestData.AddLine(names2, codes2);
            geoCoder.SetGazetteerData(gazetteerTestData.Data(gazetteerColumnNames));

            // add records matching input line 3 names to gazetteer names 1
            MatchProviderTestData matchProviderTestData = new MatchProviderTestData();
            matchProviderTestData.AddLevel1(
                names3,
                names1);

            matchProviderTestData.AddLevel2(
                names3,
                names1);

            matchProviderTestData.AddLevel3(
                names3,
                names1);

            geoCoder.SetMatchProvider(matchProviderTestData.Data());

            geoCoder.SetGazetteerColumns(gazetteerColumnNames, false);

            // act
            geoCoder.CodeAll();

            // assert
            var columns = geoCoder.CodeColumnNames();

            //line 1 - should contain codes 1
            DataRow line1 = geoCoder.InputData.Rows[0];
            Assert.AreEqual(line1[columns.Level1], codes1[0]);
            Assert.AreEqual(line1[columns.Level2], codes1[1]);
            Assert.AreEqual(line1[columns.Level3], codes1[2]);

            //line 2 - should contain codes 2
            DataRow line2 = geoCoder.InputData.Rows[1];
            Assert.AreEqual(line2[columns.Level1], codes2[0]);
            Assert.AreEqual(line2[columns.Level2], codes2[1]);
            Assert.AreEqual(line2[columns.Level3], codes2[2]);

            //line 3 - should contain codes 1
            DataRow line3 = geoCoder.InputData.Rows[2];
            Assert.AreEqual(line3[columns.Level1], codes1[0]);
            Assert.AreEqual(line3[columns.Level2], codes1[1]);
            Assert.AreEqual(line3[columns.Level3], codes1[2]);
        }

        private static GazetteerColumnNames GazetteerColumnNames()
        {
            GazetteerColumnNames gazetteerColumnNames = new GazetteerColumnNames();
            gazetteerColumnNames.Level1Code = "ID1";
            gazetteerColumnNames.Level2Code = "ID2";
            gazetteerColumnNames.Level3Code = "ID3";
            gazetteerColumnNames.Level1Name = "Name1";
            gazetteerColumnNames.Level2Name = "Name2";
            gazetteerColumnNames.Level3Name = "Name3";
            return gazetteerColumnNames;
        }

        private static InputColumnNames InputColumnNames()
        {
            InputColumnNames inputColumnNames = new InputColumnNames();
            inputColumnNames.Level1 = "admin1";
            inputColumnNames.Level2 = "admin2";
            inputColumnNames.Level3 = "admin3";
            return inputColumnNames;
        }

        #endregion Methods
    }
}