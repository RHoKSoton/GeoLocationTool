using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using Dapper;

namespace GeoLocationTool.DataAccess
{
    /// <summary>
    /// Helper class to make it easy to load or create the database
    /// </summary>
    internal static class DBHelper
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
            //Probably not the best way to do it!
            if (connection.Query<int>(@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                        WHERE TABLE_NAME = 'Location3NearMatches'").Single() == 0)
            {
                connection.Execute(@"CREATE TABLE Location1NearMatches (Id uniqueidentifier PRIMARY KEY,
                                    Location1 nvarchar(255),
                                    NearMatch nvarchar(255),
                                    Weight int)");
                connection.Execute(@"CREATE TABLE Location2NearMatches (Id uniqueidentifier PRIMARY KEY,
                                    Location1 nvarchar(255),
                                    Location2 nvarchar(255),
                                    NearMatch nvarchar(255),
                                    Weight int)");
                connection.Execute(@"CREATE TABLE Location3NearMatches (Id uniqueidentifier PRIMARY KEY,
                                    Location1 nvarchar(255),
                                    Location2 nvarchar(255),
                                    Location3 nvarchar(255),
                                    NearMatch nvarchar(255),
                                    Weight int)");
            }
        }
    }
}
