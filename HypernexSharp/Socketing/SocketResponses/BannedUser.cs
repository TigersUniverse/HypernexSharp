using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class BannedUser : ISocketResponse
    {
        public string message => "BannedUser";
        public JSONNode Result { get; }

        public string instanceId;
        public string userId;

        public BannedUser(JSONNode result)
        {
            Result = result;
            instanceId = result["instanceId"].Value;
            userId = result["userId"].Value;
        }
    }
}