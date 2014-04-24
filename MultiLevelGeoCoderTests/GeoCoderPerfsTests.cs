// GeoCoderPerfsTests.cs
namespace MultiLevelGeoCoderTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder;
    using MultiLevelGeoCoder.DataAccess;
    using MultiLevelGeoCoder.Logic;
    using System.Diagnostics;
    using System.Data.Common;
    using System.Data;
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Perfs tests
    /// </summary>
    [TestClass]
    public class GeoCoderPerfsTests
    {
        #region Fields

        private DbConnection connection;
        private string dbLocation = @"GeoLocationToolTest.sdf";
        private string inputFileLocation = Path.GetTempFileName();

        #endregion Fields

        #region Methods

        [TestCleanup]
        public void Cleanup()
        {
            connection.Close();
            File.Delete(inputFileLocation);
        }

        private string GenerateInputFile(int linesCount)
        {
            var lines = new List<string>();
            lines.Add("Admin1,Admin2,Admin3,Admin4,Population2010");
            File.WriteAllLines(inputFileLocation, lines.Concat(Enumerable.Range(1, linesCount)
                                                                         .Select(x => "REG04B,MARINDUQUE ,BOAC,Agot,502")));
            return inputFileLocation;
        }

        // Example results
        // Assumption : there are much more exact match than there are saved matches
        // All values are time in seconds (using dictionares vs no dictionary)
        // Time to create dictionaries: 0,2403948

        //Looking first in MatchedNames
        //500: 1,258668 vs 5,9540653
        //1000: 2,18586 vs 11,8857587
        //2000: 4,4386 vs 23,8948473

        //Looking first in Gazetteer
        //500: 0,0162152 vs 4,5657064
        //1000: 0,016625 vs 9,079276
        //2000: 0,0510697 vs 18,1356993

        //From 500 to 2000 lines : can be from 370 to 500 times faster if using dictionaries and looking first in Gazetteer

        // Using matched names cache v not using cache (looking first in MatchedNames)
        // with a GeoLocationToolTest.sdf file containing 9 level3 entries, 1 level1 entry and 1 level2 entry
        // 500 input lines: 0.0320834 vs 0.6627464
        // 1000 input lines: 0.0174101 vs 1.3510093
        // 2000 input lines: 0.0992199 vs 2.6536493
        // 100000 input lines: 1.0631592 vs 120.1221388

        [TestMethod]
        [Ignore]
        public void GeoCoder_PerfsTests()
        {
            // This test creates an empty GeoLocationToolTest.sdf file from which to load the cache
            // To use a file containing entries, manually create and copy the file
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            GeoCoder geoCoder = new GeoCoder(connection);
            geoCoder.LoadGazetteerFile(@"PHL_adm3.csv");//You need to copy this file manually
            Stopwatch watch = new Stopwatch();
            watch.Start();
            geoCoder.SetGazetteerColumns(new GazetteerColumnNames
            {
                Level1Code = "ID_1",
                Level2Code = "ID_2",
                Level3Code = "ID_3",
                Level1Name = "NAME_1",
                Level2Name = "NAME_2",
                Level3Name = "NAME_3"
            }, false);

            Debug.WriteLine("Time to create dictionaries: " + watch.Elapsed.TotalSeconds);

            foreach (int linesCount in new[] { 500, 1000, 2000 })
            {
                geoCoder.LoadInputFileCsv(GenerateInputFile(linesCount));
                geoCoder.SetInputColumns(geoCoder.DefaultInputColumnNames());
                watch.Restart();
                geoCoder.AddAllLocationCodes();
                var elapsed = watch.Elapsed.TotalSeconds;
                LocationCodes.useDictionaries = !LocationCodes.useDictionaries;
                InputData.UseMatchedNamesCache = !InputData.UseMatchedNamesCache;
                watch.Restart();
                geoCoder.AddAllLocationCodes();
                Debug.WriteLine(linesCount + " input lines: " + elapsed + " vs " + watch.Elapsed.TotalSeconds);
                LocationCodes.useDictionaries = !LocationCodes.useDictionaries;
                InputData.UseMatchedNamesCache = !InputData.UseMatchedNamesCache;

                foreach (var row in geoCoder.InputData.AsEnumerable())
                {
                    var elems = row.ItemArray;
                    Assert.IsFalse(elems[5] is System.DBNull);
                    Assert.IsFalse(elems[6] is System.DBNull);
                    Assert.IsFalse(elems[7] is System.DBNull);
                }
            }
        }

        /// <summary>
        /// Given pre created gazetteer and input csv files and an exising database
        /// When GeoCoder code all is run
        /// The time taken and the number of input lines is obtained
        /// </summary>
        [TestMethod]
        [Ignore]
        public void GeoCoderCodeAll_UseActualData_PerfsTests()
        {
            // Manually create and copy the GeoLocationTool.sdf file 
            // Manually create and copy the input and gazetteer files
            const string dbLocation1 = @"GeoLocationTool.sdf";
            connection = DBHelper.GetDbConnection(dbLocation1);
            // connection.InitializeDB();
            GeoCoder geoCoder = new GeoCoder(connection);
            geoCoder.LoadGazetteerFile(@"TestGaz1.csv");// You need to copy this file manually
            Stopwatch watch = new Stopwatch();
            geoCoder.SetGazetteerColumns(new GazetteerColumnNames
            {
                Level1Code = "ID_1",
                Level2Code = "ID_2",
                Level3Code = "ID_3",
                Level1Name = "NAME_1",
                Level2Name = "NAME_2",
                Level3Name = "NAME_3",
                Level1AltName = "VARNAME_1",
                Level2AltName = "VARNAME_2",
                Level3AltName = "VARNAME_3"
            }, false);

            geoCoder.LoadInputFileCsv("TestInput1.csv"); // You need to copy this file manually
            geoCoder.SetInputColumns(geoCoder.DefaultInputColumnNames());
            watch.Start();
            geoCoder.AddAllLocationCodes();
            Debug.WriteLine(geoCoder.InputData.Rows.Count + " input lines: " + watch.Elapsed.TotalSeconds);

            // Example results
            // 25 input lines: 0.072016
        }

        #endregion Methods
    }
}