// GazetteerTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Provides gazetteer test data
    /// </summary>
    internal class GazetteerTestData
    {
        #region Fields

        private readonly List<Tuple<string[], string[], string[]>> lines =
            new List<Tuple<string[], string[], string[]>>();

        #endregion Fields

        #region Methods

        public void AddLine(string[] names, string[] codes, string[] altNames= null)
        {
            lines.Add(new Tuple<string[], string[], string[]>(names, codes, altNames));
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
                    line.Item1[0], line.Item1[1], line.Item1[2],
                    line.Item2[0], line.Item2[1], line.Item2[2]
                };
                dt.LoadDataRow(values, true);
            }
            return dt;
        }

        public List<GazetteerRecord> GadmList()
        {
            List<GazetteerRecord> gadmList = new List<GazetteerRecord>();
            foreach (var line in lines)
            {
                GazetteerRecord record = new GazetteerRecord();
                record.Name1 = line.Item1[0];
                record.Name2 = line.Item1[1];
                record.Name3 = line.Item1[2];
                record.Id1 = line.Item2[0];
                record.Id2 = line.Item2[1];
                record.Id3 = line.Item2[2];
                if (line.Item3 != null)
                {
                    record.AltName1 = line.Item3[0];
                    record.AltName2 = line.Item3[1];
                    record.AltName3 = line.Item3[2];
                }
               
                gadmList.Add(record);
            }

            return gadmList;
        }

        #endregion Methods
    }
}