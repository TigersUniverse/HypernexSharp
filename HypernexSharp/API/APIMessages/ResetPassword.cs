using HypernexSharp.Libs;

namespace HypernexSharp.API.APIMessages
{
    public class ResetPassword : APIMessage
    {
        private string userid { get; }
        private string newPassword { get; }
        private string tokenContent { get; }
        private string passwordResetContent { get; }

        protected override string Endpoint => "resetPassword";

        protected override JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("userid", userid);
            if (!string.IsNullOrEmpty(tokenContent))
                o.Add("tokenContent", tokenContent);
            else if(!string.IsNullOrEmpty(passwordResetContent))
                o.Add("passwordResetContent", passwordResetContent);
            o.Add("newPassword", newPassword);
            return o;
        }
        
        public ResetPassword(string userid, string newPassword, string tokenContent = "", string passwordResetContent = "")
        {
            this.userid = userid;
            this.newPassword = newPassword;
            if (!string.IsNullOrEmpty(tokenContent))
                this.tokenContent = tokenContent;
            else if(!string.IsNullOrEmpty(passwordResetContent))
                this.passwordResetContent = passwordResetContent;
        }
    }
}