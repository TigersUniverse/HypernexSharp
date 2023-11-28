using System;
using HypernexSharp.APIObjects;

namespace HypernexSharp.API.APIMessages
{
    public class GetPopularity : APIMessage
    {
        protected override string Endpoint { get; }

        public GetPopularity(UploadType uploadType, PopularityType popularityType, int itemsPerPage = 50, int page = 0)
        {
            if (uploadType == UploadType.Media)
                throw new Exception("Cannot get popularity for Media");
            string t = uploadType == UploadType.World ? "world" : "avatar";
            Endpoint = $"popularity/{t}/{(int)popularityType}/{itemsPerPage}/{page}";
        }
    }
}