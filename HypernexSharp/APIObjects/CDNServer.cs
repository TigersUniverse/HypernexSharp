using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class CDNServer : DistanceObject
    {
        public string Server;

        internal CDNServer(JSONNode j)
        {
            Server = j["Server"].Value;
            Latitude = j["Latitude"].AsFloat;
            Longitude = j["Longitude"].AsFloat;
        }
    }
}