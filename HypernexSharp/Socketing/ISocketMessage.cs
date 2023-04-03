using SimpleJSON;

namespace HypernexSharp.Socketing
{
    internal interface ISocketMessage
    {
        string message { get; }
        JSONObject GetArgs();
    }
}