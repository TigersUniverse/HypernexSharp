using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class GotInviteRequest : ISocketResponse
    {
        public string message => "GotInviteRequest";
        
        public JSONNode Result { get; }

        public string fromUserId;

        public GotInviteRequest(JSONNode result)
        {
            Result = result;
            fromUserId = result["fromUserId"].Value;
        }
    }
}