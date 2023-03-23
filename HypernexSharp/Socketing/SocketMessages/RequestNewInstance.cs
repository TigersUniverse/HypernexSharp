using HypernexSharp.Libs;
using HypernexSharp.SocketObjects;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class RequestNewInstance : ISocketMessage
    {
        public string message => "RequestNewInstance";

        public string worldId;
        public InstancePublicity instancePublicity;
        public InstanceProtocol instanceProtocol;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("worldId", worldId);
            o.Add("instancePublicity", (int) instancePublicity);
            o.Add("instanceProtocol", (int) instanceProtocol);
            return o;
        }
    }
}