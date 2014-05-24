namespace MultiLevelGeoCoder.Model
{
    using System;

    public class Level3Match
    {
        public Guid MatchId { get; set; }
        public string AltLevel3 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
    }
}
