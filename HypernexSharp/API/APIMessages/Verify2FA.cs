using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class Verify2FA : APIMessage
    {
        private string username { get; }
        private string tokenContent { get; }
        private string code { get; }

        protected override string Endpoint => "verifyEmailToken";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("username", username);
            o.Add("tokenContent", tokenContent);
            o.Add("code", code);
            return o;
        }

        public Verify2FA(string username, string tokenContent, string code)
        {
            this.username = username;
            this.tokenContent = tokenContent;
            this.code = code;
        }
    }
}