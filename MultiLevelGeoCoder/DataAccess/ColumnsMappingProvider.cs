// ColumnsMappingProvider.cs

namespace MultiLevelGeoCoder.DataAccess
{
    using System.Data.Common;
    using System.Linq;
    using Dapper;
    using Model;

    /// <summary>
    /// Provides the saved gazetteer column selections from the database
    /// </summary>
    internal class ColumnsMappingProvider : IColumnsMappingProvider
    {
        #region Fields

        private readonly DbConnection dbConnection;

        #endregion Fields

        #region Constructors

        public ColumnsMappingProvider(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        #endregion Constructors

        #region Methods

        public GazetteerColumnsMapping GetGazetteerColumnsMapping(string fileName)
        {
            return dbConnection.Query<GazetteerColumnsMapping>(
                @"SELECT * FROM GazetteerColumnsMapping
                    WHERE FileName=@fileName",
                new {fileName}
                ).FirstOrDefault();
        }

        public void SaveGazetteerColumnsMapping(GazetteerColumnsMapping columnMapping)
        {
            bool exists = dbConnection.Query<int>(
                @"SELECT COUNT(*) FROM GazetteerColumnsMapping
                    WHERE FileName=@FileName",
                new {columnMapping.FileName}).FirstOrDefault() > 0;

            if (!exists)
            {
                dbConnection.Execute(
                    @"INSERT INTO GazetteerColumnsMapping (
                        FileName,
                        Level1Code,
                        Level1Name,
                        Level1AltName,
                        Level2Code,
                        Level2Name,
                        Level2AltName,
                        Level3Code,
                        Level3Name,
                        Level3AltName
                    ) VALUES (
                        @FileName,
                        @Level1Code,
                        @Level1Name,
                        @Level1AltName,
                        @Level2Code,
                        @Level2Name,
                        @Level2AltName,
                        @Level3Code,
                        @Level3Name,
                        @Level3AltName
                    )",
                    new
                    {
                        columnMapping.FileName,
                        columnMapping.Level1Code,
                        columnMapping.Level1Name,
                        columnMapping.Level1AltName,
                        columnMapping.Level2Code,
                        columnMapping.Level2Name,
                        columnMapping.Level2AltName,
                        columnMapping.Level3Code,
                        columnMapping.Level3Name,
                        columnMapping.Level3AltName,
                    }
                    );
            }
            else
            {
                dbConnection.Execute(
                    @"UPDATE GazetteerColumnsMapping
                        SET
                            Level1Code=@Level1Code,
                            Level1Name=@Level1Name,
                            Level1AltName=@Level1AltName,
                            Level2Code=@Level2Code,
                            Level2Name=@Level2Name,
                            Level2AltName=@Level2AltName,
                            Level3Code=@Level3Code,
                            Level3Name=@Level3Name,
                            Level3AltName=@Level3AltName
                        WHERE FileName=@FileName",
                    new
                    {
                        columnMapping.FileName,
                        columnMapping.Level1Code,
                        columnMapping.Level1Name,
                        columnMapping.Level1AltName,
                        columnMapping.Level2Code,
                        columnMapping.Level2Name,
                        columnMapping.Level2AltName,
                        columnMapping.Level3Code,
                        columnMapping.Level3Name,
                        columnMapping.Level3AltName,
                    }
                    );
            }
        }

        #endregion Methods
    }
}