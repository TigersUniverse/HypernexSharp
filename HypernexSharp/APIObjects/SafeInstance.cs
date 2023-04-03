using System.Collections.Generic;
using SimpleJSON;
using HypernexSharp.SocketObjects;

namespace HypernexSharp.APIObjects
{
    public class SafeInstance
    {
        public string GameServerId { get; set; }
        public string InstanceId { get; set; }
        public string InstanceCreatorId { get; set; }
        public InstancePublicity InstancePublicity { get; set; }
        public InstanceProtocol InstanceProtocol { get; set; }
        public List<string> ConnectedUsers { get; set; }
        public string WorldId { get; set; }

        public static SafeInstance FromJSON(JSONNode node)
        {
            SafeInstance safeInstance = new SafeInstance
            {
                GameServerId = node["GameServerId"].Value,
                InstanceId = node["InstanceId"].Value,
                InstanceCreatorId = node["InstanceCreatorId"].Value,
                InstancePublicity = (InstancePublicity) node["InstancePublicity"].AsInt,
                InstanceProtocol = (InstanceProtocol) node["InstanceProtocol"].AsInt,
                ConnectedUsers = new List<string>(),
                WorldId = node["WorldId"].Value
            };
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["ConnectedUsers"].AsArray)
                safeInstance.ConnectedUsers.Add(keyValuePair.Value.Value);
            return safeInstance;
        }
    }
}