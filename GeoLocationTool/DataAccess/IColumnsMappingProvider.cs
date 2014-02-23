using GeoLocationTool.Model;
using System;
using System.Collections.Generic;

namespace GeoLocationTool.DataAccess
{
    interface IColumnsMappingProvider
    {
        LocationColumnsMapping GetLocationColumnsMapping(string fileName);
        void SaveLocationColumnsMapping(LocationColumnsMapping columnMapping);
    }
}
