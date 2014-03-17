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

        /// <summary>
        /// Perfs tests
        /// </summary>
        
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

        [TestMethod]
        [Ignore]
        public void GeoCoder_PerfsTests()
        {
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            GeoCoder geoCoder = new GeoCoder(connection);
            geoCoder.LoadGazetter(@"PHL_adm3.csv");//You need to copy this file manually
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
                geoCoder.CodeAll();
                var elapsed = watch.Elapsed.TotalSeconds;
                LocationCodes.useDictionaries = !LocationCodes.useDictionaries;
                watch.Restart();
                geoCoder.CodeAll();
                Debug.WriteLine(linesCount + " input lines: " + elapsed + " vs " + watch.Elapsed.TotalSeconds);
                LocationCodes.useDictionaries = !LocationCodes.useDictionaries;

                foreach (var row in geoCoder.InputData.AsEnumerable())
                {
                    var elems = row.ItemArray;
                    Assert.IsFalse(elems[5] is System.DBNull);
                    Assert.IsFalse(elems[6] is System.DBNull);
                    Assert.IsFalse(elems[7] is System.DBNull);
                }
            }
        }

        #endregion Methods
    }
}