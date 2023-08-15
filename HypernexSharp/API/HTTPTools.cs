using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HypernexSharp.API
{
    internal class HTTPTools
    {
        private static HttpClient _client = new HttpClient{Timeout = new TimeSpan(1, 0, 0)};
        
        internal static async Task<string> POST(string url, string data)
        {
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(url, stringContent);
            return await response.Content.ReadAsStringAsync();
        }

        internal static async Task<string> POSTFile(string url, Dictionary<string, string> collection, FileStream file)
        {
            MemoryStream ms = new MemoryStream();
            await file.CopyToAsync(ms);
            MultipartFormDataContent multipartContent = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(ms.ToArray());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            multipartContent.Add(fileContent, "file", Path.GetFileName(file.Name));
            foreach (KeyValuePair<string, string> keyValuePair in collection)
                multipartContent.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            HttpResponseMessage response = await _client.PostAsync(url, multipartContent);
            return await response.Content.ReadAsStringAsync();
        }

        internal static async Task<string> POSTFile(string url, Dictionary<string, string> collection, string fileLocation)
        {
            using (FileStream stream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
            {
                return await POSTFile(url, collection, stream);
            }
        }

        internal static async Task<string> GET(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        
        internal static async Task<Stream> GETFile(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            return await response.Content.ReadAsStreamAsync();
        }
        
        internal static async Task<(string, Stream)> GETFileAndName(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            return (response.Content.Headers.ContentDisposition.FileName.Replace("\"", ""), await response.Content.ReadAsStreamAsync());
        }
        
        internal static async Task<Stream> POSTGetFile(string url, string data)
        {
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(url, stringContent);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}