using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class SharedAvatarToken : ISocketResponse
    {
        public string message => "SharedAvatarToken";
        public JSONNode Result { get; }

        public string fromUserId;
        public string targetUserId;
        public string avatarId;
        public string avatarToken;
        
        public SharedAvatarToken(JSONNode result)
        {
            Result = result;
            fromUserId = result["fromUserId"].Value;
            targetUserId = result["targetUserId"].Value;
            avatarId = result["avatarId"].Value;
            avatarToken = result["avatarToken"].Value;
        }
    }
}