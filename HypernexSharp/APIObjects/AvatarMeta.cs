using System.Collections.Generic;
using HypernexSharp.Libs;

namespace HypernexSharp.APIObjects
{
    public class AvatarMeta
    {
        public string Id { get; set; }
        public AvatarPublicity Publicity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; } = new List<string>();
        public string ImageURL { get; set; }
        public BuildPlatform BuildPlatform { get; set; }
        public List<Builds> Builds { get; } = new List<Builds>();

        public JSONNode GetNode()
        {
            JSONObject o = new JSONObject();
            o.Add("Id", Id);
            o.Add("Publicity", (int) Publicity);
            o.Add("Name", Name);
            o.Add("Description", Description);
            JSONArray array = new JSONArray();
            foreach (string tag in Tags)
                array.Add(tag);
            o.Add("Tags", array);
            o.Add("ImageURL", ImageURL);
            o.Add("BuildPlatform", (int) BuildPlatform);
            return o;
        }

        public static AvatarMeta FromJSON(JSONNode node)
        {
            AvatarMeta avatarMeta = new AvatarMeta(node["Id"].Value, (AvatarPublicity) node["Publicity"].AsInt,
                node["Name"].Value, node["Description"].Value, node["ImageURL"].Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Tags"].AsArray)
                avatarMeta.Tags.Add(keyValuePair.Value.Value);
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Builds"].AsArray)
                avatarMeta.Builds.Add(APIObjects.Builds.FromJSON(keyValuePair.Value));
            return avatarMeta;
        }

        public AvatarMeta(string Id, AvatarPublicity Publicity, string Name, string Description, string ImageURL)
        {
            this.Id = Id;
            this.Publicity = Publicity;
            this.Name = Name;
            this.Description = Description;
            this.ImageURL = ImageURL;
        }
    }
}