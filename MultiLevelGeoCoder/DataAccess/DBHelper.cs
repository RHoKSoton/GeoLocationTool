namespace MultiLevelGeoCoder.DataAccess
{
    using System.Data.Common;
    using System.Data.SqlServerCe;
    using System.IO;
    using System.Linq;
    using Dapper;

    /// <summary>
    /// Helper class to make it easy to load or create the database
    /// </summary>
    public static class DBHelper
    {
        public static SqlCeConnection GetDbConnection(string dbLocation)
        {
            var sqlConnection = new SqlCeConnection();
            sqlConnection.ConnectionString = "Data Source = " + dbLocation;

            if (!File.Exists(dbLocation))
            {
                SqlCeEngine engine = new SqlCeEngine(sqlConnection.ConnectionString);
                engine.CreateDatabase();
            }
            sqlConnection.Open();
            return sqlConnection;
        }

        public static void InitializeDB(this DbConnection connection)
        {
            if (!connection.TableExists("Level1Matches"))
            {
                connection.Execute(@"CREATE TABLE Level1Matches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Level1 nvarchar(255),
                                        AltLevel1 nvarchar(255),
                                        Weight int)");
                connection.Execute(@"CREATE TABLE Level2Matches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Level1 nvarchar(255),
                                        Level2 nvarchar(255),
                                        AltLevel2 nvarchar(255),
                                        Weight int)");
                connection.Execute(@"CREATE TABLE Level3Matches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Level1 nvarchar(255),
                                        Level2 nvarchar(255),
                                        Level3 nvarchar(255),
                                        AltLevel3 nvarchar(255),
                                        Weight int)");
            }

            if (!connection.TableExists("LocationColumnsMapping"))
            {
                connection.Execute(@"CREATE TABLE LocationColumnsMapping (
                                        FileName nvarchar(255) PRIMARY KEY,
                                        Level1Code int,
                                        Level1Name int,
                                        Level1AltName int,
                                        Level2Code int,
                                        Level2Name int,
                                        Level2AltName int,
                                        Level3Code int,
                                        Level3Name int,
                                        Level3AltName int)");
            }
        }

        public static bool TableExists(this DbConnection connection, string table)
        {
            return connection.Query<int>(@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                        WHERE TABLE_NAME = '" + table + "'").Single() > 0;
        }
    }
}
