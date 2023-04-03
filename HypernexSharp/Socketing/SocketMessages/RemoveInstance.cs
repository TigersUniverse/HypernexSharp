using SimpleJSON;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class RemoveInstance : ISocketMessage
    {
        public string message => "RemoveInstance";
        
        public string InstanceId;

        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("instanceId", InstanceId);
            return o;
        }
    }
}