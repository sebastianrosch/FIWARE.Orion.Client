using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    /// <summary>
    /// Represents an entity type
    /// </summary>
    public class ContextEntityType
    {
        /// <summary>
        /// The name of the entity type
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The attributes associated with this entity type
        /// </summary>
        [JsonProperty("attributes")]
        public List<ContextAttribute> Attributes { get; set; }
    }

    /// <summary>
    /// The wrapper for the context type response
    /// </summary>
    public class ContextTypesResponse
    {
        /// <summary>
        /// The list of entity types
        /// </summary>
        [JsonProperty("types")]
        public List<ContextEntityType> Types { get; set; }

        /// <summary>
        /// The status code
        /// </summary>
        [JsonProperty("statusCode")]
        public StatusCode StatusCode { get; set; }
    }
}
