using System.Collections.Generic;
using SimpleJSON;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class SendInvite : ISocketMessage
    {
        public string message => "SendInvite";

        public string gameServerId;
        public string toInstanceId;
        public string targetUserId;
        public string assetToken;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("gameServerId", gameServerId);
            o.Add("toInstanceId", toInstanceId);
            o.Add("targetUserId", targetUserId);
            o.Add("assetToken", assetToken);
            return o;
        }
    }
}