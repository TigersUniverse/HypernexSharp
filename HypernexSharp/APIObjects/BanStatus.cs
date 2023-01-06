using HypernexSharp.Libs;

namespace HypernexSharp.APIObjects
{
    public class BanStatus
    {
        public bool isBanned { get; set; }
        public int BanBegin { get; set; }
        public int BanEnd { get; set; }
        public string BanReason { get; set; }
        public string BanDescription { get; set; }

        public static BanStatus FromJSON(JSONNode node) => new BanStatus
        {
            isBanned = node["isBanned"].AsBool,
            BanBegin = node["BanBegin"].AsInt,
            BanEnd = node["BanEnd"].AsInt,
            BanReason = node["BanReason"].Value,
            BanDescription = node["BanDescription"].Value
        };
    }
}