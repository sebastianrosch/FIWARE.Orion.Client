using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.REST
{
    public class RESTClient<T>
    {
        private string AuthHeaderKey;
        private string AuthToken;

        public RESTClient()
        {

        }

        /// <summary>
        /// Creates a new instance of the RESTClient with authentication information
        /// </summary>
        /// <param name="authHeaderKey"></param>
        /// <param name="authToken"></param>
        public RESTClient(string authHeaderKey, string authToken)
        {
            this.AuthHeaderKey = authHeaderKey;
            this.AuthToken = authToken;
        }

        /// <summary>
        /// Retrieves the date from the provided URI and returns it as an object of type T
        /// </summary>
        /// <param name="uri">The URL to retrieve</param>
        /// <returns></returns>
        public async Task<T> GetAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Posts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to post to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<T> PostAsync(string uri, string body)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Puts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to put to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<T> PutAsync(string uri, string body)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Deletes the date at the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to delete</param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
