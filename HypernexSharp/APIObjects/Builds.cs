using System.Collections.Generic;
using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class Builds
    {
        public string FileId { get; set; }
        public BuildPlatform BuildPlatform { get; set; }
        public List<string> ServerScripts { get; } = new List<string>();

        public JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("FileId", FileId);
            o.Add("BuildPlatform", (int) BuildPlatform);
            return o;
        }

        public static Builds FromJSON(JSONNode node)
        {
            Builds builds = new Builds
            {
                FileId = node["FileId"].Value,
                BuildPlatform = (BuildPlatform) node["BuildPlatform"].AsInt
            };
            if(node.HasKey("ServerScripts"))
                foreach (KeyValuePair<string,JSONNode> keyValuePair in node["ServerScripts"].AsArray)
                    builds.ServerScripts.Add(keyValuePair.Value.Value);
            return builds;
        }
    }
}