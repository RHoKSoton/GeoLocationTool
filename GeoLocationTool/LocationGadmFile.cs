using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationTool
{
    using System.IO;
    using CsvHelper;

    /// <summary>
    /// Read the location data from file
    /// </summary>
     internal class LocationGadmFile
    {
         internal static IEnumerable<Gadm> ReadLocationFile(string path)
        {
            IEnumerable<Gadm> gadmList;

            // open for read only   
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
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
    }
}
