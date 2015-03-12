using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    /// <summary>
    /// Represents the structure of a context attribute
    /// </summary>
    public class ContextAttribute
    {
        /// <summary>
        /// The attribute name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The type of the attribute value
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The attribute value
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }

        /// <summary>
        /// The list of metadata
        /// </summary>
        [JsonProperty("metadatas")]
        public List<ContextAttributeMetadata> Metadata { get; set; }
    }

    /// <summary>
    /// Represents the metadata of an attribute
    /// </summary>
    public class ContextAttributeMetadata
    {
        /// <summary>
        /// The metadata name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The metadata value type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The metedata value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// Represents an entity in the context broker
    /// </summary>
    public class ContextElement
    {
        /// <summary>
        /// The entity type
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

        /// <summary>
        /// The attributes of this entity
        /// </summary>
        [JsonProperty("attributes")]
        public List<ContextAttribute> Attributes { get; set; }
    }

    /// <summary>
    /// The wrapper object of a context update or registration
    /// </summary>
    public class ContextUpdate
    {
        /// <summary>
        /// The list of entities to update
        /// </summary>
        [JsonProperty("contextElements")]
        public List<ContextElement> ContextElements { get; set; }

        /// <summary>
        /// The action of this context update. Use UpdateActionTypes helper.
        /// </summary>
        [JsonProperty("updateAction")]
        public string UpdateAction { get; set; }
    }
}
