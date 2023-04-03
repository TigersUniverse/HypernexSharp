using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class FailedToShareAvatarToken : ISocketResponse
    {
        public string message => "FailedToShareAvatarToken";
        public JSONNode Result { get; }

        public string targetUserId;
        public string avatarId;
        public string avatarToken;
        
        public FailedToShareAvatarToken(JSONNode result)
        {
            Result = result;
            targetUserId = result["targetUserId"].Value;
            avatarId = result["avatarId"].Value;
            avatarToken = result["avatarToken"].Value;
        }
    }
}