using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class SimpleTargetUserId : APIMessage
    {
        private string userid { get; }
        private string tokenContent { get; }
        private string targetUserId { get; }

        private string endpoint;
        protected override string Endpoint => endpoint;

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            o.Add("targetUserId", targetUserId);
            return o;
        }
        
        public SimpleTargetUserId(string endpoint, string userid, string tokenContent, string targetUserId)
        {
            this.endpoint = endpoint;
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.targetUserId = targetUserId;
        }
    }
}