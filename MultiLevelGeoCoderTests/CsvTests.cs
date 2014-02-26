
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
        public void LoadGadmCSV()
        {
            List<Gadm> gadmList;

            //Given
            string csv = @"""PID"",""ID_0"",""ISO"",""NAME_0"",""ID_1"",""NAME_1"",""ID_2"",""NAME_2"",""ID_3"",""NAME_3"",""NL_NAME_3"",""VARNAME_3"",""TYPE_3"",""ENGTYPE_3""" + Environment.NewLine
                       + @"50863,177,""PHL"",""Philippines"",1,""Abra"",16,""Manabo"",172,""San Jose Norte"","""","""",""Barangay"",""Village""" + Environment.NewLine
                       + @"50864,177,""PHL"",""Philippines"",1,""Abra"",16,""Manabo"",173,""San Jose Sur"","""","""",""Barangay"",""Village""" + Environment.NewLine;

            //When
            using (var sr = new StringReader(csv))
            {
                var csvReader = new CsvReader(sr);
                gadmList = csvReader.GetRecords<Gadm>().ToList();
            }

            //Then
            Assert.AreEqual(2, gadmList.Count);
            Assert.AreEqual("Abra", gadmList.First().NAME_1);
            Assert.AreEqual("Abra", gadmList.Last().NAME_1);
            Assert.AreEqual("Manabo", gadmList.First().NAME_2);
            Assert.AreEqual("Manabo", gadmList.Last().NAME_2);
            Assert.AreEqual("San Jose Norte", gadmList.First().NAME_3);
            Assert.AreEqual("San Jose Sur", gadmList.Last().NAME_3);
        }
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
