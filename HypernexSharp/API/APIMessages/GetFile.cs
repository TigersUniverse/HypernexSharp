namespace HypernexSharp.API.APIMessages
{
    public class GetFile : APIMessage
    {
        private string endpoint;
        protected override string Endpoint => endpoint;

        public GetFile(string userid, string fileid) => endpoint = "file/" + userid + "/" + fileid;
        public GetFile(string userid, string fileid, string fileToken) =>
            endpoint = "file/" + userid + "/" + fileid + "/" + fileToken;
    }
}