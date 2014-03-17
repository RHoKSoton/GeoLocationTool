// GazetteerData.cs

namespace MultiLevelGeoCoder.Logic
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Holds all the gazetter data, and details of the columns to be used.
    /// Provides a subset of the data that just contains the required data.
    /// </summary>
    internal class GazetteerData
    {
        #region Constructors

        public GazetteerData(DataTable data)
        {
            Data = data;
        }

        #endregion Constructors

        #region Properties

        public GazetteerColumnNames ColumnNames { get; set; }

        public DataTable Data { get; private set; }

        /// <summary>
        /// Provides a location list containing just the columns in the Column Names.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public List<Gadm> LocationList
        {
            get
            {
                // create a new list of  just the gazetteer data that we need
                List<Gadm> locationCodeList = new List<Gadm>();
                foreach (DataRow row in Data.Rows)
                {
                    Gadm gadm = new Gadm();
                    gadm.NAME_1 = row[ColumnNames.Level1Name].ToString();
                    gadm.ID_1 = row[ColumnNames.Level1Code].ToString();
                    //gadm.VarName1 = row[AdminAltName].ToString();

                    gadm.NAME_2 = row[ColumnNames.Level2Name].ToString();
                    gadm.ID_2 = row[ColumnNames.Level2Code].ToString();
                    //gadm.VarName1 = row[AdminAltName].ToString();

                    gadm.NAME_3 = row[ColumnNames.Level3Name].ToString();
                    gadm.ID_3 = row[ColumnNames.Level3Code].ToString();
                    //gadm.VarName1 = row[AdminAltName].ToString();
                    locationCodeList.Add(gadm);
                }
                return locationCodeList;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Provides a list of all the column header names present in the gazetteer data.
        /// </summary>
        /// <returns>List of column names</returns>
        public IList<string> AllColumnNames()
        {
            List<string> list =
                (from DataColumn dataColumn in Data.Columns select dataColumn.ColumnName)
                    .ToList();

            return list;
        }

        #endregion Methods
    }
}