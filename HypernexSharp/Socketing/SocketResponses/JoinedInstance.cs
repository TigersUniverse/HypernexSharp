using HypernexSharp.SocketObjects;
using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class JoinedInstance : ISocketResponse
    {
        public string message => "JoinedInstance";
        public JSONNode Result { get; }

        public string Uri;
        public InstanceProtocol InstanceProtocol;
        public string gameServerId;
        public string instanceId;
        public string tempUserToken;
        public string worldId;
        public string instanceCreatorId;

        public JoinedInstance(JSONNode result)
        {
            Result = result;
            Uri = result["Uri"].Value;
            InstanceProtocol = (InstanceProtocol) result["Uri"].AsInt;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
            tempUserToken = result["tempUserToken"].Value;
            worldId = result["worldId"].Value;
            instanceCreatorId = result["instanceCreatorId"].Value;
        }
    }
}