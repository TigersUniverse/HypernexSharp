using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class Region
    {
        public string ContinentCode;
        public string City;
        public string State;
        public string Country;

        public static Region FromJSON(JSONNode node) => new Region
        {
            ContinentCode = node["ContinentCode"].Value,
            City = node["City"].Value,
            State = node["State"].Value,
            Country = node["Country"].Value
        };
    }
}