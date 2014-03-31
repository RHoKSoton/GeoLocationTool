// GazetteerTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using System.Data;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Provides gazetteer test data
    /// </summary>
    internal class GazetteerTestData
    {
        #region Fields

        private readonly List<KeyValuePair<string[], string[]>> lines =
            new List<KeyValuePair<string[], string[]>>();

        #endregion Fields

        #region Methods

        public void AddLine(string[] names, string[] codes)
        {
            lines.Add(new KeyValuePair<string[], string[]>(names, codes));
        }

        public DataTable Data(GazetteerColumnNames gazetteerColumnNames)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(gazetteerColumnNames.Level1Name);
            dt.Columns.Add(gazetteerColumnNames.Level2Name);
            dt.Columns.Add(gazetteerColumnNames.Level3Name);
            dt.Columns.Add(gazetteerColumnNames.Level1Code);
            dt.Columns.Add(gazetteerColumnNames.Level2Code);
            dt.Columns.Add(gazetteerColumnNames.Level3Code);

            foreach (var line in lines)
            {
                object[] values =
                {
                    line.Key[0], line.Key[1], line.Key[2],
                    line.Value[0], line.Value[1], line.Value[2]
                };
                dt.LoadDataRow(values, true);
            }
            return dt;
        }

        public IEnumerable<Gadm> GadmList()
        {
            List<Gadm> gadmList = new List<Gadm>();
            foreach (var keyValuePair in lines)
            {
                Gadm record = new Gadm();
                record.NAME_1 = keyValuePair.Key[0];
                record.NAME_2 = keyValuePair.Key[1];
                record.NAME_3 = keyValuePair.Key[2];
                record.ID_1 = keyValuePair.Value[0];
                record.ID_2 = keyValuePair.Value[1];
                record.ID_3 = keyValuePair.Value[2];
                gadmList.Add(record);
            }

            return gadmList;
        }

        #endregion Methods
    }
}