namespace MultiLevelGeoCoder.DataAccess
{
    using Model;

    public interface IColumnsMappingProvider
    {
        LocationColumnsMapping GetLocationColumnsMapping(string fileName);
        void SaveLocationColumnsMapping(LocationColumnsMapping columnMapping);
    }
}
