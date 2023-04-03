using SimpleJSON;
using HypernexSharp.Socketing.SocketResponses;

namespace HypernexSharp.Socketing
{
    internal class FromGameServerMessage
    {
        public string serverTokenContent { get; }
        public string gameServerId { get; private set; }
        public string gameServerToken { get; private set; }
        public string message { get; set; }
        public JSONObject args { get; set; }

        public FromGameServerMessage(string serverTokenContent) => this.serverTokenContent = serverTokenContent;

        public void RegisterAuth(SendAuth auth)
        {
            gameServerId = auth.gameServerId;
            gameServerToken = auth.gameServerToken;
        }
        
        public FromGameServerMessage CreateMessage(ISocketMessage m) =>
            new FromGameServerMessage(serverTokenContent)
            {
                gameServerId = gameServerId,
                gameServerToken = gameServerToken,
                message = m.message,
                args = m.GetArgs()
            };
        
        public JSONNode GetJSON()
        {
            JSONObject o = new JSONObject();
            o.Add("serverTokenContent", serverTokenContent);
            o.Add("gameServerId", gameServerId);
            o.Add("gameServerToken", gameServerToken);
            o.Add("message", message);
            o.Add("args", args);
            return o;
        }
    }
}