using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class GameServer
    {
        public string GameServerId;
        public Region Region;

        public static GameServer FromJSON(JSONNode node)
        {
            GameServer gameServer = new GameServer
            {
                GameServerId = node["GameServerId"].Value
            };
            if (node["Region"] != null)
                gameServer.Region = Region.FromJSON(node["Region"]);
            else
                gameServer.Region = new Region();
            return gameServer;
        }
    }
}