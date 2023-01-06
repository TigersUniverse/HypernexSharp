using HypernexSharp.APIObjects;

namespace HypernexSharp.API.APIResults
{
    public class LoginResult
    {
        public APIObjects.LoginResult Result { get; set; }
        public WarnStatus WarnStatus { get; set; }
        public BanStatus BanStatus { get; set; }
        public Token Token { get; set; }
    }
}