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

        private const string KeySeperator = "|";

        private readonly IEnumerable<GazetteerRecord> gazzetteerData;

        private Dictionary<string, GeoCode> level1Dictionary;
        private Dictionary<string, GeoCode> level2Dictionary;
        private Dictionary<string, GeoCode> level3Dictionary;

        #endregion Fields

        #region Constructors

        public GazetteerDataDictionaries(IEnumerable<GazetteerRecord> gazzetteerData)
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
                name1.Trim().ToLower() + KeySeperator + name2.Trim().ToLower(),
                out geoCode);
            return geoCode;
        }

        public GeoCode GetLevel3Code(string name1, string name2, string name3)
        {
            GeoCode geoCode;
            level3Dictionary.TryGetValue(
                name1.Trim().ToLower() + KeySeperator +
                name2.Trim().ToLower() + KeySeperator +
                name3.Trim().ToLower(),
                out geoCode);
            return geoCode;
        }

        private void DictionaryLevel1UsingAlternateNames(GazetteerRecord gazetteerRecord)
        {
            // P1A
            if (!string.IsNullOrEmpty(gazetteerRecord.AltName1))
            {
                string altLevel1Key1 = gazetteerRecord.AltName1.Trim().ToLower();
                if (!level1Dictionary.ContainsKey(altLevel1Key1))
                    level1Dictionary.Add(
                        altLevel1Key1,
                        new GeoCode(gazetteerRecord.Id1, gazetteerRecord.Name1));
            }
        }

        private void DictionaryLevel2UsingAlternateNames(GazetteerRecord gazetteerRecord)
        {
            // standard name
            string level1 = gazetteerRecord.Name1.Trim().ToLower();

            if (!string.IsNullOrEmpty(gazetteerRecord.AltName2))
            {
                string altLevel2 = gazetteerRecord.AltName2.Trim().ToLower();

                // P1 + T1A
                string altLevel2Key1 = level1 + KeySeperator + altLevel2;
                if (!level2Dictionary.ContainsKey(altLevel2Key1))
                    level2Dictionary.Add(
                        altLevel2Key1,
                        new GeoCode(gazetteerRecord.Id2, gazetteerRecord.Name2));
            }
        }

        private void DictionaryLevel3UsingAlternateNames(GazetteerRecord gazetteerRecord)
        {
            // standard names
            string level1 = gazetteerRecord.Name1.Trim().ToLower();
            string level2 = gazetteerRecord.Name2.Trim().ToLower();

            // alternate names
            if (!string.IsNullOrEmpty(gazetteerRecord.AltName3))
            {
                string altLevel3 = gazetteerRecord.AltName3.Trim().ToLower();
                //P1 + T1 + V1A
                string altLevel3Key1 = level1 + KeySeperator + level2 + KeySeperator +
                                       altLevel3;
                if (!level3Dictionary.ContainsKey(altLevel3Key1))
                    level3Dictionary.Add(
                        altLevel3Key1,
                        new GeoCode(gazetteerRecord.Id3, gazetteerRecord.Name3));
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
                string level2Key = level1Key + KeySeperator + gadm.Name2.Trim().ToLower();
                if (!level2Dictionary.ContainsKey(level2Key))
                    level2Dictionary.Add(level2Key, new GeoCode(gadm.Id2, gadm.Name2));

                //P1 + T1 + V1
                string level3Key = level2Key + KeySeperator + gadm.Name3.Trim().ToLower();
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