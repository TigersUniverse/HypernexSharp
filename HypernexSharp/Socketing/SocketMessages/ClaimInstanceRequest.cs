using System;
using HypernexSharp.Libs;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class ClaimInstanceRequest : ISocketMessage
    {
        public string message => "ClaimInstanceRequest";
        
        public string TemporaryId;
        public string Uri;

        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("TemporaryId", TemporaryId);
            o.Add("Uri", Uri);
            return o;
        }
    }
}