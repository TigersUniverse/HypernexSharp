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
        protected virtual string APIURL => String.Empty;

        private string GetAPIURL(HypernexSettings settings) => !string.IsNullOrEmpty(APIURL) ? APIURL : settings.APIURL;

        internal void SendRequest(HypernexSettings settings, Action<APIResult> callback = null, Action<int> progress = null)
        {
            Task.Factory.StartNew(async () =>
            {
                JSONNode n = GetNode();
                string d = n.ToString();
                string res = await HTTPTools.POST(GetAPIURL(settings) + Endpoint, d, progress);
                if(callback != null)
                    callback.Invoke(new APIResult(res));
            }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }

        internal void SendForm(HypernexSettings settings, Action<APIResult> callback = null, Action<int> progress = null)
        {
            Task.Factory.StartNew(async () =>
                {
                    (FileStream, Dictionary<string, string>) form = GetFileForm();
                    string res = await HTTPTools.POSTFile(GetAPIURL(settings) + Endpoint, form.Item2, form.Item1, progress);
                    if (callback != null)
                        callback.Invoke(new APIResult(res));
                }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }

        internal void GetRequest(HypernexSettings settings, Action<APIResult> callback = null)
        {
            Task.Factory.StartNew(async () =>
            {
                string res = await HTTPTools.GET(GetAPIURL(settings) + Endpoint + GetQuery());
                if(callback != null)
                    callback.Invoke(new APIResult(res));
            });
        }

        internal void GetAttachment(HypernexSettings settings, Action<Stream> callback = null, Action<int> progress = null)
        {
            Task.Factory.StartNew(async () =>
            {
                Stream res = await HTTPTools.GETFile(GetAPIURL(settings) + Endpoint + GetQuery(), progress);
                if(callback != null)
                    callback.Invoke(res);
            }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }
        
        internal void GetAttachment(HypernexSettings settings, Action<string, Stream> callback = null, Action<int> progress = null)
        {
            Task.Factory.StartNew(async () =>
            {
                (string, Stream) res = await HTTPTools.GETFileAndName(GetAPIURL(settings) + Endpoint + GetQuery(), progress);
                if(callback != null)
                    callback.Invoke(res.Item1, res.Item2);
            }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }

        internal void PostGetAttachment(HypernexSettings settings, Action<Stream> callback = null, Action<int> progress = null)
        {
            Task.Factory.StartNew(async () =>
                {
                    JSONNode n = GetNode();
                    string d = n.ToString();
                    Stream res = await HTTPTools.POSTGetFile(GetAPIURL(settings) + Endpoint, d, progress);
                    if(callback != null)
                        callback.Invoke(res);
                }, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }

        protected abstract string Endpoint { get; }

        protected virtual JSONNode GetNode(){ return new JSONObject(); }
        protected virtual string GetQuery(){ return String.Empty; }
        protected virtual (FileStream, Dictionary<string, string>) GetFileForm(){ return (null, null); }
    }
}