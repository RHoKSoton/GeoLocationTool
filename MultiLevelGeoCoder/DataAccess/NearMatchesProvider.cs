namespace MultiLevelGeoCoder.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using Dapper;
    using MultiLevelGeoCoder.Model;

    /// <summary>
    /// Get or save NearMatches
    /// </summary>
    public class NearMatchesProvider : INearMatchesProvider
    {
        public DbConnection SqlConnection { get; set; }

        public NearMatchesProvider(DbConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }

        public IEnumerable<Level1Match> GetActualMatches(string nearMatch)
        {
            return SqlConnection.Query<Level1Match>(
                @"SELECT * FROM Level1NearMatches
                    WHERE NearMatch=@nearMatch ORDER BY Weight DESC",
                new { nearMatch }
            );
        }

        public IEnumerable<Level2Match> GetActualMatches(string nearMatch, string level1)
        {
            return SqlConnection.Query<Level2Match>(
                @"SELECT * FROM Level2NearMatches
                    WHERE NearMatch=@nearMatch AND Level1=@level1 ORDER BY Weight DESC",
                new { nearMatch, level1 }
            );
        }

        public IEnumerable<Level3Match> GetActualMatches(string nearMatch, string level1, string level2)
        {
            return SqlConnection.Query<Level3Match>(
                @"SELECT * FROM Level3NearMatches
                    WHERE NearMatch=@nearMatch AND Level1=@level1 AND Level2=@level2 ORDER BY Weight DESC",
                new { nearMatch, level1, level2 }
            );
        }

        public void SaveMatch(string nearMatch, string level1)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Level1NearMatches
                    WHERE NearMatch=@nearMatch AND Level1=@level1",
                new { nearMatch, level1 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Level1NearMatches (Id, Level1, NearMatch, Weight)
                                    VALUES (newid(), @level1, @NearMatch, 1)",
                                    new { level1, nearMatch });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Level1NearMatches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }

        public void SaveMatch(string nearMatch, string level1, string level2)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Level2NearMatches
                    WHERE NearMatch=@nearMatch AND Level1=@level1 AND Level2=@level2",
                new { nearMatch, level1, level2 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Level2NearMatches (Id, Level1, Level2, NearMatch, Weight)
                                    VALUES (newid(), @level1, @level2, @nearMatch, 1)",
                                    new { nearMatch, level1, level2 });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Level2NearMatches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }

        public void SaveMatch(string nearMatch, string level1, string level2, string level3)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Level3NearMatches
                    WHERE NearMatch=@nearMatch AND Level1=@level1 AND Level2=@level2 AND Level3=@level3",
                new { nearMatch, level1, level2, level3 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Level3NearMatches (Id, Level1, Level2, Level3, NearMatch, Weight)
                                    VALUES (newid(), @level1, @level2, @level3, @nearMatch, 1)",
                                    new { nearMatch, level1, level2, level3 });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Level3NearMatches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }
    }
}
