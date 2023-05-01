using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class GotInvite : ISocketResponse
    {
        public string message => "GotInvite";
        public JSONNode Result { get; }

        public string fromUserId;
        public string toGameServerId;
        public string toInstanceId;
        public string worldId;

        public GotInvite(JSONNode result)
        {
            Result = result;
            fromUserId = result["fromUserId"].Value;
            toGameServerId = result["toGameServerId"].Value;
            toInstanceId = result["toInstanceId"].Value;
            worldId = result["worldId"].Value;
        }
    }
}