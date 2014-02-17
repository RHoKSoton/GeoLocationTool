// InputData.cs

namespace GeoLocationTool.Logic
{
    using System;
    using System.Data;

    /// <summary>
    /// Holds the input data and added codes
    /// </summary>
    public class InputData
    {
        #region Fields

        public const string Level1CodeColumnName = "ProvinceCode";
        public const string Level2CodeColumnName = "MunicipalityCode";
        public const string Level3CodeColumnName = "BaracayCode";

        public DataTable dt;

        #endregion Fields

        #region Properties

        public int ColumnIndexLoc1 { get; set; }

        public int ColumnIndexLoc2 { get; set; }

        public int ColumnIndexLoc3 { get; set; }

        #endregion Properties

        #region Methods

        public void AddCodeCollumns()
        {
            if (!dt.Columns.Contains(Level1CodeColumnName))
            {
                dt.Columns.Add(Level1CodeColumnName, typeof (String));
            }

            if (!dt.Columns.Contains(Level2CodeColumnName))
            {
                dt.Columns.Add(Level2CodeColumnName, typeof (String));
            }

            if (!dt.Columns.Contains(Level3CodeColumnName))
            {
                dt.Columns.Add(Level3CodeColumnName, typeof (String));
            }
        }

        /// <summary>
        /// Adds the location codes to the input data records.
        /// </summary>
        /// <param name="locationData">The location data.</param>
        public void AddLocationCodes(LocationData locationData)
        {
            foreach (DataRow dataRow in dt.Rows)
            {
                //create location
                Location location = new Location();
                location.Province =
                    dataRow.ItemArray[ColumnIndexLoc1].ToString();
                location.Municipality =
                    dataRow.ItemArray[ColumnIndexLoc2].ToString();
                location.Barangay =
                    dataRow.ItemArray[ColumnIndexLoc3].ToString();

                // get codes
                locationData.GetLocationCodes(location);

                //add codes
                dataRow[Level1CodeColumnName] = location.ProvinceCode;
                dataRow[Level2CodeColumnName] = location.MunicipalityCode;
                dataRow[Level3CodeColumnName] = location.BarangayCode;
                dataRow.AcceptChanges();
            }
            dt.AcceptChanges();
        }

        /// <summary>
        /// Gets the unmatched records.
        /// </summary>
        /// <returns>A view only containing records where one or more location codes is missing.</returns>
        public DataView GetUnmatchedRecords()
        {
            // only show those records where a location code is null
            EnumerableRowCollection<DataRow> query = from record in dt.AsEnumerable()
                where record.Field<String>(Level1CodeColumnName) == null ||
                      record.Field<string>(Level2CodeColumnName) == null ||
                      record.Field<string>(Level3CodeColumnName) == null
                select record;

            DataView unmatched = query.AsDataView();
            return unmatched;
        }

        #endregion Methods
    }
}