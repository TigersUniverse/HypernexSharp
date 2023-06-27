namespace HypernexSharp.API.APIMessages
{
    public class FileMeta : APIMessage
    {
        private readonly string endpoint = "filemeta/";
        protected override string Endpoint => endpoint;

        public FileMeta(string userid, string fileid) => endpoint += userid + "/" + fileid;
    }
}