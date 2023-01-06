using HypernexSharp.Libs;

namespace HypernexSharp.APIObjects
{
    public class FileData
    {
        public string UserId { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public UploadType UploadType { get; set; }
        public string Key { get; set; }

        public static FileData FromJSON(JSONNode node) => new FileData
        {
            UserId = node["UserId"].Value,
            FileId = node["FileId"].Value,
            FileName = node["FileName"].Value,
            UploadType = (UploadType) node["FileType"].AsInt,
            Key = node["Key"].Value
        };
    }
}