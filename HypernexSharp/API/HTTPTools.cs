using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HypernexSharp.API
{
    internal class HTTPTools
    {
        private static HttpClient _client = new HttpClient();
        
        internal static async Task<string> POST(string url, string data)
        {
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(url, stringContent);
            return await response.Content.ReadAsStringAsync();
        }

        internal static async Task<string> POSTFile(string url, Dictionary<string, string> collection, Stream file)
        {
            MultipartContent multipartContent = new MultipartFormDataContent();
            foreach (KeyValuePair<string, string> keyValuePair in collection)
                ((MultipartFormDataContent) multipartContent).Add(new StringContent(keyValuePair.Value),
                    keyValuePair.Key);
            ((MultipartFormDataContent) multipartContent).Add(new StreamContent(file), "file");
            HttpResponseMessage response = await _client.PostAsync(url, multipartContent);
            return await response.Content.ReadAsStringAsync();
        }

        internal static async Task<string> POSTFile(string url, Dictionary<string, string> collection, string fileLocation)
        {
            using (FileStream stream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
            {
                byte[] result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int) stream.Length);
                return await POSTFile(url, collection, new MemoryStream(result));
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
    }
}