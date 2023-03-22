using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class RemovedModerator : ISocketResponse
    {
        public string message => "RemovedModerator";
        public JSONNode Result { get; }

        public string instanceId;
        public string userId;

        public RemovedModerator(JSONNode result)
        {
            Result = result;
            instanceId = result["instanceId"].Value;
            userId = result["userId"].Value;
        }
    }
}