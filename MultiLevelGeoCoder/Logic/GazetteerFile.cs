// GazetteerFile.cs

namespace MultiLevelGeoCoder.Logic
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Holds all the gazetter data, and details of the columns to be used.
    /// Provides a subset of the data that just contains the required data.
    /// </summary>
    public class GazetteerFile
    {
        #region Constructors

        public GazetteerFile(DataTable data)
        {
            Data = data;
        }

        #endregion Constructors

        #region Properties

        public int Admin1AltName { get; set; }

        // column header indices
        // todo use column names instead
        public int Admin1Code { get; set; }

        public int Admin1Name { get; set; }

        public int Admin2AltName { get; set; }

        public int Admin2Code { get; set; }

        public int Admin2Name { get; set; }

        public int Admin3AltName { get; set; }

        public int Admin3Code { get; set; }

        public int Admin3Name { get; set; }

        public DataTable Data { get; private set; }

        /// <summary>
        /// Gets the location list containing just the location columns specified.
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
                    gadm.NAME_1 = row[Admin1Name].ToString();
                    gadm.ID_1 = row[Admin1Code].ToString();
                    //gadm.VarName1 = row[AdminAltName].ToString();

                    gadm.NAME_2 = row[Admin2Name].ToString();
                    gadm.ID_2 = row[Admin2Code].ToString();
                    //gadm.VarName1 = row[AdminAltName].ToString();

                    gadm.NAME_3 = row[Admin3Name].ToString();
                    gadm.ID_3 = row[Admin3Code].ToString();
                    //gadm.VarName1 = row[AdminAltName].ToString();
                    locationCodeList.Add(gadm);
                }
                return locationCodeList;
            }
        }

        #endregion Properties

        #region Methods

        public bool ColumnIndicesValid()
        {
            // code column indices must be greater than 0
            bool valid = (Admin1Code > 0);
            valid = valid & (Admin2Code > 0);
            valid = valid & (Admin3Code > 0);

            // name column indices must be greater than 0
            valid = valid & (Admin1Name > 0);
            valid = valid & (Admin2Name > 0);
            valid = valid & (Admin2Name > 0);

            return valid;
        }

        #endregion Methods
    }
}