using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class VerifyEmailToken : APIMessage
    {
        private string username { get; }
        private string tokenContent { get; }
        private string emailToken { get; }

        protected override string Endpoint => "verifyEmailToken";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("username", username);
            o.Add("tokenContent", tokenContent);
            o.Add("emailToken", emailToken);
            return o;
        }

        public VerifyEmailToken(string username, string tokenContent, string emailToken)
        {
            this.username = username;
            this.tokenContent = tokenContent;
            this.emailToken = emailToken;
        }
    }
}