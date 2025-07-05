using System.Collections.Generic;
using System.IO;
using HypernexSharp.APIObjects;

namespace HypernexSharp.API.APIMessages
{
    public class Upload : APIMessage
    {
        public FileStream file { get; }
        public string userid { get; }
        public string tokenContent { get; }

        protected override string Endpoint => "upload";
        protected override string APIURL => cdn;

        private string cdn;

        protected override (FileStream, Dictionary<string, string>) GetFileForm()
        {
            Dictionary<string, string> collection = new Dictionary<string, string>();
            collection.Add("userid", userid);
            collection.Add("tokenContent", tokenContent);
            return (file, collection);
        }

        public Upload(string userid, string tokenContent, FileStream file, string cdn)
        {
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.file = file;
            this.cdn = cdn;
        }
    }
}