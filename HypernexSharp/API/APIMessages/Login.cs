using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class Login : APIMessage
    {
        private string userid { get; }
        private string password { get; }
        private string twofacode { get; }

        protected override string Endpoint => "login";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            o.Add("password", password);
            if(!string.IsNullOrEmpty(twofacode))
                o.Add("twofacode", twofacode);
            return o;
        }

        public Login(string userid, string password, string twofacode = "")
        {
            this.userid = userid;
            this.password = password;
            this.twofacode = twofacode;
        }
    }
}