﻿namespace MultiLevelGeoCoder.Model
{
    using System;

    public class Level2Match
    {
        public Guid MatchId { get; set; }
        public string AltLevel2 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public int Weight { get; set; }
    }
}
