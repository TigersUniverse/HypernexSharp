using SimpleJSON;
using HypernexSharp.SocketObjects;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class UpdatedInstance : ISocketResponse
    {
        public string message => "UpdatedInstance";
        public JSONNode Result { get; }

        public InstanceMeta instanceMeta;

        public UpdatedInstance(JSONNode result)
        {
            Result = result;
            instanceMeta = InstanceMeta.FromJSON(result["instanceMeta"]);
        }
    }
}