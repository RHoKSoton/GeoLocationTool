using GeoLocationTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.Common;

namespace GeoLocationTool.DataAccess
{
    /// <summary>
    /// Get or save NearMatches
    /// </summary>
    internal class NearMatchesProvider : INearMatchesProvider
    {
        public DbConnection SqlConnection { get; set; }

        public NearMatchesProvider(DbConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }

        public IEnumerable<Location1NearMatch> GetActualMatches(string nearMatch)
        {
            return SqlConnection.Query<Location1NearMatch>(
                @"SELECT * FROM Location1NearMatches
                    WHERE NearMatch=@nearMatch",
                new { nearMatch }
            );
        }

        public IEnumerable<Location2NearMatch> GetActualMatches(string nearMatch, string location1)
        {
            return SqlConnection.Query<Location2NearMatch>(
                @"SELECT * FROM Location2NearMatches
                    WHERE NearMatch=@nearMatch AND Location1=@location1",
                new { nearMatch, location1 }
            );
        }

        public IEnumerable<Location3NearMatch> GetActualMatches(string nearMatch, string location1, string location2)
        {
            return SqlConnection.Query<Location3NearMatch>(
                @"SELECT * FROM Location3NearMatches
                    WHERE NearMatch=@nearMatch AND Location1=@location1 AND Location2=@location2",
                new { nearMatch, location1, location2 }
            );
        }

        public void SaveMatch(string nearMatch, string location1)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Location1NearMatches
                    WHERE NearMatch=@nearMatch AND Location1=@location1",
                new { nearMatch, location1 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Location1NearMatches (Id, Location1, NearMatch, Weight)
                                    VALUES (newid(), @location1, @NearMatch, 1)",
                                    new { location1, nearMatch });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Location1NearMatches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }

        public void SaveMatch(string nearMatch, string location1, string location2)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Location2NearMatches
                    WHERE NearMatch=@nearMatch AND Location1=@location1 AND Location2=@location2",
                new { nearMatch, location1, location2 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Location2NearMatches (Id, Location1, Location2, NearMatch)
                                    VALUES (newid(), @location1, @location2, @nearMatch)",
                                    new { nearMatch, location1, location2 });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Location2NearMatches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }

        public void SaveMatch(string nearMatch, string location1, string location2, string location3)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Location3NearMatches
                    WHERE NearMatch=@nearMatch AND Location1=@location1 AND Location2=@location2 AND Location3=@location3",
                new { nearMatch, location1, location2, location3 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Location3NearMatches (Id, Location1, Location2, Location3, NearMatch)
                                    VALUES (newid(), @location1, @location2, @location3, @nearMatch)",
                                    new { nearMatch, location1, location2, location3 });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Location3NearMatches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }
    }
}
