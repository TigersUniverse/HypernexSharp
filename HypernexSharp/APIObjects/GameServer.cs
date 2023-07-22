using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class GameServer
    {
        public string GameServerId;
        public Region Region;

        public static GameServer FromJSON(JSONNode node) => new GameServer
        {
            GameServerId = node["GameServerId"].Value,
            Region = Region.FromJSON(node["Region"])
        };
    }
}