using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class PopularityObject
    {
        public int Usages { get; set; }

        public JSONNode ToJSON()
        {
            JSONObject o = new JSONObject();
            o.Add("Usages", Usages);
            return o;
        }

        public static PopularityObject FromJSON(JSONNode node) => new PopularityObject
        {
            Usages = node["Usages"].AsInt
        };
    }
}