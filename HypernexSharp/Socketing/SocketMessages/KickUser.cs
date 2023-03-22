using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class KickUser : ISocketMessage
    {
        public string message => "KickUser";
        
        public string InstanceId;
        public string UserId;

        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("instanceId", InstanceId);
            o.Add("userId", UserId);
            return o;
        }
    }
}