using System;
using System.Collections.Generic;
using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    [Serializable]
    public class WorldMeta
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public WorldPublicity Publicity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; } = new List<string>();
        public string ThumbnailURL { get; set; }
        public List<string> IconURLs { get; } = new List<string>();
        public List<string> ServerScripts { get; } = new List<string>();
        public BuildPlatform BuildPlatform { get; set; }
        public List<Builds> Builds { get; } = new List<Builds>();

        public JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("Id", Id);
            o.Add("OwnerId", OwnerId);
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
            JSONArray scriptsarray = new JSONArray();
            foreach (string script in ServerScripts)
                scriptsarray.Add(script);
            o.Add("ServerScripts", scriptsarray);
            o.Add("BuildPlatform", (int) BuildPlatform);
            return o;
        }

        public static WorldMeta FromJSON(JSONNode node)
        {
            WorldMeta worldMeta = new WorldMeta(node["Id"].Value, node["OwnerId"].Value,
                (WorldPublicity) node["Publicity"].AsInt, node["Name"].Value, node["Description"].Value,
                node["ThumbnailURL"].Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Tags"].AsArray)
                worldMeta.Tags.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["IconURLs"].AsArray)
                worldMeta.IconURLs.Add(keyValuePair.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["ServerScripts"].AsArray)
                worldMeta.IconURLs.Add(keyValuePair.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Builds"].AsArray)
                worldMeta.Builds.Add(APIObjects.Builds.FromJSON(keyValuePair.Value));
            return worldMeta;
        }

        public WorldMeta(string Id, string OwnerId, WorldPublicity Publicity, string Name, string Description,
            string ThumbnailURL)
        {
            this.Id = Id;
            this.OwnerId = OwnerId;
            this.Publicity = Publicity;
            this.Name = Name;
            this.Description = Description;
            this.ThumbnailURL = ThumbnailURL;
        }
    }
}