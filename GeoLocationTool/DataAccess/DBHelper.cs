using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using Dapper;

namespace GeoLocationTool.DataAccess
{
    //Not fan of this name
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
            if (connection.Query<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'NearMatches'").Single() == 0)
                connection.Execute("CREATE TABLE NearMatches (matchId uniqueidentifier PRIMARY KEY, near nvarchar(255), actual nvarchar(255))");
        }
    }
}
