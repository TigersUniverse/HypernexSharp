using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class RequestedInstanceCreated : ISocketResponse
    {
        public string message => "RequestedInstanceCreated";
        public JSONNode Result { get; }

        public string temporaryId;

        public RequestedInstanceCreated(JSONNode result)
        {
            Result = result;
            temporaryId = result["temporaryId"].Value;
        }
    }
}