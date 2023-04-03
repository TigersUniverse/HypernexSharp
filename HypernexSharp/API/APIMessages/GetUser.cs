using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class GetUser : APIMessage
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string tokenContent { get; }

        protected override string Endpoint => "getUser";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            if(!string.IsNullOrEmpty(username))
                o.Add("username", username);
            else if(!string.IsNullOrEmpty(userid))
                o.Add("userid", userid);
            if(!string.IsNullOrEmpty(tokenContent))
                o.Add("tokenContent", tokenContent);
            return o;
        }
        
        public GetUser(string tokenContent) => this.tokenContent = tokenContent;
    }
}