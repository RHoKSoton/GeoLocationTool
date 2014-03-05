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

        public IEnumerable<Level1Match> GetActualMatches(string alternateName)
        {
            return SqlConnection.Query<Level1Match>(
                @"SELECT * FROM Level1Matches
                    WHERE AltLevel1=@alternateName ORDER BY Weight DESC",
                new { alternateName }
            );
        }

        public IEnumerable<Level2Match> GetActualMatches(string alternateName, string level1)
        {
            return SqlConnection.Query<Level2Match>(
                @"SELECT * FROM Level2Matches
                    WHERE AltLevel2=@alternateName AND Level1=@level1 ORDER BY Weight DESC",
                new { alternateName, level1 }
            );
        }

        public IEnumerable<Level3Match> GetActualMatches(string alternateName, string level1, string level2)
        {
            return SqlConnection.Query<Level3Match>(
                @"SELECT * FROM Level3Matches
                    WHERE AltLevel3=@alternateName AND Level1=@level1 AND Level2=@level2 ORDER BY Weight DESC",
                new { alternateName, level1, level2 }
            );
        }

        public void SaveMatch(string alternateName, string level1)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Level1Matches
                    WHERE AltLevel1=@alternateName AND Level1=@level1",
                new { alternateName, level1 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Level1Matches (Id, Level1, AltLevel1, Weight)
                                    VALUES (newid(), @level1, @alternateName, 1)",
                                    new { level1, alternateName });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Level1Matches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }

        public void SaveMatch(string alternateName, string level1, string level2)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Level2Matches
                    WHERE AltLevel2=@alternateName AND Level1=@level1 AND Level2=@level2",
                new { alternateName, level1, level2 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Level2Matches (Id, Level1, Level2, AltLevel2, Weight)
                                    VALUES (newid(), @level1, @level2, @alternateName, 1)",
                                    new { alternateName, level1, level2 });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Level2Matches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }

        public void SaveMatch(string alternateName, string level1, string level2, string level3)
        {
            Guid guid = SqlConnection.Query<Guid>(
                @"SELECT TOP 1 Id FROM Level3Matches
                    WHERE AltLevel3=@alternateName AND Level1=@level1 AND Level2=@level2 AND Level3=@level3",
                new { alternateName, level1, level2, level3 }).FirstOrDefault();
            
            if (guid == Guid.Empty)
            {
                SqlConnection.Execute(@"INSERT INTO Level3Matches (Id, Level1, Level2, Level3, AltLevel3, Weight)
                                    VALUES (newid(), @level1, @level2, @level3, @alternateName, 1)",
                                    new { alternateName, level1, level2, level3 });
            }
            else
            {
                SqlConnection.Execute(@"UPDATE Level3Matches
                                    SET Weight=Weight+1 WHERE Id=@guid",
                                    new { guid });
            }
        }
    }
}
