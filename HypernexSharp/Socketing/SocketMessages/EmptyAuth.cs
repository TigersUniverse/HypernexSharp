using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    internal class EmptyAuth : ISocketMessage
    {
        public string message => "auth";
        public JSONObject _ => new JSONObject();
        public JSONObject GetArgs() => _;
    }
}