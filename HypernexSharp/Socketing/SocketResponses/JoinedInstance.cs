using System.Collections.Generic;
using HypernexSharp.SocketObjects;
using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class JoinedInstance : ISocketResponse
    {
        public string message => "JoinedInstance";
        public JSONNode Result { get; }

        public string Uri;
        public InstanceProtocol InstanceProtocol;
        public InstancePublicity InstancePublicity;
        public string gameServerId;
        public string instanceId;
        public string tempUserToken;
        public string worldId;
        public string instanceCreatorId;
        public List<string> Moderators = new List<string>();
        public List<string> BannedUsers = new List<string>();

        public JoinedInstance(JSONNode result)
        {
            Result = result;
            Uri = result["Uri"].Value;
            InstanceProtocol = (InstanceProtocol) result["InstanceProtocol"].AsInt;
            InstancePublicity = (InstancePublicity) result["InstancePublicity"].AsInt;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
            tempUserToken = result["tempUserToken"].Value;
            worldId = result["worldId"].Value;
            instanceCreatorId = result["instanceCreatorId"].Value;
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result["Moderators"].AsArray)
                Moderators.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result["BannedUsers"].AsArray)
                BannedUsers.Add(keyValuePair.Value.Value);
        }
    }
}