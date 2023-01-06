using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class ChangeEmail : APIMessage
    {
        private string username { get; }
        private string tokenContent { get; }
        private string newEmail { get; }

        protected override string Endpoint => "verifyEmailToken";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("username", username);
            o.Add("tokenContent", tokenContent);
            o.Add("newEmail", newEmail);
            return o;
        }

        public ChangeEmail(string username, string tokenContent, string newEmail)
        {
            this.username = username;
            this.tokenContent = tokenContent;
            this.newEmail = newEmail;
        }
    }
}