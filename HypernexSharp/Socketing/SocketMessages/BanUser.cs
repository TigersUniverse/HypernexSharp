﻿using SimpleJSON;

namespace HypernexSharp.Socketing.SocketMessages
{
    public class BanUser : ISocketMessage
    {
        public string message => "BanUser";
        
        public string InstanceId;
        public string UserId;

        public JSONObject GetArgs()
        {
            JSONObject o = new JSONObject();
            o.Add("instanceId", InstanceId);
            o.Add("userId", UserId);
            return o;
        }
    }
}