using SimpleJSON;

namespace HypernexSharp.Socketing
{
    internal class FromUserMessage
    {
        public string userId { get; }
        public string tokenContent { get; }
        public string message { get; private set; }
        public JSONObject args { get; private set; }

        public FromUserMessage(string userId, string tokenContent)
        {
            this.userId = userId;
            this.tokenContent = tokenContent;
        }

        public FromUserMessage CreateMessage(ISocketMessage m) =>
            new FromUserMessage(userId, tokenContent)
            {
                message = m.message,
                args = m.GetArgs()
            };
        
        public JSONNode GetJSON()
        {
            JSONObject o = new JSONObject();
            o.Add("userId", userId);
            o.Add("tokenContent", tokenContent);
            o.Add("message", message);
            o.Add("args", args);
            return o;
        }
    }
}