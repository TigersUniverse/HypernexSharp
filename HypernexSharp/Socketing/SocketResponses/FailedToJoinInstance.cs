using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class FailedToJoinInstance : ISocketResponse
    {
        public string message => "FailedToJoinInstance";
        public JSONNode Result { get; }

        public string gameServerId;
        public string instanceId;

        public FailedToJoinInstance(JSONNode result)
        {
            Result = result;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
        }
    }
}