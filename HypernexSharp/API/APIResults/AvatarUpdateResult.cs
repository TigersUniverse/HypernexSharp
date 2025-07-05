using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class AvatarUpdateResult
    {
        public FileData UploadData { get; }
        public string AvatarId { get; }
        
        public AvatarUpdateResult(){}

        internal AvatarUpdateResult(JSONNode result)
        {
            UploadData = FileData.FromJSON(result);
            AvatarId = result["AvatarId"].Value;
        }
    }
}