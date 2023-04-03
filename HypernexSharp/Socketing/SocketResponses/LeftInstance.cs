using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class LeftInstance : ISocketResponse
    {
        public string message => "LeftInstance";
        public JSONNode Result { get; }
        
        public string gameServerId;
        public string instanceId;

        public LeftInstance(JSONNode result)
        {
            Result = result;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
        }
    }
}