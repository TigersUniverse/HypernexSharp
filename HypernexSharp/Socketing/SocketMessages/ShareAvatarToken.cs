using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class ShareAvatarToken : ISocketMessage
    {
        public string message => "ShareAvatarToken";

        public string targetUserId;
        public string avatarId;
        public string avatarToken;
        
        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("targetUserId", targetUserId);
            o.Add("avatarId", avatarId);
            o.Add("avatarToken", avatarToken);
            return o;
        }
    }
}