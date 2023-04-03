using SimpleJSON;

namespace HypernexSharp.Socketing
{
    public interface ISocketResponse
    {
        string message { get; }
        JSONNode Result { get; }
    }
}