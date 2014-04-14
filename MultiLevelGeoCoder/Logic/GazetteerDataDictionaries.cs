// GazetteerDataDictionaries.cs

namespace MultiLevelGeoCoder.Logic
{
    using System.Collections.Generic;

    /// <summary>
    /// Holds gazetteer data in dictionaries to improve performance
    /// </summary>
    internal class GazetteerDataDictionaries
    {
        #region Fields

        private readonly IEnumerable<Gadm> gazzetteerData;

        private Dictionary<string, GeoCode> level1Dictionary;
        private Dictionary<string, GeoCode> level2Dictionary;
        private Dictionary<string, GeoCode> level3Dictionary;

        #endregion Fields

        #region Constructors

        public GazetteerDataDictionaries(IEnumerable<Gadm> gazzetteerData)
        {
            this.gazzetteerData = gazzetteerData;
            InitializeDictionaries();
        }

        #endregion Constructors

        #region Methods

        public GeoCode GetLevel1Code(string name1)
        {
            GeoCode geoCode;
            level1Dictionary.TryGetValue(name1.Trim().ToLower(), out geoCode);
            return geoCode;
        }

        public GeoCode GetLevel2Code(string name1, string name2)
        {
            GeoCode geoCode;
            level2Dictionary.TryGetValue(
                name1.Trim().ToLower() + name2.Trim().ToLower(),
                out geoCode);
            return geoCode;
        }

        public GeoCode GetLevel3Code(string name1, string name2, string name3)
        {
            GeoCode geoCode;
            level3Dictionary.TryGetValue(
                name1.Trim().ToLower() +
                name2.Trim().ToLower() +
                name3.Trim().ToLower(),
                out geoCode);
            return geoCode;
        }

        private void DictionaryLevel1UsingAlternateNames(Gadm gadm)
        {
            // P1A
            if (!string.IsNullOrEmpty(gadm.AltName1))
            {
                string altLevel1Key1 = gadm.AltName1.Trim().ToLower();
                if (!level1Dictionary.ContainsKey(altLevel1Key1))
                    level1Dictionary.Add(altLevel1Key1, new GeoCode(gadm.Id1, gadm.Name1));
            }
        }

        private void DictionaryLevel2UsingAlternateNames(Gadm gadm)
        {
            // standard name
            string level1 = gadm.Name1.Trim().ToLower();


            if (!string.IsNullOrEmpty(gadm.AltName2))
            {
                string altLevel2 = gadm.AltName2.Trim().ToLower();

                // P1 + T1A
                string altLevel2Key1 = level1 + altLevel2;
                if (!level2Dictionary.ContainsKey(altLevel2Key1))
                    level2Dictionary.Add(altLevel2Key1, new GeoCode(gadm.Id2, gadm.Name2));
            }
        }

        private void DictionaryLevel3UsingAlternateNames(Gadm gadm)
        {
            // standard names
            string level1 = gadm.Name1.Trim().ToLower();
            string level2 = gadm.Name2.Trim().ToLower();

            // alternate names
            if (!string.IsNullOrEmpty(gadm.AltName3))
            {
                string altLevel3 = gadm.AltName3.Trim().ToLower();
                //P1 + T1 + V1A
                string altLevel3Key1 = level1 + level2 + altLevel3;
                if (!level3Dictionary.ContainsKey(altLevel3Key1))
                    level3Dictionary.Add(altLevel3Key1, new GeoCode(gadm.Id3, gadm.Name3));
            }
        }

        private void InitializeDictionaries()
        {
            level1Dictionary = new Dictionary<string, GeoCode>();
            level2Dictionary = new Dictionary<string, GeoCode>();
            level3Dictionary = new Dictionary<string, GeoCode>();

            foreach (var gadm in gazzetteerData)
            {
                //P1
                string level1Key = gadm.Name1.Trim().ToLower();
                if (!level1Dictionary.ContainsKey(level1Key))
                    level1Dictionary.Add(level1Key, new GeoCode(gadm.Id1, gadm.Name1));

                // P1 + T1
                string level2Key = level1Key + gadm.Name2.Trim().ToLower();
                if (!level2Dictionary.ContainsKey(level2Key))
                    level2Dictionary.Add(level2Key, new GeoCode(gadm.Id2, gadm.Name2));

                //P1 + T1 + V1
                string level3Key = level2Key + gadm.Name3.Trim().ToLower();
                if (!level3Dictionary.ContainsKey(level3Key))
                    level3Dictionary.Add(level3Key, new GeoCode(gadm.Id3, gadm.Name3));

                // add any gazetteer alternate names too
                DictionaryLevel1UsingAlternateNames(gadm);
                DictionaryLevel2UsingAlternateNames(gadm);
                DictionaryLevel3UsingAlternateNames(gadm);
            }
        }

        #endregion Methods
    }
}