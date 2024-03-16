using SimpleJSON;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class RequestInvite : ISocketMessage
    {
        public string message => "RequestInvite";

        public string targetUserId;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("targetUserId", targetUserId);
            return o;
        }
    }
}