using SimpleJSON;

namespace HypernexSharp.API.APIMessages
{
    public class CreateUser : APIMessage
    {
        public string username { get; }
        public string password { get; }
        public string email { get; }
        public string inviteCode { get; }
        protected override string Endpoint => "createUser";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("username", username);
            o.Add("password", password);
            o.Add("email", email);
            if(!string.IsNullOrEmpty(inviteCode))
                o.Add("inviteCode", inviteCode);
            return o;
        }

        public CreateUser(string u, string p, string e, string i = "")
        {
            username = u;
            password = p;
            email = e;
            inviteCode = i;
        }
    }
}