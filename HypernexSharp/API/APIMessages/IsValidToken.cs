using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class IsValidToken : APIMessage
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string tokenContent { get; }

        protected override string Endpoint => "isValidToken";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            if(!string.IsNullOrEmpty(username))
                o.Add("username", username);
            else if(!string.IsNullOrEmpty(userid))
                o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            return o;
        }

        public IsValidToken(string tokenContent) => this.tokenContent = tokenContent;
    }
}