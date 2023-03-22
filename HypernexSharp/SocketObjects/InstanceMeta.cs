using System;
using System.Collections.Generic;
using HypernexSharp.Libs;

namespace HypernexSharp.SocketObjects
{
    public class InstanceMeta
    {
        public Uri Uri { get; set; }
        public string GameServerId { get; set; }
        public string InstanceId { get; set; }
        public string WorldId { get; set; }
        public InstancePublicity InstancePublicity { get; set; }
        public string InstanceCreatorId { get; set; }
        public List<string> InvitedUsers { get; set; }
        public List<string> BannedUsers { get; set; }
        public List<string> ConnectedUsers { get; set; }
        public List<string> Moderators { get; set; }

        public static InstanceMeta FromJSON(JSONNode node)
        {
            InstanceMeta instanceMeta = new InstanceMeta
            {
                Uri = new Uri(node["Uri"].Value),
                GameServerId = node["GameServerId"].Value,
                InstanceId = node["InstanceId"].Value,
                WorldId = node["WorldId"].Value,
                InstancePublicity = (InstancePublicity) node["InstancePublicity"].AsInt,
                InstanceCreatorId = node["InstanceCreatorId"].Value,
                InvitedUsers = new List<string>(),
                BannedUsers = new List<string>(),
                ConnectedUsers = new List<string>(),
                Moderators = new List<string>()
            };
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["InvitedUsers"].AsArray)
                instanceMeta.InvitedUsers.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["BannedUsers"].AsArray)
                instanceMeta.BannedUsers.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["ConnectedUsers"].AsArray)
                instanceMeta.ConnectedUsers.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Moderators"].AsArray)
                instanceMeta.Moderators.Add(keyValuePair.Value.Value);
            return instanceMeta;
        }
    }
}