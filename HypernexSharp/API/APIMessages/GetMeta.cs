using HypernexSharp.APIObjects;

namespace HypernexSharp.API.APIMessages
{
    public class GetMeta : APIMessage
    {
        private string endpoint = "meta/";
        protected override string Endpoint => endpoint;
        
        public GetMeta(UploadType metaType, string id)
        {
            switch (metaType)
            {
                case UploadType.Avatar:
                    endpoint += "avatar/" + id;
                    break;
                case UploadType.World:
                    endpoint += "world/" + id;
                    break;
            }
        }
    }
}