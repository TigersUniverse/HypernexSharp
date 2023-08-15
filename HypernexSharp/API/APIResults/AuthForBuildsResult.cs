using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class AuthForBuildsResult
    {
        public bool AuthForBuilds { get; }

        internal AuthForBuildsResult(JSONObject o) => AuthForBuilds = o["authForBuilds"].AsBool;
    }
}