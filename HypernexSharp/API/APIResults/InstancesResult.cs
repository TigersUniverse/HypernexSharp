using System.Collections.Generic;
using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class InstancesResult
    {
        public List<SafeInstance> SafeInstances { get; set; } = new List<SafeInstance>();
        
        public InstancesResult(){}

        internal InstancesResult(JSONNode result)
        {
            foreach (KeyValuePair<string,JSONNode> keyValuePair in result["SafeInstances"].AsArray)
                SafeInstances.Add(SafeInstance.FromJSON(keyValuePair.Value));
        }
    }
}