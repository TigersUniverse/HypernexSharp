using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketResponses
{
    // NOTE: You will get this message when an authenticated User is going to connect to the Game Server
    public class TempUserToken : ISocketResponse
    {
        public string message => "TempUserToken";
        public JSONNode Result { get; }

        public string tempUserToken;
        public string userId;
        
        public TempUserToken(JSONNode result)
        {
            Result = result;
            tempUserToken = result["tempUserToken"].Value;
            userId = result["userId"].Value;
        }
    }
}