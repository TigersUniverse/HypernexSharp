using System;
using System.Collections.Generic;

namespace HypernexSharp.APIObjects
{
    public class DistanceObject
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        
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

        public static void Sort(ref List<DistanceObject> distanceObjects, float latitude, float longitude)
        {
            if(distanceObjects.Count <= 1 || latitude == 0 || longitude == 0) return;
            distanceObjects.Sort((a, b) =>
            {
                bool d1z = a.Latitude == 0 && a.Longitude == 0;
                bool d2z = b.Latitude == 0 && b.Longitude == 0;
                if (d1z && !d2z)
                    return 1;
                if (!d1z && d2z)
                    return -1;
                if (d1z && d2z)
                    return 0;
                float d1 = a.GetDistance(latitude, longitude);
                float d2 = b.GetDistance(latitude, longitude);
                return d1.CompareTo(d2);
            });
        }
    }
}