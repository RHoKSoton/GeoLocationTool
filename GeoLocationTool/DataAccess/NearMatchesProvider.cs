using GeoLocationTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.Common;

namespace GeoLocationTool.DataAccess
{
    internal class NearMatchesProvider : INearMatchesProvider
    {
        public DbConnection SqlConnection { get; set; }

        public NearMatchesProvider(DbConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }

        public IEnumerable<NearMatch> GetActualMatches(string near)
        {
            return SqlConnection.Query<NearMatch>("SELECT * FROM NearMatches WHERE near=@near", new { near });
        }

        public void InsertMatch(string near, string actual)
        {
            SqlConnection.Execute(@"INSERT INTO NearMatches (matchId, near, actual)
                                    VALUES (newid(), @near, @actual)", new { near, actual });
        }
    }
}
