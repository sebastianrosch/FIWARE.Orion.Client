using FIWARE.Orion.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client
{
    /// <summary>
    /// The client that connects to the Orion instance and updates context or executes queries
    /// </summary>
    public class OrionClient
    {
        private OrionConfig _config;

        /// <summary>
        /// Creates a new instance of the Orion client with default configuration (connecting to the default Orion instance)
        /// </summary>
        public OrionClient()
        {
            _config = new OrionConfig();
        }

        /// <summary>
        /// Creates a new instance of the Orion client with the specified configuration
        /// </summary>
        /// <param name="config"></param>
        public OrionClient(OrionConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Posts the specified ContextUpdate
        /// </summary>
        /// <param name="contextUpdate">The context update</param>
        /// <returns>The response object</returns>
        public async Task<ContextResponses> UpdateContextAsync(ContextUpdate contextUpdate)
        {
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.UpdateContextPath);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(OrionConfig.AuthHeaderKey, _config.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string body = JsonConvert.SerializeObject(contextUpdate);

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    ContextResponses contextResponses = JsonConvert.DeserializeObject<ContextResponses>(content);

                    if (contextResponses.contextResponses == null)
                        contextResponses.contextResponses = new List<ContextResponse>();

                    return contextResponses;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Subscribes the specified URLs for context changes
        /// </summary>
        /// <param name="contextSubscription">The context subscription</param>
        /// <returns>The response object</returns>
        public async Task<ContextSubscriptionResponse> SubscribeAsync(ContextSubscription contextSubscription)
        {
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.SubscribeContextPath);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(OrionConfig.AuthHeaderKey, _config.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string body = JsonConvert.SerializeObject(contextSubscription);

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    ContextSubscriptionResponse contextSubscriptionResponse = JsonConvert.DeserializeObject<ContextSubscriptionResponse>(content);

                    return contextSubscriptionResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Queries the Context Broker for the specified query
        /// </summary>
        /// <param name="contextQuery">The context query</param>
        /// <returns>The response object</returns>
        public async Task<ContextResponses> QueryAsync(ContextQuery contextQuery)
        {
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.QueryContextPath);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(OrionConfig.AuthHeaderKey, _config.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string body = JsonConvert.SerializeObject(contextQuery);

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    ContextResponses contextResponses = JsonConvert.DeserializeObject<ContextResponses>(content);

                    if (contextResponses.contextResponses == null)
                        contextResponses.contextResponses = new List<ContextResponse>();

                    return contextResponses;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Gets the current version of the Orion Context Broker
        /// </summary>
        /// <returns>The version object</returns>
        public async Task<OrionVersion> GetOrionVersionAsync()
        {
            string uri = string.Format(OrionConfig.VersionUrlFormat, _config.BaseUrl, OrionConfig.VersionPath);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(OrionConfig.AuthHeaderKey, _config.Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    OrionVersion orionVersion = JsonConvert.DeserializeObject<OrionVersion>(content);

                    return orionVersion;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// The configuration for the Orion Client
        /// </summary>
        public class OrionConfig
        {
            private string _baseUrl = "http://orion.lab.fi-ware.org:1026/";
            public const string UrlFormat = "{0}/{1}/{2}";
            public const string VersionUrlFormat = "{0}/{1}";
            public string Version1Path = "v1";

            public const string VersionPath = "version";
            public const string PublishPath = "publish";
            public const string UpdateContextPath = "updateContext";
            public const string QueryContextPath = "queryContext";
            public const string ContextEntitiesPath = "contextEntities";
            public const string SubscribeContextPath = "subscribeContext";
            public const string AuthHeaderKey = "X-Auth-Token";

            /// <summary>
            /// The Access Token for the Orion Context Broker API. Get yours at https://forge.fiware.org/plugins/mediawiki/wiki/fiware/index.php/Publish/Subscribe_Broker_-_Orion_Context_Broker_-_Quick_Start_for_Programmers
            /// </summary>
            public string Token { get; set; }

            /// <summary>
            /// The base URL of the Orion instance. Overwrite this with your own URI. By default, uses the Orion Global instance at orion.lab.fi-ware.org.
            /// </summary>
            public string BaseUrl
            {
                get
                {
                    return _baseUrl;
                }
                set
                {
                    _baseUrl = value;
                }
            }

        }
    }
}
