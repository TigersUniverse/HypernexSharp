using System.Collections.Generic;
using HypernexSharp.Libs;

namespace HypernexSharp.APIObjects
{
    public class WorldMeta
    {
        public string Id { get; set; }
        public WorldPublicity Publicity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; } = new List<string>();
        public string ThumbnailURL { get; set; }
        public List<string> IconURLs { get; } = new List<string>();

        public JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("Id", Id);
            o.Add("Publicity", (int) Publicity);
            o.Add("Name", Name);
            o.Add("Description", Description);
            JSONArray tagarray = new JSONArray();
            foreach (string tag in Tags)
                tagarray.Add(tag);
            o.Add("Tags", tagarray);
            o.Add("ThumbnailURL", ThumbnailURL);
            JSONArray iconarray = new JSONArray();
            foreach (string iconUrL in IconURLs)
                iconarray.Add(iconUrL);
            o.Add("IconURLs", iconarray);
            return o;
        }

        public static WorldMeta FromJSON(JSONNode node)
        {
            WorldMeta worldMeta = new WorldMeta
            {
                Id = node["Id"].Value,
                Publicity = (WorldPublicity) node["Publicity"].AsInt,
                Name = node["Name"].Value,
                Description = node["Description"].Value,
                ThumbnailURL = node["ThumbnailURL"].Value
            };
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Tags"].AsArray)
                worldMeta.Tags.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["IconURLs"].AsArray)
                worldMeta.IconURLs.Add(keyValuePair.Value);
            return worldMeta;
        }
    }
}