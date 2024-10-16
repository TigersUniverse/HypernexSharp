using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HypernexSharp.API
{
    internal class HTTPTools
    {
        private static Uri GetUri(string url) => new Uri(url);

        internal static async Task<string> POST(string url, string data, Action<int> progress = null)
        {
            using (WebClient w = new WebClient())
            {
                w.Headers[HttpRequestHeader.ContentType] = "application/json";
                w.UploadProgressChanged += (sender, args) => progress?.Invoke(args.ProgressPercentage);
                return await w.UploadStringTaskAsync(GetUri(url), WebRequestMethods.Http.Post, data);
            }
        }

        internal static async Task<string> POSTFile(string url, Dictionary<string, string> collection, FileStream file,
            Action<int> progress = null)
        {
            using (WebClient w = new WebClient())
            {
                // Boundary for the multipart/form-data request
                string boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");
                w.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    byte[] boundaryBytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");

                    // Add form data to the request
                    foreach (var kvp in collection)
                    {
                        memoryStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        string formData = $"Content-Disposition: form-data; name=\"{kvp.Key}\"\r\n\r\n{kvp.Value}\r\n";
                        byte[] formDataBytes = Encoding.UTF8.GetBytes(formData);
                        memoryStream.Write(formDataBytes, 0, formDataBytes.Length);
                    }

                    // Add the file to the request
                    memoryStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    string header = $"Content-Disposition: form-data; name=\"file\"; filename=\"{Path.GetFileName(file.Name)}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    byte[] headerBytes = Encoding.UTF8.GetBytes(header);
                    memoryStream.Write(headerBytes, 0, headerBytes.Length);

                    // Copy the file stream to the memory stream
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = await file.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);
                        progress?.Invoke((int)(memoryStream.Length * 100 / file.Length));
                    }

                    // Write the trailing boundary
                    byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                    memoryStream.Write(trailer, 0, trailer.Length);

                    // Convert the MemoryStream to a byte array
                    byte[] data = memoryStream.ToArray();

                    // Upload the data
                    byte[] response = await w.UploadDataTaskAsync(url, data);
                    return Encoding.UTF8.GetString(response);
                }
            }
        }
        
        internal static async Task<string> POSTFile(string url, Dictionary<string, string> collection, string fileLocation, Action<int> progress = null)
        {
            using (FileStream stream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
            {
                return await POSTFile(url, collection, stream, progress);
            }
        }
        
        internal static async Task<string> GET(string url)
        {
            using (WebClient w = new WebClient())
            {
                return await w.DownloadStringTaskAsync(url);
            }
        }
        
        internal static async Task<Stream> GETFile(string url, Action<int> progress = null)
        {
            using (WebClient w = new WebClient())
            {
                w.DownloadProgressChanged += (sender, args) => progress?.Invoke(args.ProgressPercentage);
                byte[] data = await w.DownloadDataTaskAsync(url);
                return new MemoryStream(data);
            }
        }
        
        internal static async Task<(string, Stream)> GETFileAndName(string url, Action<int> progress = null)
        {
            using (WebClient w = new WebClient())
            {
                TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
                string fileName = null;
                w.DownloadProgressChanged += (sender, args) => progress?.Invoke(args.ProgressPercentage);
                w.DownloadDataCompleted += (sender, args) =>
                {
                    if (args.Error != null)
                    {
                        tcs.SetException(args.Error);
                        return;
                    }
                    if (args.Cancelled)
                    {
                        tcs.SetCanceled();
                        return;
                    }
                    if (w.ResponseHeaders != null)
                    {
                        string contentDisposition = w.ResponseHeaders["Content-Disposition"];
                        if (string.IsNullOrEmpty(contentDisposition)) return;
                        const string fileNameKey = "filename=";
                        int fileNameIndex = contentDisposition.IndexOf(fileNameKey, StringComparison.OrdinalIgnoreCase);
                        if (fileNameIndex >= 0)
                        {
                            fileName = contentDisposition.Substring(fileNameIndex + fileNameKey.Length).Trim('"');
                        }
                    }
                    tcs.SetResult(args.Result);
                };
                w.DownloadDataAsync(GetUri(url));
                byte[] data = await tcs.Task;
                return (fileName ?? throw new Exception("Unknown File Name!"), new MemoryStream(data));
            }
        }
        
        internal static async Task<Stream> POSTGetFile(string url, string data, Action<int> progress = null)
        {
            using (WebClient w = new WebClient())
            {
                w.Headers[HttpRequestHeader.ContentType] = "application/json";
                w.DownloadProgressChanged += (sender, args) => progress?.Invoke(args.ProgressPercentage);
                byte[] fileData =
                    await w.UploadDataTaskAsync(url, WebRequestMethods.Http.Post, Encoding.UTF8.GetBytes(data));
                return new MemoryStream(fileData);
            }
        }
    }
}