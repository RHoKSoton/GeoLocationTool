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
                        Location1Code,
                        Location1Name,
                        Location1AltName,
                        Location2Code,
                        Location2Name,
                        Location2AltName,
                        Location3Code,
                        Location3Name,
                        Location3AltName
                    ) VALUES (
                        @FileName,
                        @Location1Code,
                        @Location1Name,
                        @Location1AltName,
                        @Location2Code,
                        @Location2Name,
                        @Location2AltName,
                        @Location3Code,
                        @Location3Name,
                        @Location3AltName
                    )",
                    new {
                        columnMapping.FileName,
                        columnMapping.Location1Code,
                        columnMapping.Location1Name,
                        columnMapping.Location1AltName,
                        columnMapping.Location2Code,
                        columnMapping.Location2Name,
                        columnMapping.Location2AltName,
                        columnMapping.Location3Code,
                        columnMapping.Location3Name,
                        columnMapping.Location3AltName,
                    }
                );
            }
            else
            {
                SqlConnection.Execute(
                    @"UPDATE LocationColumnsMapping
                        SET
                            Location1Code=@Location1Code,
                            Location1Name=@Location1Name,
                            Location1AltName=@Location1AltName,
                            Location2Code=@Location2Code,
                            Location2Name=@Location2Name,
                            Location2AltName=@Location2AltName,
                            Location3Code=@Location3Code,
                            Location3Name=@Location3Name,
                            Location3AltName=@Location3AltName
                        WHERE FileName=@FileName",
                    new {
                        columnMapping.FileName,
                        columnMapping.Location1Code,
                        columnMapping.Location1Name,
                        columnMapping.Location1AltName,
                        columnMapping.Location2Code,
                        columnMapping.Location2Name,
                        columnMapping.Location2AltName,
                        columnMapping.Location3Code,
                        columnMapping.Location3Name,
                        columnMapping.Location3AltName,
                    }
                );
            }
        }
    }
}
