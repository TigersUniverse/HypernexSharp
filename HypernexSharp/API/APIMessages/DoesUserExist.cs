using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class DoesUserExist : APIMessage
    {
        public string userid { get; set; }

        protected override string Endpoint => "doesUserExist";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            return o;
        }

        protected override string GetQuery() => "?userid=" + userid;
    }
}