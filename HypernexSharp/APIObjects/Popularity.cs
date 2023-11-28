using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class Popularity
    {
        public string Id { get; set; }
        public PopularityObject Hourly { get; set; }
        public PopularityObject Daily { get; set; }
        public PopularityObject Weekly { get; set; }
        public PopularityObject Monthly { get; set; }
        public PopularityObject Yearly { get; set; }

        public JSONNode ToJSON()
        {
            JSONObject o = new JSONObject();
            o.Add("Id", Id);
            o.Add("Hourly", Hourly.ToJSON());
            o.Add("Daily", Daily.ToJSON());
            o.Add("Weekly", Weekly.ToJSON());
            o.Add("Monthly", Monthly.ToJSON());
            o.Add("Yearly", Yearly.ToJSON());
            return o;
        }

        public static Popularity FromJSON(JSONNode node)
        {
            Popularity popularity = new Popularity();
            popularity.Id = node["Id"];
            popularity.Hourly = PopularityObject.FromJSON(node["Hourly"]);
            popularity.Daily = PopularityObject.FromJSON(node["Daily"]);
            popularity.Weekly = PopularityObject.FromJSON(node["Weekly"]);
            popularity.Monthly = PopularityObject.FromJSON(node["Monthly"]);
            popularity.Yearly = PopularityObject.FromJSON(node["Yearly"]);
            return popularity;
        }
    }
}