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

        private static readonly GeoCode P1 = new GeoCode("1", "P1");
        private static readonly GeoCode P1T1 = new GeoCode("10", "T1");
        private static readonly GeoCode P1V1 = new GeoCode("100", "V1");
        private static readonly GeoCode P1V2 = new GeoCode("101", "V2");
        private static readonly GeoCode P2 = new GeoCode("2", "P2");
        private static readonly GeoCode P2T2 = new GeoCode("20", "T2");
        private static readonly GeoCode P2V1 = new GeoCode("200", "V1");
        private static readonly GeoCode P2V2 = new GeoCode("300", "V2");
        private static readonly GeoCode P2V3 = new GeoCode("400", "V3");

        private readonly List<KeyValuePair<string[], string[]>> lines2 =
            new List<KeyValuePair<string[], string[]>>();

        #endregion Fields

        #region Methods

        public static IEnumerable<Gadm> TestData(Gadm gadmRecord)
        {
            List<Gadm> gadmList = new List<Gadm>();
            gadmList.Add(gadmRecord);
            return gadmList;
        }

        public void AddLine(string[] names, string[] codes)
        {
            lines2.Add(new KeyValuePair<string[], string[]>(names, codes));
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

            foreach (var line in lines2)
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

        internal static Gadm Record1()
        {
            return new Gadm
            {
                ID_1 = P1.Code,
                NAME_1 = P1.Name,
                ID_2 = P1T1.Code,
                NAME_2 = P1T1.Name,
                ID_3 = P1V1.Code,
                NAME_3 = P1V1.Name
            };
        }

        internal static Gadm Record2()
        {
            return new Gadm
            {
                ID_1 = P1.Code,
                NAME_1 = P1.Name,
                ID_2 = P1T1.Code,
                NAME_2 = P1T1.Name,
                ID_3 = P1V2.Code,
                NAME_3 = P1V2.Name
            };
        }

        internal static Gadm Record3()
        {
            return new Gadm
            {
                ID_1 = P2.Code,
                NAME_1 = P2.Name,
                ID_2 = P2T2.Code,
                NAME_2 = P2T2.Name,
                ID_3 = P2V1.Code,
                NAME_3 = P2V1.Name
            };
        }

        internal static Gadm Record4()
        {
            return new Gadm
            {
                ID_1 = P2.Code,
                NAME_1 = P2.Name,
                ID_2 = P2T2.Code,
                NAME_2 = P2T2.Name,
                ID_3 = P2V2.Code,
                NAME_3 = P2V2.Name
            };
        }

        internal static Gadm Record5()
        {
            return new Gadm
            {
                ID_1 = P2.Code,
                NAME_1 = P2.Name,
                ID_2 = P2T2.Code,
                NAME_2 = P2T2.Name,
                ID_3 = P2V3.Code,
                NAME_3 = P2V3.Name
            };
        }

        internal static IList<Gadm> TestData()
        {
            List<Gadm> gadmList = new List<Gadm>();

            gadmList.Add(Record1());
            gadmList.Add(Record2());
            gadmList.Add(Record3());
            gadmList.Add(Record4());
            gadmList.Add(Record5());

            return gadmList;
        }

        #endregion Methods
    }
}