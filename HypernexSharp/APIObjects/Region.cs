using System;
using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class Region
    {
        public string ContinentCode;
        public string City;
        public string State;
        public string Country;
        public float Latitude;
        public float Longitude;
        
        // https://stackoverflow.com/a/51839058
        private double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
    
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
        
        public float GetDistance(float la, float lo) => (float) GetDistance(lo, la, Longitude, Latitude);

        public static Region FromJSON(JSONNode node) => new Region
        {
            ContinentCode = node["ContinentCode"].Value,
            City = node["City"].Value,
            State = node["State"].Value,
            Country = node["Country"].Value,
            Latitude = node["Latitude"].AsFloat,
            Longitude = node["Longitude"].AsFloat
        };
    }
}