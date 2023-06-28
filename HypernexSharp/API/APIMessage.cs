using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SimpleJSON;

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
            }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }

        internal void SendForm(HypernexSettings settings, Action<APIResult> callback = null)
        {
            Task.Factory.StartNew(async () =>
                {
                    (FileStream, Dictionary<string, string>) form = GetFileForm();
                    string res = await HTTPTools.POSTFile(settings.APIURL + Endpoint, form.Item2, form.Item1);
                    if (callback != null)
                        callback.Invoke(new APIResult(res));
                }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
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
            }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }
        
        internal void GetAttachment(HypernexSettings settings, Action<string, Stream> callback = null)
        {
            Task.Factory.StartNew(async () =>
            {
                (string, Stream) res = await HTTPTools.GETFileAndName(settings.APIURL + Endpoint + GetQuery());
                if(callback != null)
                    callback.Invoke(res.Item1, res.Item2);
            }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }

        protected abstract string Endpoint { get; }

        protected virtual JSONNode GetNode(){ return new JSONObject(); }
        protected virtual string GetQuery(){ return String.Empty; }
        protected virtual (FileStream, Dictionary<string, string>) GetFileForm(){ return (null, null); }
    }
}