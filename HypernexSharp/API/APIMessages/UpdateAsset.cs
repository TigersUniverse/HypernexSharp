using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class UpdateAsset : APIMessage
    {
        private string userid { get; }
        private string tokenContent { get; }
        private string fileid { get; }
        private AvatarMeta avatarmeta { get; }
        private WorldMeta worldmeta { get; }

        private string endpoint;
        protected override string Endpoint => endpoint;

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            if(!string.IsNullOrEmpty(fileid))
                o.Add("fileid", fileid);
            if(avatarmeta != null)
                o.Add("avatarmeta", avatarmeta.GetNode().ToString());
            else if(worldmeta != null)
                o.Add("worldmeta", worldmeta.GetNode().ToString());
            return o;
        }
        
        public UpdateAsset(string userid, string tokenContent, AvatarMeta avatarMeta)
        {
            endpoint = "update/avatar";
            this.userid = userid;
            this.tokenContent = tokenContent;
            avatarmeta = avatarMeta;
        }

        public UpdateAsset(string userid, string tokenContent, string fileid, AvatarMeta avatarMeta) : this(userid,
            tokenContent, avatarMeta) => this.fileid = fileid;
        
        public UpdateAsset(string userid, string tokenContent, WorldMeta worldMeta)
        {
            endpoint = "update/world";
            this.userid = userid;
            this.tokenContent = tokenContent;
            worldmeta = worldMeta;
        }

        public UpdateAsset(string userid, string tokenContent, string fileid, WorldMeta worldMeta) : this(userid,
            tokenContent, worldMeta) => this.fileid = fileid;
    }
}