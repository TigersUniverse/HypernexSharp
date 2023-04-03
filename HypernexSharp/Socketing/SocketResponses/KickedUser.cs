using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class KickedUser : ISocketResponse
    {
        public string message => "KickedUser";
        public JSONNode Result { get; }

        public string instanceId;
        public string userId;

        public KickedUser(JSONNode result)
        {
            Result = result;
            instanceId = result["instanceId"].Value;
            userId = result["userId"].Value;
        }
    }
}