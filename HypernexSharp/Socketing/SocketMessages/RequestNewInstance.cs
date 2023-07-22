using SimpleJSON;
using HypernexSharp.SocketObjects;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class RequestNewInstance : ISocketMessage
    {
        public string message => "RequestNewInstance";

        public string worldId;
        public InstancePublicity instancePublicity;
        public InstanceProtocol instanceProtocol;
        public string gameServerId;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("worldId", worldId);
            o.Add("instancePublicity", (int) instancePublicity);
            o.Add("instanceProtocol", (int) instanceProtocol);
            if(!string.IsNullOrEmpty(gameServerId))
                o.Add("gameServerId", gameServerId);
            return o;
        }
    }
}