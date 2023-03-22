using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class UnbannedUser : ISocketResponse
    {
        public string message => "UnbannedUser";
        public JSONNode Result { get; }

        public string instanceId;
        public string userId;

        public UnbannedUser(JSONNode result)
        {
            Result = result;
            instanceId = result["instanceId"].Value;
            userId = result["userId"].Value;
        }
    }
}