// GazetteerTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Provides gazetteer test data
    /// </summary>
    internal static class GazetteerTestData
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

        #endregion Fields

        #region Methods

        public static IEnumerable<Gadm> TestData(Gadm gadmRecord)
        {
            List<Gadm> gadmList = new List<Gadm>();
            gadmList.Add(gadmRecord);
            return gadmList;
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