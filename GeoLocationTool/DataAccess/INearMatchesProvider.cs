using GeoLocationTool.Model;
using System;
using System.Collections.Generic;

namespace GeoLocationTool.DataAccess
{
    interface INearMatchesProvider
    {
        IEnumerable<NearMatch> GetActualMatches(string near);
        void InsertMatch(string near, string actual);
    }
}
