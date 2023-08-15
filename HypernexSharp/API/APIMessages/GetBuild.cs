using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class GetBuild : APIMessage
    {
        private string userid { get; }
        private string tokenContent { get; }
        private int buildArtifact { get; }

        private string endpoint = "getBuild/";
        protected override string Endpoint => endpoint;

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("buildArtifact", buildArtifact);
            o.Add("userid", userid);
            o.Add("tokenContent", tokenContent);
            return o;
        }
        
        public GetBuild(string name, string version, int buildArtifact, string userid, string tokenContent)
        {
            endpoint += $"{name}/{version}";
            this.userid = userid;
            this.tokenContent = tokenContent;
            this.buildArtifact = buildArtifact;
        }
    }
}