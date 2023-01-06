using System.Collections.Generic;
using System.IO;
using HypernexSharp.APIObjects;

namespace HypernexSharp.API.APIMessages
{
    public class Upload : APIMessage
    {
        public Stream file { get; }
        public string userid { get; }
        public string tokenContent { get; }
        public AvatarMeta avatarMeta { get; }
        public WorldMeta worldMeta { get; }

        protected override string Endpoint => "upload";

        protected override (Stream, Dictionary<string, string>) GetFileForm()
        {
            Dictionary<string, string> collection = new Dictionary<string, string>();
            collection.Add("userid", userid);
            collection.Add("tokenContent", tokenContent);
            if(avatarMeta != null)
                collection.Add("avatarMeta", avatarMeta.ToString());
            else if(worldMeta != null)
                collection.Add("worldMeta", worldMeta.ToString());
            return (file, collection);
        }

        public Upload(string userid, string tokenContent, Stream file)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.file = file;
        }
        
        public Upload(string userid, string tokenContent, Stream file, AvatarMeta avatarMeta)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.file = file;
            this.avatarMeta = avatarMeta;
        }
        
        public Upload(string userid, string tokenContent, Stream file, WorldMeta worldMeta)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.file = file;
            this.worldMeta = worldMeta;
        }
    }
}