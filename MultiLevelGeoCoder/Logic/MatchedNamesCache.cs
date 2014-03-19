// MatchedNamesCache.cs

namespace MultiLevelGeoCoder.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using Model;

    /// <summary>
    /// Holds an in memory copy of the matched names data.
    /// </summary>
    internal class MatchedNamesCache
    {
        #region Fields

        private readonly IMatchProvider matchProvider;

        private List<Level1Match> level1Matches = new List<Level1Match>();
        private List<Level2Match> level2Matches = new List<Level2Match>();
        private List<Level3Match> level3Matches = new List<Level3Match>();

        #endregion Fields

        #region Constructors

        public MatchedNamesCache(IMatchProvider matchProvider)
        {
            this.matchProvider = matchProvider;
        }

        #endregion Constructors

        #region Methods

        public Level1Match Level1Match(string match)
        {
            //  note there should only ever be one actual name for the given match name
            // todo we need to ensure that there is only one name possibility in the database

            return level1Matches.FirstOrDefault(x => x.AltLevel1 == match);
        }

        public Level2Match Level2Match(string level1, string match)
        {
            return
                level2Matches.FirstOrDefault(
                    x => x.AltLevel2 == match && x.Level1 == level1);
        }

        public Level3Match Level3Match(string level1, string level2, string match)
        {
            return
                level3Matches.FirstOrDefault(
                    x => x.AltLevel3 == match && x.Level1 == level1 && x.Level2 == level2);
        }

        public void Refresh()
        {
            level1Matches = matchProvider.GetAllLevel1().ToList();
            level2Matches = matchProvider.GetAllLevel2().ToList();
            level3Matches = matchProvider.GetAllLevel3().ToList();
        }

        #endregion Methods
    }
}