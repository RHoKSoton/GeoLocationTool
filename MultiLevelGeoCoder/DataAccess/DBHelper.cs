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
            if (!connection.TableExists("Location1NearMatches"))
            {
                connection.Execute(@"CREATE TABLE Location1NearMatches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Location1 nvarchar(255),
                                        NearMatch nvarchar(255),
                                        Weight int)");
                connection.Execute(@"CREATE TABLE Location2NearMatches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Location1 nvarchar(255),
                                        Location2 nvarchar(255),
                                        NearMatch nvarchar(255),
                                        Weight int)");
                connection.Execute(@"CREATE TABLE Location3NearMatches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Location1 nvarchar(255),
                                        Location2 nvarchar(255),
                                        Location3 nvarchar(255),
                                        NearMatch nvarchar(255),
                                        Weight int)");
            }

            if (!connection.TableExists("LocationColumnsMapping"))
            {
                connection.Execute(@"CREATE TABLE LocationColumnsMapping (
                                        FileName nvarchar(255) PRIMARY KEY,
                                        Location1Code int,
                                        Location1Name int,
                                        Location1AltName int,
                                        Location2Code int,
                                        Location2Name int,
                                        Location2AltName int,
                                        Location3Code int,
                                        Location3Name int,
                                        Location3AltName int)");
            }
        }

        public static bool TableExists(this DbConnection connection, string table)
        {
            return connection.Query<int>(@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                        WHERE TABLE_NAME = '" + table + "'").Single() > 0;
        }
    }
}
