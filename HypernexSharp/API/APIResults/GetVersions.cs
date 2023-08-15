using System.Collections.Generic;
using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class GetVersions
    {
        public string Name { get; }
        public List<string> Versions { get; } = new List<string>();

        internal GetVersions(JSONObject o)
        {
            Name = o["Name"].Value;
            foreach (JSONNode value in o["Versions"].AsArray.Values)
                Versions.Add(value.Value);
        }
    }
}