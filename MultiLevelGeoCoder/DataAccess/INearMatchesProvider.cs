namespace MultiLevelGeoCoder.DataAccess
{
    using System.Collections.Generic;
    using MultiLevelGeoCoder.Model;

    public interface INearMatchesProvider
    {
        IEnumerable<Level1NearMatch> GetActualMatches(string nearMatch);
        IEnumerable<Level2NearMatch> GetActualMatches(string nearMatch, string level1);
        IEnumerable<Level3NearMatch> GetActualMatches(string nearMatch, string level1, string level2);
        void SaveMatch(string nearMatch, string level1);
        void SaveMatch(string nearMatch, string level1, string level2);
        void SaveMatch(string nearMatch, string level1, string level2, string level3);
    }
}
