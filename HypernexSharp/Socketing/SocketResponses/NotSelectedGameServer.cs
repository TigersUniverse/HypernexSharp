using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class NotSelectedGameServer : ISocketResponse
    {
        public string message => "NotSelectedGameServer";
        public JSONNode Result { get; }

        public string TemporaryId;

        public NotSelectedGameServer(JSONNode result)
        {
            Result = result;
            TemporaryId = result["temporaryId"].Value;
        }
    }
}