using HypernexSharp.SocketObjects;
using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class InstanceOpened : ISocketResponse
    {
        public string message => "InstanceOpened";
        public JSONNode Result { get; }
        
        public string gameServerId;
        public string instanceId;
        public string Uri;
        public InstanceProtocol InstanceProtocol;
        public string worldId;
        public string tempUserToken;

        public InstanceOpened(JSONNode result)
        {
            Result = result;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
            Uri = result["Uri"].Value;
            worldId = result["worldId"].Value;
            tempUserToken = result["tempUserToken"].Value;
            InstanceProtocol = (InstanceProtocol) result["InstanceProtocol"].AsInt;
        }
    }
}