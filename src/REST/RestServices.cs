using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kalakoi.Xbox.OpenXBL
{
    /// <summary>
    /// Provides easy access to GET and POST functionality for REST services.
    /// </summary>
    internal static class RestServices
    {
        /// <summary>
        /// Sends GET request to REST service and returns response asynchronously.
        /// </summary>
        /// <param name="uri">Address to send request to.</param>
        /// <param name="APIKey">API key to use with the request.</param>
        /// <returns>JSON data as string.</returns>
        internal static async Task<string> GetResponseAsync(Uri uri, string APIKey)
        {
            //Create client to send GET request
            using (HttpClient client = new HttpClient())
            {
                //Add request headers
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome / 58.0.3029.110 Safari / 537.36");
                client.DefaultRequestHeaders.Add("X-Authorization", APIKey);
                //Send request and await response
                string response = await client.GetStringAsync(uri).ConfigureAwait(false);
                return response;
            }
        }

        /// <summary>
        /// Sends GET request to REST service and returns response asynchronously.
        /// </summary>
        /// <param name="uri">Address to send request to.</param>
        /// <returns>JSON data as string.</returns>
        internal static async Task<string> GetResponseAsync(Uri uri)
        {
            //Create client to send GET request
            using (HttpClient client = new HttpClient())
            {
                //Add request headers
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome / 58.0.3029.110 Safari / 537.36");
                //Send request and await response
                string response = await client.GetStringAsync(uri).ConfigureAwait(false);
                return response;
            }
        }

        /// <summary>
        /// Sends GET request to REST service and returns response.
        /// </summary>
        /// <param name="uri">Address to send request to.</param>
        /// <param name="APIKey">API key to use with the request.</param>
        /// <returns>JSON data as string.</returns>
        internal static string GetResponse(Uri uri, string APIKey)
        {
            //Create client to send GET request
            using (WebClient client = new WebClient())
            {
                //Add request headers
                client.Headers.Add(string.Format("X-Authorization: {0}", APIKey));
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                client.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US");
                //Send request and await response
                using (Stream ReqStream = client.OpenRead(uri))
                using (StreamReader Reader = new StreamReader(ReqStream))
                {
                    string response = Reader.ReadToEnd();
                    return response;
                }
            }
        }

        /// <summary>
        /// Sends GET request to REST service and returns response.
        /// </summary>
        /// <param name="uri">Address to send request to.</param>
        /// <returns>JSON data as string.</returns>
        internal static string GetResponse(Uri uri)
        {
            //Create client to send GET request
            using (WebClient client = new WebClient())
            {
                //Add request headers
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                client.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US");
                //Send request and await response
                using (Stream ReqStream = client.OpenRead(uri))
                using (StreamReader Reader = new StreamReader(ReqStream))
                {
                    string response = Reader.ReadToEnd();
                    return response;
                }
            }
        }

        /// <summary>
        /// Sends POST request to REST service with JSON data and returns response asynchronously.
        /// </summary>
        /// <param name="uri">Web address to send request.</param>
        /// <param name="Data">JSON data to send with request.</param>
        /// <param name="APIKey">API key to use with the request.</param>
        /// <returns>JSON data as string.</returns>
        internal static async Task<string> GetPostResponseAsync(Uri uri, string Data, string APIKey)
        {
            //Create Client to send and receive data from REST service
            using (HttpClient client = new HttpClient())
            {
                //Add headers for request
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome / 58.0.3029.110 Safari / 537.36");
                client.DefaultRequestHeaders.Add("X-Authorization", APIKey);
                client.DefaultRequestHeaders.Host = uri.Host;
                //Create content to send from JSON string
                HttpContent content = new StringContent(Data, Encoding.UTF8, @"application/json");
                //Send POST request and await response
                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                //Return response JSON as string
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Sends POST request to REST service with JSON data and returns response.
        /// </summary>
        /// <param name="uri">Web address to send request.</param>
        /// <param name="Data">JSON data to send with request.</param>
        /// <param name="APIKey">API key to use with the request.</param>
        /// <returns>JSON data as string.</returns>
        internal static string GetPostResponse(Uri uri, string Data, string APIKey)
        {
            //Create client to send POST request
            using (WebClient client = new WebClient())
            {
                //Add request headers
                client.Headers.Add(string.Format("X-Authorization: {0}", APIKey));
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                client.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US");
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                //Send POST request and await response
                string response = client.UploadString(uri, Data);
                return response;
            }
        }
    }
}
