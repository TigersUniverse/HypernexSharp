using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class WarnStatus
    {
        public bool isWarned { get; set; }
        public int TimeWarned { get; set; }
        public string WarnReason { get; set; }
        public string WarnDescription { get; set; }

        public static WarnStatus FromJSON(JSONNode node) => new WarnStatus
        {
            isWarned = node["isWarned"].AsBool,
            TimeWarned = node["TimeWarned"].AsInt,
            WarnReason = node["WarnReason"].Value,
            WarnDescription = node["WarnDescription"].Value
        };
    }
}