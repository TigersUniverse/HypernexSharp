using SimpleJSON;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class InstanceReady : ISocketMessage
    {
        public string message => "InstanceReady";
        
        public string instanceId;
        public string uri;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("instanceId", instanceId);
            o.Add("Uri", uri);
            return o;
        }
    }
}