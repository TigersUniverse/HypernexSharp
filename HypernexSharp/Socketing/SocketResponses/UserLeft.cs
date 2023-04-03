using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class UserLeft : ISocketResponse
    {
        public string message => "UserLeft";
        public JSONNode Result { get; }

        public string instanceId;
        public string userId;

        public UserLeft(JSONNode result)
        {
            Result = result;
            instanceId = result["instanceId"].Value;
            userId = result["userId"].Value;
        }
    }
}