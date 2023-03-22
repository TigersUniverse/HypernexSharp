using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class LeaveInstance : ISocketMessage
    {
        public string message => "LeaveInstance";

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