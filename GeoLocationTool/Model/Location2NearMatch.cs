﻿using System;

namespace GeoLocationTool.Model
{
    internal class Location2NearMatch
    {
        public Guid MatchId { get; set; }
        public string NearMatch { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public int Weight { get; set; }
    }
}