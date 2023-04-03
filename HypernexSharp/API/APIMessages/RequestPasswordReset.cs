using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class RequestPasswordReset : APIMessage
    {
        private string email { get; }

        protected override string Endpoint => "requestPasswordReset";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("email", email);
            return o;
        }

        public RequestPasswordReset(string email) => this.email = email;
    }
}