namespace HypernexSharp
{
    public class HypernexSettings
    {
        public string Username { get; }
        public string Password { get; }
        public string Email { get; }
        public string InviteCode { get; }
        public string TwoFACode { get; }

        public string TargetDomain { get; set; }
        public string APIURL => "https://" + TargetDomain + "/api/v1/";
        
        public HypernexSettings(){}

        public HypernexSettings(string username, string password, string twofacode = "")
        {
            Username = username;
            Password = password;
            TwoFACode = twofacode;
        }
        
        public HypernexSettings(string username, string email, string password, string inviteCode, string twofacode = "")
        {
            Username = username;
            Password = password;
            Email = email;
            InviteCode = inviteCode;
            TwoFACode = twofacode;
        }
    }
}