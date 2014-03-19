// IMatchProvider.cs

namespace MultiLevelGeoCoder.DataAccess
{
    using System.Collections.Generic;
    using Model;

    public interface IMatchProvider
    {
        #region Methods

        IEnumerable<Level1Match> GetAllLevel1();

        IEnumerable<Level2Match> GetAllLevel2();

        IEnumerable<Level3Match> GetAllLevel3();

        IEnumerable<Level1Match> GetMatches(string nearMatch);

        IEnumerable<Level2Match> GetMatches(string nearMatch, string level1);

        IEnumerable<Level3Match> GetMatches(
            string nearMatch,
            string level1,
            string level2);

        void SaveMatchLevel1(string input, string level1);

        void SaveMatchLevel2(string alternateName, string level1, string level2);

        void SaveMatchLevel3(string input, string level1, string level2, string level3);

        #endregion Methods
    }
}