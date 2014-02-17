// LocationGadmFile.cs

namespace GeoLocationTool.DataAccess
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using Logic;

    /// <summary>
    /// Read the location data from file
    /// </summary>
    internal class LocationGadmFile
    {
        #region Methods

        internal static IEnumerable<Gadm> ReadLocationFile(string path)
        {
            IEnumerable<Gadm> gadmList;

            // open for read only
            using (
                FileStream fileStream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read))
            {
                // this data is a known format so we can use a strongly typed reader
                using (
                    var csvReader = new CsvReader(new StreamReader(fileStream)))
                {
                    gadmList = csvReader.GetRecords<Gadm>().ToList();
                }
            }
            return gadmList;
        }

        #endregion Methods
    }
}