namespace HypernexSharp
{
    public class HypernexSettings
    {
        public string Username { get; }
        public string Password { get; }
        public string Email { get; }
        public string InviteCode { get; }
        public string TwoFACode { get; }
        public string UserId { get; }
        public string TokenContent { get; }

        public string TargetDomain { get; set; }
        public bool IsHTTP { get; set; } = false;
        public string APIVersion { get; set; } = "v1";

        public string APIURL
        {
            get
            {
                if (IsHTTP)
                    return "http://" + TargetDomain + "/api/" + APIVersion + "/";
                return "https://" + TargetDomain + "/api/" + APIVersion + "/";
            }
        }
        
        public HypernexSettings(){}

        public HypernexSettings(string userid, string tokenContent)
        {
            UserId = userid;
            TokenContent = tokenContent;
        }

        public HypernexSettings(string username, string password, string twofacode = "")
        {
            Username = username;
            Password = password;
            TwoFACode = twofacode;
        }
        
        public HypernexSettings(string username, string email, string password, string inviteCode = "")
        {
            Username = username;
            Password = password;
            Email = email;
            InviteCode = inviteCode;
        }
    }
}