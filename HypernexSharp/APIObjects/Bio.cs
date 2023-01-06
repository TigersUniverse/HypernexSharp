using HypernexSharp.Libs;

namespace HypernexSharp.APIObjects
{
    public class Bio
    {
        public bool isPrivateAccount { get; set; }
        public Status Status { get; set; }
        public string StatusText { get; set; }
        public string Description { get; set; }
        public string PfpURL { get; set; }
        public string BannerURL { get; set; }
        public string DisplayName { get; set; }
        public Pronouns Pronouns { get; set; }
        public SetPronouns SetPronouns;

        public JSONNode ToJSON()
        {
            JSONObject o = new JSONObject();
            o.Add("isPrivateAccount", isPrivateAccount);
            o.Add("Status", (int) Status);
            o.Add("StatusText", StatusText);
            o.Add("Description", Description);
            o.Add("PfpURL", PfpURL);
            o.Add("BannerURL", BannerURL);
            o.Add("DisplayName", DisplayName);
            if (SetPronouns != null)
            {
                if(SetPronouns.Remove)
                    o.Add("Pronouns", "remove");
                else
                    o.Add("Pronouns", SetPronouns.GetNode());
            }
            return o;
        }

        public static Bio FromJSON(JSONNode node)
        {
            Bio bio = new Bio
            {
                isPrivateAccount = node["isPrivateAccount"].AsBool,
                Status = (Status) node["Status"].AsInt,
                StatusText = node["StatusText"].Value,
                Description = node["Description"].Value,
                PfpURL = node["PfpURL"].Value,
                BannerURL = node["BannerURL"].Value,
                DisplayName = node["DisplayName"].Value
            };
            if(node.HasKey("Pronouns"))
                bio.Pronouns = Pronouns.FromJSON(node["Pronouns"]);
            return bio;
        }
    }
}