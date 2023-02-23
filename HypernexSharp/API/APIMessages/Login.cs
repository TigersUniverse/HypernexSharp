using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class Login : APIMessage
    {
        private string username { get; }
        private string password { get; }
        private string twofacode { get; }

        protected override string Endpoint => "login";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("username", username);
            o.Add("password", password);
            if(!string.IsNullOrEmpty(twofacode))
                o.Add("twofacode", twofacode);
            return o;
        }

        public Login(string username, string password, string twofacode = "")
        {
            this.username = username;
            this.password = password;
            this.twofacode = twofacode;
        }
    }
}