using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class WorldUpdateResult
    {
        public FileData UploadData { get; }
        public string WorldId { get; }
        
        public WorldUpdateResult(){}

        internal WorldUpdateResult(JSONNode result)
        {
            UploadData = FileData.FromJSON(result);
            WorldId = result["WorldId"].Value;
        }
    }
}