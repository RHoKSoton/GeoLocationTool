namespace MultiLevelGeoCoder.Model
{
    using System;

    public class Location3NearMatch
    {
        public Guid MatchId { get; set; }
        public string NearMatch { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public int Weight { get; set; }
    }
}
