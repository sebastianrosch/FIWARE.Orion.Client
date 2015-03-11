using FIWARE.Orion.Client.Models;
using FIWARE.Orion.Client.REST;
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
        private JsonSerializerSettings jsonSettings;

        /// <summary>
        /// Creates a new instance of the Orion client with default configuration (connecting to the default Orion instance)
        /// </summary>
        public OrionClient()
        {
            _config = new OrionConfig();
            jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        /// <summary>
        /// Creates a new instance of the Orion client with the specified configuration
        /// </summary>
        /// <param name="config"></param>
        public OrionClient(OrionConfig config)
        {
            _config = config;
            jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        /// <summary>
        /// Posts the specified ContextUpdate
        /// </summary>
        /// <param name="contextUpdate">The context update</param>
        /// <returns>The response object</returns>
        public async Task<ContextResponses> UpdateContextAsync(ContextUpdate contextUpdate)
        {
            RESTClient<ContextResponses> client = new RESTClient<ContextResponses>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.UpdateContextPath);
            string body = JsonConvert.SerializeObject(contextUpdate, jsonSettings);

            ContextResponses contextResponses = await client.PostAsync(uri, body);

            if (contextResponses.contextResponses == null)
                contextResponses.contextResponses = new List<ContextResponse>();

            return contextResponses;
        }

        /// <summary>
        /// Queries the Context Broker for the specified query
        /// </summary>
        /// <param name="contextQuery">The context query</param>
        /// <returns>The response object</returns>
        public async Task<ContextResponses> QueryAsync(ContextQuery contextQuery)
        {
            RESTClient<ContextResponses> client = new RESTClient<ContextResponses>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.QueryContextPath);
            string body = JsonConvert.SerializeObject(contextQuery, jsonSettings);

            ContextResponses contextResponses = await client.PostAsync(uri, body);

            if (contextResponses.contextResponses == null)
                contextResponses.contextResponses = new List<ContextResponse>();

            return contextResponses;
        }

        /// <summary>
        /// Gets the current version of the Orion Context Broker
        /// </summary>
        /// <returns>The version object</returns>
        public async Task<OrionVersion> GetOrionVersionAsync()
        {
            RESTClient<OrionVersion> client = new RESTClient<OrionVersion>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.VersionUrlFormat, _config.BaseUrl, OrionConfig.VersionPath);
            return await client.GetAsync(uri);
        }

        #region Types

        /// <summary>
        /// Gets all types currently in the Orion Context Broker
        /// </summary>
        /// <returns>The response object</returns>
        public async Task<ContextTypesResponse> GetTypesAsync()
        {
            RESTClient<ContextTypesResponse> client = new RESTClient<ContextTypesResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.ContextTypesPath);
            return await client.GetAsync(uri);
        }

        /// <summary>
        /// Gets all types currently in the Orion Context Broker
        /// </summary>
        /// <returns>The response object</returns>
        public async Task<ContextAttributesResponse> GetAttributesForTypeAsync(string type)
        {
            RESTClient<ContextAttributesResponse> client = new RESTClient<ContextAttributesResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.ConvenienceUrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.ContextTypesPath, type);
            return await client.GetAsync(uri);
        }

        #endregion

        #region Subscriptions

        /// <summary>
        /// Subscribes the specified URLs for context changes
        /// </summary>
        /// <param name="contextSubscription">The context subscription</param>
        /// <returns>The response object</returns>
        public async Task<ContextSubscriptionResponse> SubscribeAsync(ContextSubscription contextSubscription)
        {
            RESTClient<ContextSubscriptionResponse> client = new RESTClient<ContextSubscriptionResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.SubscribeContextPath);
            string body = JsonConvert.SerializeObject(contextSubscription, jsonSettings);

            return await client.PostAsync(uri, body);
        }

        /// <summary>
        /// Updates the specified context subscription. The subscription needs an id.
        /// </summary>
        /// <param name="contextSubscription">The context subscription</param>
        /// <returns>The response object</returns>
        public async Task<ContextSubscriptionResponse> UpdateSubscriptionAsync(ContextSubscription contextSubscription)
        {
            RESTClient<ContextSubscriptionResponse> client = new RESTClient<ContextSubscriptionResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.UpdateSubscriptionContextPath);
            string body = JsonConvert.SerializeObject(contextSubscription, jsonSettings);

            return await client.PostAsync(uri, body);
        }

        /// <summary>
        /// Unsubscribes the subscription with the specified subscription id.
        /// </summary>
        /// <param name="subscriptionId">The context subscription id</param>
        /// <returns>The response object</returns>
        public async Task<ContextUnsubscribeResponse> UnsubscribeAsync(string subscriptionId)
        {
            ContextSubscription contextSubscription = new ContextSubscription() { subscriptionId = subscriptionId };

            RESTClient<ContextUnsubscribeResponse> client = new RESTClient<ContextUnsubscribeResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.UnsubscribeContextPath);
            string body = JsonConvert.SerializeObject(contextSubscription, jsonSettings);

            return await client.PostAsync(uri, body);
        }

        #endregion

        #region Convenience Methods

        /// <summary>
        /// Retrieves all entities
        /// </summary>
        /// <returns>The response object</returns>
        public async Task<ContextResponses> GetAllEntitiesAsync()
        {
            RESTClient<ContextResponses> client = new RESTClient<ContextResponses>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.UrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.ContextEntitiesPath);
            ContextResponses contextResponses = await client.GetAsync(uri);

            if (contextResponses.contextResponses == null)
                contextResponses.contextResponses = new List<ContextResponse>();

            return contextResponses;
        }

        /// <summary>
        /// Retrieves the entity with the specified id
        /// </summary>
        /// <returns>The response object</returns>
        public async Task<ContextResponse> GetEntityAsync(string entityId)
        {
            RESTClient<ContextResponse> client = new RESTClient<ContextResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.ConvenienceUrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.ContextEntitiesPath, entityId);
            return await client.GetAsync(uri);
        }

        /// <summary>
        /// Updates the entity with the specified id
        /// </summary>
        /// <returns>The response object</returns>
        public async Task<ContextResponse> UpdateEntityAsync(string entityId, ContextElement entity)
        {
            RESTClient<ContextResponse> client = new RESTClient<ContextResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.ConvenienceUrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.ContextEntitiesPath, entityId);
            return await client.GetAsync(uri);
        }

        /// <summary>
        /// Deletes the entity with the specified id
        /// </summary>
        /// <returns>The response object</returns>
        public async Task<ContextResponse> DeleteEntityAsync(string entityId)
        {
            RESTClient<ContextResponse> client = new RESTClient<ContextResponse>(OrionConfig.AuthHeaderKey, _config.Token);
            string uri = string.Format(OrionConfig.ConvenienceUrlFormat, _config.BaseUrl, _config.Version1Path, OrionConfig.ContextEntitiesPath, entityId);
            return await client.DeleteAsync(uri);
        }


        #endregion

        /// <summary>
        /// The configuration for the Orion Client
        /// </summary>
        public class OrionConfig
        {
            private string _baseUrl = "http://orion.lab.fi-ware.org:1026/";
            public const string UrlFormat = "{0}/{1}/{2}";
            public const string ConvenienceUrlFormat = "{0}/{1}/{2}/{3}";
            public const string VersionUrlFormat = "{0}/{1}";
            public string Version1Path = "v1";

            public const string VersionPath = "version";
            public const string PublishPath = "publish";
            public const string UpdateContextPath = "updateContext";
            public const string QueryContextPath = "queryContext";
            public const string SubscribeContextPath = "subscribeContext";
            public const string UpdateSubscriptionContextPath = "updateContext";
            public const string UnsubscribeContextPath = "unsubscribeContext";


            public const string ContextEntitiesPath = "contextEntities";
            public const string ContextTypesPath = "contextTypes";


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
