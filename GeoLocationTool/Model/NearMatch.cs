using System;

namespace GeoLocationTool.Model
{
    internal class NearMatch
    {
        public Guid MatchId { get; set; }
        public string Near { get; set; }
        public string Actual { get; set; }
    }
}
