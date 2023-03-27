using System;
using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class JoinedInstance : ISocketResponse
    {
        public string message => "JoinedInstance";
        public JSONNode Result { get; }

        public string Uri;
        public string gameServerId;
        public string instanceId;
        public string tempUserToken;

        public JoinedInstance(JSONNode result)
        {
            Result = result;
            Uri = result["Uri"].Value;
            gameServerId = result["gameServerId"].Value;
            instanceId = result["instanceId"].Value;
            tempUserToken = result["tempUserToken"].Value;
        }
    }
}