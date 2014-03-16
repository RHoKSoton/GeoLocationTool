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

    /// <summary>
    /// Perfs tests
    /// </summary>
    [TestClass]
    public class GeoCoderPerfsTests
    {
        #region Fields

        private DbConnection connection;
        private string dbLocation = @"GeoLocationToolTest.sdf";

        #endregion Fields

        #region Methods

        [TestCleanup]
        public void Cleanup()
        {
            connection.Close();
        }

        /// <summary>
        /// Perfs tests
        /// </summary>
        [TestMethod]
        [Ignore]
        public void GeoCoder_PerfsTests()
        {
            connection = DBHelper.GetDbConnection(dbLocation);
            connection.InitializeDB();
            GeoCoder geoCoder = new GeoCoder(connection);
            geoCoder.LoadGazetter(@"PHL_adm3.csv");//You need to copy this file manually
            geoCoder.LoadInputFileCsv(@"Sue - initial test.csv");//You need to copy this file manually, ideally with 100 or more lines
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
                });
            Debug.WriteLine("Time to create dictionaries: " + watch.Elapsed.TotalSeconds);
            watch.Restart();
            geoCoder.SetInputColumns(geoCoder.DefaultInputColumnNames());
            geoCoder.CodeAll();
            var elapsed = watch.Elapsed.TotalSeconds;
            LocationCodes.useDictionaries = !LocationCodes.useDictionaries;
            watch.Restart();
            geoCoder.CodeAll();
            Debug.WriteLine(elapsed + " vs " + watch.Elapsed.TotalSeconds);

            foreach (var row in geoCoder.InputData.AsEnumerable())
            {
                var elems = row.ItemArray;
                try
                {
                    Assert.IsFalse(elems[5] is System.DBNull);
                    Assert.IsFalse(elems[6] is System.DBNull);
                    Assert.IsFalse(elems[7] is System.DBNull);
                }
                catch (Exception)
                {
                    Console.WriteLine(row);
                }
            }
        }

        #endregion Methods
    }
}