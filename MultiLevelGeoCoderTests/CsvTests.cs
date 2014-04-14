
namespace MultiLevelGeoCoderTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultiLevelGeoCoder.Logic;
    using MultiLevelGeoCoder.DataAccess;

    [TestClass]
    public class CsvTests
    {
        [TestMethod]
        public void LoadInputCSV()
        {
            //Given
            string path = Path.GetFullPath(@"input.csv");

            //When
            var dataTable = FileImport.ReadCsvFile(path, true, ",");

            //Then
            Assert.AreEqual(2, dataTable.Rows.Count);
            Assert.AreEqual("MARINDUQUE", dataTable.Rows[0][0]);
            Assert.AreEqual("BOAC", dataTable.Rows[0][1]);
            Assert.AreEqual("MARINDUQUE", dataTable.Rows[1][0]);
            Assert.AreEqual("BO,AC", dataTable.Rows[1][1]);
        }

        [TestMethod]
        public void LoadInputTabDelim()
        {
            //Given
            string path = Path.GetFullPath(@"inputTabs.csv");

            //When
            var dataTable = FileImport.ReadCsvFile(path, true, "\t");

            //Then
            Assert.AreEqual(2, dataTable.Rows.Count);
            Assert.AreEqual("MARINDUQUE", dataTable.Rows[0][0]);
            Assert.AreEqual("BOAC", dataTable.Rows[0][1]);
            Assert.AreEqual("MARINDUQUE", dataTable.Rows[1][0]);
            Assert.AreEqual("BO\tAC", dataTable.Rows[1][1]);
        }
    }
}
