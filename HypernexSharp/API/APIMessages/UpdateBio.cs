using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class UpdateBio : APIMessage
    {
        private string userid { get; }
        private string tokenContent { get; }
        private Bio bio { get; }

        protected override string Endpoint => "updateBio";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            o.Add("bio", bio.ToJSON());
            return o;
        }
        
        public UpdateBio(string userid, string tokenContent, Bio bio)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.bio = bio;
        }
    }
}