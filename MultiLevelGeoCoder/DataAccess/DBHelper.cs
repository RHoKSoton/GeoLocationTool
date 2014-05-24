// DBHelper.cs

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
        #region Methods

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
                                        Level1 nvarchar(255) NOT NULL,
                                        AltLevel1 nvarchar(255) NOT NULL,
                                        Weight int,
                                        CONSTRAINT AK_Level1_AltLevel1 UNIQUE (Level1,AltLevel1))");
                connection.Execute(@"CREATE TABLE Level2Matches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Level1 nvarchar(255) NOT NULL,
                                        Level2 nvarchar(255) NOT NULL,
                                        AltLevel2 nvarchar(255) NOT NULL,
                                        Weight int,
                                        CONSTRAINT AK_Level1_Level2_AltLevel2 UNIQUE (Level1, Level2, AltLevel2))");
                connection.Execute(@"CREATE TABLE Level3Matches (
                                        Id uniqueidentifier PRIMARY KEY,
                                        Level1 nvarchar(255) NOT NULL,
                                        Level2 nvarchar(255) NOT NULL,
                                        Level3 nvarchar(255) NOT NULL,
                                        AltLevel3 nvarchar(255) NOT NULL,
                                        Weight int,
                                        CONSTRAINT AK_Level1_Level2_Level3_AltLevel3 UNIQUE (Level1, Level2, Level3, AltLevel3))");
            }

            if (!connection.TableExists("GazetteerColumnsMapping"))
            {
                connection.Execute(@"CREATE TABLE GazetteerColumnsMapping (
                                        FileName nvarchar(255) PRIMARY KEY,
                                        Level1Code nvarchar(255),
                                        Level1Name nvarchar(255),
                                        Level1AltName nvarchar(255),
                                        Level2Code nvarchar(255),
                                        Level2Name nvarchar(255),
                                        Level2AltName nvarchar(255),
                                        Level3Code nvarchar(255),
                                        Level3Name nvarchar(255),
                                        Level3AltName nvarchar(255))");
            }
        }

        public static bool TableExists(this DbConnection connection, string table)
        {
            return connection.Query<int>(@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                        WHERE TABLE_NAME = '" + table + "'").Single() > 0;
        }

        #endregion Methods
    }
}