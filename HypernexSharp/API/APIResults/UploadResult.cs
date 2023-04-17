using HypernexSharp.APIObjects;

namespace HypernexSharp.API.APIResults
{
    public class UploadResult
    {
        public FileData UploadData { get; set; }
        public string AvatarId { get; set; }
        public string WorldId { get; set; }
    }
}