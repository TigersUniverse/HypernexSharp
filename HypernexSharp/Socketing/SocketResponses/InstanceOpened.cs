using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class InstanceOpened : ISocketResponse
    {
        public string message => "InstanceOpened";
        public JSONNode Result { get; }
        
        public string gameServerId;
        public string instanceId;

        public InstanceOpened(JSONNode result)
        {
            Result = result;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
        }
    }
}