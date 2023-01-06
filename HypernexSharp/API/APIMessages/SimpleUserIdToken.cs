using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class SimpleUserIdToken : APIMessage
    {
        private string userid { get; }
        private string tokenContent { get; }

        private string endpoint;
        protected override string Endpoint => endpoint;

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            return o;
        }
        
        public SimpleUserIdToken(string endpoint, string userid, string tokenContent)
        {
            this.endpoint = endpoint;
            this.userid = userid;
            this.tokenContent = tokenContent;
        }
    }
}