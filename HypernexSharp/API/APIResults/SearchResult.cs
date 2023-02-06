using System.Collections.Generic;
using HypernexSharp.Libs;

namespace HypernexSharp.API.APIResults
{
    public class SearchResult
    {
        public List<string> Candidates { get; }

        public SearchResult() => Candidates = new List<string>();
        
        public SearchResult(JSONArray array)
        {
            Candidates = new List<string>();
            foreach (KeyValuePair<string,JSONNode> keyValuePair in array)
                Candidates.Add(keyValuePair.Value.Value);
        }
    }
}