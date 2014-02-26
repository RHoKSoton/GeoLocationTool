namespace MultiLevelGeoCoder.DataAccess
{
    using System.Data.Common;
    using System.Linq;
    using Dapper;
    using Model;

    /// <summary>
    /// Get or save Columns Mapping
    /// </summary>
    public class ColumnsMappingProvider : IColumnsMappingProvider
    {
        public DbConnection SqlConnection { get; set; }

        public ColumnsMappingProvider(DbConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }

        public LocationColumnsMapping GetLocationColumnsMapping(string fileName)
        {
            return SqlConnection.Query<LocationColumnsMapping>(
                @"SELECT * FROM LocationColumnsMapping
                    WHERE FileName=@fileName",
                new { fileName }
            ).FirstOrDefault();
        }

        public void SaveLocationColumnsMapping(LocationColumnsMapping columnMapping)
        {
            bool exists = SqlConnection.Query<int>(
                @"SELECT COUNT(*) FROM LocationColumnsMapping
                    WHERE FileName=@FileName",
                new { columnMapping.FileName }).FirstOrDefault() > 0;

            if (!exists)
            {
                SqlConnection.Execute(
                    @"INSERT INTO LocationColumnsMapping (
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
                    new {
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
                SqlConnection.Execute(
                    @"UPDATE LocationColumnsMapping
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
                    new {
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
    }
}
