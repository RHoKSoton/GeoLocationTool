namespace MultiLevelGeoCoder.DataAccess
{
    using System.Collections.Generic;
    using MultiLevelGeoCoder.Model;

    public interface INearMatchesProvider
    {
        IEnumerable<Location1NearMatch> GetActualMatches(string nearMatch);
        IEnumerable<Location2NearMatch> GetActualMatches(string nearMatch, string location1);
        IEnumerable<Location3NearMatch> GetActualMatches(string nearMatch, string location1, string location2);
        void SaveMatch(string nearMatch, string location1);
        void SaveMatch(string nearMatch, string location1, string location2);
        void SaveMatch(string nearMatch, string location1, string location2, string location3);
    }
}
