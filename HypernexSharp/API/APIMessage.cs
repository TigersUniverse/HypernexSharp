using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HypernexSharp.Libs;

namespace HypernexSharp.API
{
    public abstract class APIMessage
    {
        internal void SendRequest(HypernexSettings settings, Action<APIResult> callback = null)
        {
            Task.Factory.StartNew(async () =>
            {
                JSONNode n = GetNode();
                string d = n.ToString();
                string res = await HTTPTools.POST(settings.APIURL + Endpoint, d);
                if(callback != null)
                    callback.Invoke(new APIResult(res));
            });
        }

        internal void SendForm(HypernexSettings settings, Action<APIResult> callback = null)
        {
            Task.Factory.StartNew(async () =>
            {
                (Stream, Dictionary<string, string>) form = GetFileForm();
                string res = await HTTPTools.POSTFile(settings.APIURL + Endpoint, form.Item2, form.Item1);
                if(callback != null)
                    callback.Invoke(new APIResult(res));
            });
        }

        internal void GetRequest(HypernexSettings settings, Action<APIResult> callback = null)
        {
            Task.Factory.StartNew(async () =>
            {
                string res = await HTTPTools.GET(settings.APIURL + Endpoint + GetQuery());
                if(callback != null)
                    callback.Invoke(new APIResult(res));
            });
        }

        internal void GetAttachment(HypernexSettings settings, Action<Stream> callback = null)
        {
            Task.Factory.StartNew(async () =>
            {
                Stream res = await HTTPTools.GETFile(settings.APIURL + Endpoint + GetQuery());
                if(callback != null)
                    callback.Invoke(res);
            });
        }

        protected abstract string Endpoint { get; }

        protected virtual JSONNode GetNode(){ return new JSONObject(); }
        protected virtual string GetQuery(){ return String.Empty; }
        protected virtual (Stream, Dictionary<string, string>) GetFileForm(){ return (null, null); }
    }
}