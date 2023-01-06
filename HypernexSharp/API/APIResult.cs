using HypernexSharp.Libs;

namespace HypernexSharp.API
{
    internal class APIResult
    {
        public bool success { get; }
        public string message { get; }
        public JSONObject result { get; }

        public APIResult(string json)
        {
            JSONNode node = JSON.Parse(json);
            success = node["success"].AsBool;
            message = node["message"].Value;
            result = node["result"].AsObject;
        }
    }
}