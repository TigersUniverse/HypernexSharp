using SimpleJSON;

namespace HypernexSharp.Socketing.SocketResponses
{
    public class EmptyResult : ISocketResponse
    {
        public string message { get; }
        private JSONNode _ = new JSONObject();
        public JSONNode Result => _;

        public EmptyResult(string message) => this.message = message;
    }
}