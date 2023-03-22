using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class SendInvite : ISocketMessage
    {
        public string message => "SendInvite";

        public string gameServerId;
        public string toInstanceId;
        public string targetUserId;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("gameServerId", gameServerId);
            o.Add("toInstanceId", toInstanceId);
            o.Add("targetUserId", targetUserId);
            return o;
        }
    }
}