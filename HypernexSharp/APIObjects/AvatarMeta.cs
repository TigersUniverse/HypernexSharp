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
            return o;
        }

        public static AvatarMeta FromJSON(JSONNode node)
        {
            AvatarMeta avatarMeta = new AvatarMeta
            {
                Id = node["Id"].Value,
                Publicity = (AvatarPublicity) node["Publicity"].AsInt,
                Name = node["Name"].Value,
                Description = node["Description"].Value,
                ImageURL = node["ImageURL"].Value
            };
            foreach (KeyValuePair<string,JSONNode> keyValuePair in node["Tags"].AsArray)
                avatarMeta.Tags.Add(keyValuePair.Value.Value);
            return avatarMeta;
        }
    }
}