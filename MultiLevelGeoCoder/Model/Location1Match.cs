namespace MultiLevelGeoCoder.Model
{
    using System;

    public class Level1Match
    {
        public Guid MatchId { get; set; }
        public string AltLevel1 { get; set; }
        public string Level1 { get; set; }
        public int Weight { get; set; }
    }
}
