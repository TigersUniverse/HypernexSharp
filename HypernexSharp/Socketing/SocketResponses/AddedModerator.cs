using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class AddedModerator : ISocketResponse
    {
        public string message => "AddedModerator";
        public JSONNode Result { get; }

        public string instanceId;
        public string userId;

        public AddedModerator(JSONNode result)
        {
            Result = result;
            instanceId = result["instanceId"].Value;
            userId = result["userId"].Value;
        }
    }
}