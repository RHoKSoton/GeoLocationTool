// INearMatchesProvider.cs

namespace MultiLevelGeoCoder.DataAccess
{
    using System.Collections.Generic;
    using Model;

    public interface INearMatchesProvider
    {
        #region Methods

        IEnumerable<Level1Match> GetActualMatches(string nearMatch);

        IEnumerable<Level2Match> GetActualMatches(string nearMatch, string level1);

        IEnumerable<Level3Match> GetActualMatches(
            string nearMatch,
            string level1,
            string level2);

        void SaveMatchLevel1(string input, string level1);

        void SaveMatchLevel2(string alternateName, string level1, string level2);

        void SaveMatchLevel3(string input, string level1, string level2, string level3);

        #endregion Methods
    }
}