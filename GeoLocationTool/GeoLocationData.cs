// GeoLocationData.cs

namespace GeoLocationTool
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Holds the location data to match against 
    /// Provides the location codes where there are exact matches
    /// </summary>
    public class GeoLocationData
    {
        #region Fields

        private readonly IEnumerable<Gadm> locationList;

        #endregion Fields

        #region Constructors

        public GeoLocationData(IEnumerable<Gadm> locationList)
        {
            this.locationList = locationList;
        }

        public GeoLocationData()
        {
            locationList = new List<Gadm>();
        }

        #endregion Constructors

        #region Methods

        public void AddCodesToLocation(Location location)
        {
            Location location1 = location;
            var level1 = Level1Match(location1);
            if (level1 != null)
            {
                location.ProvinceCode = level1.ID_1;
                Gadm level2 = Level2Match(location1);
                if (level2 != null)
                {
                    location.MunicipalityCode = level1.ID_2;
                    Gadm level3 = Level3Match(location1);
                    if (level3 != null)
                    {
                        location.BaracayCode = level3.ID_3;
                    }
                }
            }
        }

        public IEnumerable<string> Level1List()
        {
            var levelList = locationList.Select(l => l.NAME_1);
            return levelList.Distinct();
        }

        public IEnumerable<string> Level2List()
        {
            var levelList = locationList.Select(l => l.NAME_2);
            return levelList.Distinct();
        }

        public IEnumerable<string> Level3List()
        {
            var levelList = locationList.Select(l => l.NAME_3);
            return levelList.Distinct();
        }

        private Gadm Level1Match(Location location)
        {
            // just match level 1
            var matchRecords = from record in locationList
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Province.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            return firstOrDefault;
        }

        private Gadm Level2Match(Location location)
        {
            // must match level 1 and 2
            var matchRecords = from record in locationList
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Province.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_2,
                        location.Municipality.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            return firstOrDefault;
        }

        private Gadm Level3Match(Location location)
        {
            // must match all three levels
            var matchRecords = from record in locationList
                where
                    (String.Equals(
                        record.NAME_1,
                        location.Province.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_2,
                        location.Municipality.Trim(),
                        StringComparison.OrdinalIgnoreCase)) &&
                    (String.Equals(
                        record.NAME_3,
                        location.Baracay.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                select record;

            var firstOrDefault = matchRecords.FirstOrDefault();
            return firstOrDefault;
        }

        #endregion Methods
    }
}