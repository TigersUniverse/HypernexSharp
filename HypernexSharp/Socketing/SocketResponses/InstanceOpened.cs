using System.Collections.Generic;
using HypernexSharp.SocketObjects;
using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class InstanceOpened : ISocketResponse
    {
        public string message => "InstanceOpened";
        public JSONNode Result { get; }
        
        public string gameServerId;
        public string instanceId;
        public string Uri;
        public InstanceProtocol InstanceProtocol;
        public InstancePublicity InstancePublicity;
        public string worldId;
        public string tempUserToken;
        public List<string> Moderators = new List<string>();
        public List<string> BannedUsers = new List<string>();

        public InstanceOpened(JSONNode result)
        {
            Result = result;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
            Uri = result["Uri"].Value;
            worldId = result["worldId"].Value;
            tempUserToken = result["tempUserToken"].Value;
            InstanceProtocol = (InstanceProtocol) result["InstanceProtocol"].AsInt;
            InstancePublicity = (InstancePublicity) result["InstancePublicity"].AsInt;
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result["Moderators"].AsArray)
                Moderators.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result["BannedUsers"].AsArray)
                BannedUsers.Add(keyValuePair.Value.Value);
        }
    }
}