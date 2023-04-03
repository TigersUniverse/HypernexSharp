using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class FailedToLeaveInstance : ISocketResponse
    {
        public string message => "FailedToLeaveInstance";
        public JSONNode Result { get; }
        
        public string gameServerId;
        public string instanceId;

        public FailedToLeaveInstance(JSONNode result)
        {
            Result = result;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
        }
    }
}