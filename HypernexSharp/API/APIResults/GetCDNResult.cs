using System.Collections.Generic;
using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class GetCDNResult
    {
        public List<CDNServer> Servers { get; } = new List<CDNServer>();
        
        public GetCDNResult(){}

        internal GetCDNResult(JSONNode result)
        {
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result["servers"].AsArray)
                Servers.Add(new CDNServer(keyValuePair.Value));
        }
    }
}