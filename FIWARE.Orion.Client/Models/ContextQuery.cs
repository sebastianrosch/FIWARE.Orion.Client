using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    /// <summary>
    /// Represents a context entity in a query
    /// </summary>
    public class ContextQueryEntity
    {
        /// <summary>
        /// The type of entities to query
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Whether the entity id is a pattern
        /// </summary>
        [JsonProperty("isPattern")]
        public bool IsPattern { get; set; }

        /// <summary>
        /// The entity id or an id pattern (for example containing .* as a placeholder)
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    /// <summary>
    /// Represents a context query
    /// </summary>
    public class ContextQuery
    {
        /// <summary>
        /// The list of entities to query
        /// </summary>
        [JsonProperty("entities")]
        public List<ContextQueryEntity> Entities { get; set; }

        /// <summary>
        /// The list of attribute names to query
        /// </summary>
        [JsonProperty("attributes")]
        public List<string> Attributes { get; set; }
    }
}
