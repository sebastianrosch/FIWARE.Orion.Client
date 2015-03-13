using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    /// <summary>
    /// Represents the status code of the response
    /// </summary>
    public class StatusCode
    {
        /// <summary>
        /// The status code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// A textual explanation of the status
        /// </summary>
        [JsonProperty("reasonPhrase")]
        public string ReasonPhrase { get; set; }
    }

    /// <summary>
    /// Represents the error code of the response
    /// </summary>
    public class ErrorCode
    {
        /// <summary>
        /// The error code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// A textual representation of the error code
        /// </summary>
        [JsonProperty("reasonPhrase")]
        public string ReasonPhrase { get; set; }

        /// <summary>
        /// A detailed description of the error
        /// </summary>
        [JsonProperty("details")]
        public string Details { get; set; }
    }

    /// <summary>
    /// Represents a response of a context action
    /// </summary>
    public class ContextResponse
    {
        /// <summary>
        /// The context element this response applies to
        /// </summary>
        [JsonProperty("contextElement")]
        public ContextElement ContextElement { get; set; }

        /// <summary>
        /// The status code of this response
        /// </summary>
        [JsonProperty("statusCode")]
        public StatusCode StatusCode { get; set; }
    }

    /// <summary>
    /// The wrapper for context responses
    /// </summary>
    public class ContextResponses
    {
        /// <summary>
        /// The subscription id of the corresponding subscription, if this response comes from a subscription
        /// </summary>
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The originating url of a context update that triggered this subscription, if this response comes from a subscription
        /// </summary>
        [JsonProperty("originator")]
        public string Originator { get; set; }

        /// <summary>
        /// The list of individual responses
        /// </summary>
        [JsonProperty("contextResponses")]
        public List<ContextResponse> Responses { get; set; }

        /// <summary>
        /// The error code
        /// </summary>
        [JsonProperty("errorCode")]
        public ErrorCode ErrorCode { get; set; }
    }
}
