// IColumnsMappingProvider.cs

namespace MultiLevelGeoCoder.DataAccess
{
    using Model;

    /// <summary>
    /// Provides the saved gazetteer column selections from the database
    /// </summary>
    internal interface IColumnsMappingProvider
    {
        #region Methods

        GazetteerColumnsMapping GetGazetteerColumnsMapping(string fileName);

        void SaveGazetteerColumnsMapping(GazetteerColumnsMapping columnMapping);

        #endregion Methods
    }
}