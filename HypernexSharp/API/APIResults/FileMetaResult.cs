using HypernexSharp.APIObjects;
using SimpleJSON;

namespace HypernexSharp.API.APIResults
{
    public class FileMetaResult
    {
        public FileData FileMeta { get; }
        
        public FileMetaResult(){}

        internal FileMetaResult(JSONNode result) => FileMeta = FileData.FromJSON(result);
    }
}