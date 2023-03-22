using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketResponses
{
    internal class SendAuth : ISocketResponse
    {
        public string message => "sendauth";
        public JSONNode Result { get; }

        public string gameServerId;
        public string gameServerToken;

        public SendAuth(JSONNode result)
        {
            Result = result;
            gameServerId = result["gameServerId"].Value;
            gameServerToken = result["gameServerToken"].Value;
        }
    }
}