using System.Collections.Generic;
using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class GameServersResult
    {
        public List<GameServer> GameServers = new List<GameServer>();
        
        public GameServersResult(){}

        internal GameServersResult(JSONNode result)
        {
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result["GameServers"].AsArray)
                GameServers.Add(GameServer.FromJSON(keyValuePair.Value));
        }
    }
}