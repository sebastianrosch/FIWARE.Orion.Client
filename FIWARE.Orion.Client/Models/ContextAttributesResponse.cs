using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    /// <summary>
    /// Represents an attribute response
    /// </summary>
    public class ContextAttributesResponse
    {
        /// <summary>
        /// The name of the entity type that has the attributes
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// The list of attributes of this entity type
        /// </summary>
        [JsonProperty("attributes")]
        public List<ContextAttribute> attributes { get; set; }

        /// <summary>
        /// The status code
        /// </summary>
        [JsonProperty("statuscode")]
        public StatusCode statusCode { get; set; }
    }
}
