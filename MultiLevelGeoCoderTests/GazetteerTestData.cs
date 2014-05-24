// GazetteerTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using MultiLevelGeoCoder.Logic;

    /// <summary>
    /// Gazetteer test data set including main and alternate names
    /// </summary>
    internal class GazetteerTestData
    {
        #region Fields

        // Test Gazetteer Data
        private static readonly string[] AltNames1 = {"P1A", "T1A", "V1A"};
        private static readonly string[] AltNames3 = {"P2A", "T2A", "V1A"};
        private static readonly string[] AltNames4 = {"P1A", "T2A", "V1A"};
        private static readonly string[] AltNamesEmpty = {"", "", ""};

        // we dont care about the codes for these tests
        private static readonly string[] Codes0 = {"0", "0", "0"};

        private static readonly string[] Names1 = {"P1", "T1", "V1"};
        private static readonly string[] Names2 = {"P1", "T1", "V2"};
        private static readonly string[] Names3 = {"P2", "T2", "V1"};
        private static readonly string[] Names4 = {"P2", "T2", "V2"};
        private static readonly string[] Names5 = {"P2", "T2", "V3"};
        private static readonly string[] Names6 = {"P1", "T2", "V1"};
        private static readonly string[] Names7 = {"P1", "T2", "V4"};
        private static readonly string[] Names8 = {"P2", "T1", "V2"};

        #endregion Fields

        #region Methods

        public static List<GazetteerRecord> TestData1()
        {
            GazetteerRecords gazetteerRecords = new GazetteerRecords();
            // P1, T1, V1, P1A, T1A, V1A
            gazetteerRecords.AddLine(Names1, Codes0, AltNames1);
            // P1, T1, V2, "","",""
            gazetteerRecords.AddLine(Names2, Codes0, AltNamesEmpty);
            // P2, T2, V1, P2A, T2A, V1A
            gazetteerRecords.AddLine(Names3, Codes0, AltNames3);
            // P2, T2, V2,
            gazetteerRecords.AddLine(Names4, Codes0);
            // P2, T2, V3,
            gazetteerRecords.AddLine(Names5, Codes0);
            // P1, T2, V1, P1A, T2A, V1A,
            gazetteerRecords.AddLine(Names6, Codes0, AltNames4);
            // P1, T2, V4
            gazetteerRecords.AddLine(Names7, Codes0);
            // P2, T1, V2
            gazetteerRecords.AddLine(Names8, Codes0);

            return gazetteerRecords.GadmList();
        }

        #endregion Methods
    }
}