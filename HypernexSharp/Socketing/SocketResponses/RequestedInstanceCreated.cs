using SimpleJSON;
using HypernexSharp.SocketObjects;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class RequestedInstanceCreated : ISocketResponse
    {
        public string message => "RequestedInstanceCreated";
        public JSONNode Result { get; }

        public string temporaryId;
        public InstanceProtocol instanceProtocol;

        public RequestedInstanceCreated(JSONNode result)
        {
            Result = result;
            temporaryId = result["temporaryId"].Value;
            instanceProtocol = (InstanceProtocol) result["instanceProtocol"].AsInt;
        }
    }
}