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

        internal const string code1 = "1";
        internal const string code2 = "15";
        internal const string code3 = "150";
        internal const string name1 = "TestProvince";
        internal const string name2 = "TestTown";
        internal const string name3 = "TestVillage";

        #endregion Fields

        #region Methods

        internal static IList<Gadm> TestData()
        {
            List<Gadm> gadmList = new List<Gadm>();

            gadmList.Add(
                new Gadm
                {
                    ID_1 = "1",
                    NAME_1 = "Abra",
                    ID_2 = "18",
                    NAME_2 = "Pidigan",
                    ID_3 = "188",
                    NAME_3 = "Alinaya",
                    VARNAME_3 = ""
                });

            gadmList.Add(
                new Gadm
                {
                    ID_1 = "1",
                    NAME_1 = "Abra",
                    ID_2 = "17",
                    NAME_2 = "PeÃ±arrubia",
                    ID_3 = "181",
                    NAME_3 = "Malamsit",
                    VARNAME_3 = "Pau-Malamsit"
                });

            gadmList.Add(
                new Gadm
                {
                    ID_1 = "9",
                    NAME_1 = "Basilan",
                    ID_2 = "132",
                    NAME_2 = "Tipo-Tipo",
                    ID_3 = "3018",
                    NAME_3 = "Tipo-Tipo Proper",
                    VARNAME_3 = "Poblacion"
                });

            gadmList.Add(
                new Gadm
                {
                    ID_1 = code1,
                    NAME_1 = name1,
                    ID_2 = code2,
                    NAME_2 = name2,
                    ID_3 = code3,
                    NAME_3 = name3,
                });
            return gadmList;
        }

        #endregion Methods
    }
}