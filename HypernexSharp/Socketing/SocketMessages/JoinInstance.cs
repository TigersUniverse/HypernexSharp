using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class JoinInstance : ISocketMessage
    {
        public string message => "JoinInstance";

        public string gameServerId;
        public string instanceId;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("gameServerId", gameServerId);
            o.Add("instanceId", instanceId);
            return o;
        }
    }
}