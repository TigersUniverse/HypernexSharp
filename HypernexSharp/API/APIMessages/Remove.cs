using HypernexSharp.APIObjects;
using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class Remove : APIMessage
    {
        private string userid { get; }
        private string avatarid { get; }
        private string worldid { get; }
        private string fileid { get; }
        private string tokenContent { get; }

        private string endpoint = "remove/";
        protected override string Endpoint => endpoint;

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            if(!string.IsNullOrEmpty(avatarid))
                o.Add("avatarid", avatarid);
            else if(!string.IsNullOrEmpty(worldid))
                o.Add("worldid", worldid);
            else if(!string.IsNullOrEmpty(fileid))
                o.Add("fileid", fileid);
            return o;
        }
        
        public Remove(UploadType removeType, string userid, string tokenContent, string id)
        {
            switch (removeType)
            {
                case UploadType.Avatar:
                    endpoint += "avatar";
                    avatarid = id;
                    break;
                case UploadType.World:
                    endpoint += "world";
                    worldid = id;
                    break;
                case UploadType.Media:
                    endpoint += "file";
                    fileid = id;
                    break;
            }
            this.userid = userid;
            this.tokenContent = tokenContent;
        }
    }
}