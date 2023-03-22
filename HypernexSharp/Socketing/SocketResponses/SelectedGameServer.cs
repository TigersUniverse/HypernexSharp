using HypernexSharp.Libs;
using HypernexSharp.SocketObjects;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class SelectedGameServer : ISocketResponse
    {
        public string message => "SelectedGameServer";
        public JSONNode Result { get; }

        public InstanceMeta instanceMeta;

        public SelectedGameServer(JSONNode result)
        {
            Result = result;
            instanceMeta = InstanceMeta.FromJSON(result["instanceMeta"]);
        }
    }
}