using HypernexSharp.Libs;

namespace HypernexSharp.APIObjects
{
    public class Builds
    {
        public string FileId { get; set; }
        public BuildPlatform BuildPlatform { get; set; }
        
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
            return builds;
        }
    }
}