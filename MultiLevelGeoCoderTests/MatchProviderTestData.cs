// MatchProviderTestData.cs

namespace MultiLevelGeoCoderTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MultiLevelGeoCoder.Model;

    /// <summary>
    /// Contains matched names test data
    /// </summary>
    internal class MatchProviderTestData
    {
        #region Fields

        private readonly List<Level1Match> level1Matches = new List<Level1Match>();
        private readonly List<Level2Match> level2Matches = new List<Level2Match>();
        private readonly List<Level3Match> level3Matches = new List<Level3Match>();

        #endregion Fields

        #region Methods

        public void AddLevel1(string[] match, string[] actual)
        {
            var matched = Level1Matches(match[0]).FirstOrDefault();
            if (matched != null)
            {
                level1Matches.Remove(matched);
            }
            Level1Match level1Match = new Level1Match();
            level1Match.Level1 = actual[0];
            level1Match.AltLevel1 = match[0];
            level1Matches.Add(level1Match);
        }

        public void AddLevel2(string[] match, string[] actual)
        {
            var matched = Level2Matches(actual[0], match[1]).FirstOrDefault();
            if (matched != null)
            {
                level2Matches.Remove(matched);
            }
            Level2Match level2Match = new Level2Match();
            level2Match.Level1 = actual[0];
            level2Match.Level2 = actual[1];
            level2Match.AltLevel2 = match[1];
            level2Matches.Add(level2Match);
        }

        public void AddLevel3(string[] match, string[] actual)
        {
            var matched = Level3Matches(actual[0], actual[1], match[2]).FirstOrDefault();
            if (matched != null)
            {
                level3Matches.Remove(matched);
            }
            Level3Match level3Match = new Level3Match();
            level3Match.Level1 = actual[0];
            level3Match.Level2 = actual[1];
            level3Match.Level3 = actual[2];
            level3Match.AltLevel3 = match[2];
            level3Matches.Add(level3Match);
        }

        public IEnumerable<Level1Match> AllLevel1()
        {
            return level1Matches;
        }

        public IEnumerable<Level2Match> AllLevel2()
        {
            return level2Matches;
        }

        public IEnumerable<Level3Match> AllLevel3()
        {
            return level3Matches;
        }

        public IEnumerable<Level1Match> EmptyLevel1List()
        {
            return new List<Level1Match>();
        }

        public IEnumerable<Level2Match> EmptyLevel2List()
        {
            return new List<Level2Match>();
        }

        public IEnumerable<Level3Match> EmptyLevel3List()
        {
            return new List<Level3Match>();
        }

        public IEnumerable<Level1Match> Level1Matches(string level1)
        {
            return
                level1Matches.Where(
                    x =>
                        x.AltLevel1 == level1);
        }

        public IEnumerable<Level2Match> Level2Matches(string level1, string level2)
        {
            return
                level2Matches.Where(
                    x =>
                        x.Level1 == level1 &&
                        x.AltLevel2 == level2);
        }

        public IEnumerable<Level3Match> Level3Matches(
            string level1,
            string level2,
            string level3)
        {
            return level3Matches.Where(
                x =>
                    x.Level1 == level1 &&
                    x.Level2 == level2 &&
                    x.AltLevel3 == level3);
        }

        #endregion Methods
    }
}