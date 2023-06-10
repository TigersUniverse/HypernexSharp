using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class ManageAssetToken : APIMessage
    {
        public string userid { get; }
        public string tokenContent { get; }
        public ManageAssetTokenAction action { get; }
        public string assetId { get; }
        public string removeAssetToken { get; set; }

        protected override string Endpoint => "manageAssetToken";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            o.Add("action", (int) action);
            o.Add("assetId", assetId);
            if(!string.IsNullOrEmpty(removeAssetToken))
                o.Add("assetToken", removeAssetToken);
            return o;
        }

        public ManageAssetToken(string userid, string tokenContent, ManageAssetTokenAction action, string assetId)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.action = action;
            this.assetId = assetId;
        }
    }
}