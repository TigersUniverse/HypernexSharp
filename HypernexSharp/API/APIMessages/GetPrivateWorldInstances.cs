using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class GetPrivateWorldInstances : APIMessage
    {
        private string worldid { get; }
        private string userid { get; }
        private string tokenContent { get; }

        protected override string Endpoint => "instances";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("worldid", worldid);
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            return o;
        }
        
        public GetPrivateWorldInstances(string worldid, string userid, string tokenContent)
        {
            this.worldid = worldid;
            this.userid = userid;
            this.tokenContent = tokenContent;
        }
    }
}