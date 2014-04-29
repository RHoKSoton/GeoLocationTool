// MatchProvider.cs

namespace MultiLevelGeoCoder.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using Dapper;
    using Model;

    /// <summary>
    /// Get or save matched names. A match being where the user has specified an 
    /// alternative name that will be used in the input instead 
    /// of a particular name in the gazetteer 
    /// </summary>
    internal class MatchProvider : IMatchProvider
    {
        #region Fields

        private readonly DbConnection sqlConnection;

        #endregion Fields

        #region Constructors

        public MatchProvider(DbConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<Level1Match> GetAllLevel1()
        {
            return sqlConnection.Query<Level1Match>(
                @"SELECT * FROM Level1Matches"
                );
        }

        public IEnumerable<Level2Match> GetAllLevel2()
        {
            return sqlConnection.Query<Level2Match>(
                @"SELECT * FROM Level2Matches"
                );
        }

        public IEnumerable<Level3Match> GetAllLevel3()
        {
            return sqlConnection.Query<Level3Match>(
                @"SELECT * FROM Level3Matches"
                );
        }

        public IEnumerable<Level1Match> GetMatches(string alternateName)
        {
            return sqlConnection.Query<Level1Match>(
                @"SELECT * FROM Level1Matches
                    WHERE AltLevel1=@alternateName ORDER BY Weight DESC",
                new {alternateName}
                );
        }

        public IEnumerable<Level2Match> GetMatches(
            string alternateName,
            string level1)
        {
            return sqlConnection.Query<Level2Match>(
                @"SELECT * FROM Level2Matches
                    WHERE AltLevel2=@alternateName AND Level1=@level1 ORDER BY Weight DESC",
                new {alternateName, level1}
                );
        }

        public IEnumerable<Level3Match> GetMatches(
            string alternateName,
            string level1,
            string level2)
        {
            return sqlConnection.Query<Level3Match>(
                @"SELECT * FROM Level3Matches
                    WHERE AltLevel3=@alternateName AND Level1=@level1 AND Level2=@level2 ORDER BY Weight DESC",
                new {alternateName, level1, level2}
                );
        }

        public void SaveMatchLevel1(string alternateName, string level1)
        {
            Guid guid = sqlConnection.Query<Guid>(
                @"SELECT Id FROM Level1Matches
                    WHERE AltLevel1=@alternateName",
                new {alternateName, level1}).FirstOrDefault();

            if (guid == Guid.Empty)
            {
                sqlConnection.Execute(
                    @"INSERT INTO Level1Matches (Id, Level1, AltLevel1, Weight)
                                    VALUES (newid(), @level1, @alternateName, 2)",
                    new {level1, alternateName});
            }
            else
            {
                sqlConnection.Execute(
                    @"UPDATE Level1Matches
                                    SET Level1=@level1, AltLevel1=@alternateName WHERE Id=@guid",
                    new {level1, alternateName, guid});
            }
        }

        public void SaveMatchLevel2(string alternateName, string level1, string level2)
        {
            Guid guid = sqlConnection.Query<Guid>(
                @"SELECT Id FROM Level2Matches
                    WHERE AltLevel2=@alternateName AND Level1=@level1",
                new {alternateName, level1,}).FirstOrDefault();

            if (guid == Guid.Empty)
            {
                sqlConnection.Execute(
                    @"INSERT INTO Level2Matches (Id, Level1, Level2, AltLevel2, Weight)
                                    VALUES (newid(), @level1, @level2, @alternateName, 2)",
                    new {alternateName, level1, level2});
            }
            else
            {
                sqlConnection.Execute(
                    @"UPDATE Level2Matches
                                      SET Level1=@level1, Level2=@level2, AltLevel2=@alternateName
                                      WHERE Id=@guid",
                    new {alternateName, level1, level2, guid});
            }
        }

        public void SaveMatchLevel3(
            string alternateName,
            string level1,
            string level2,
            string level3)
        {
            Guid guid = sqlConnection.Query<Guid>(
                @"SELECT Id FROM Level3Matches
                    WHERE AltLevel3=@alternateName AND Level1=@level1 AND Level2=@level2",
                new {alternateName, level1, level2}).FirstOrDefault();

            if (guid == Guid.Empty)
            {
                sqlConnection.Execute(
                    @"INSERT INTO Level3Matches (Id, Level1, Level2, Level3, AltLevel3, Weight)
                                    VALUES (newid(), @level1, @level2, @level3, @alternateName, 2)",
                    new {alternateName, level1, level2, level3});
            }
            else
            {
                sqlConnection.Execute(
                    @"UPDATE Level3Matches
                                     SET Level1=@level1, Level2=@level2, Level3=@level3, AltLevel3=@alternateName
                                     WHERE Id=@guid",
                    new {alternateName, level1, level2, level3, guid});
            }
        }

        #endregion Methods
    }
}