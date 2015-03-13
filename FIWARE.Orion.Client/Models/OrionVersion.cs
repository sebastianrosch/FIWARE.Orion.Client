using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    /// <summary>
    /// Represents the current Orion version
    /// </summary>
    public class Orion
    {
        /// <summary>
        /// The version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// The uptime of the context broker
        /// </summary>
        [JsonProperty("uptime")]
        public string Uptime { get; set; }

        /// <summary>
        /// The git hash of this build
        /// </summary>
        [JsonProperty("git_hash")]
        public string GitHash { get; set; }

        /// <summary>
        /// The date and time this version was compiled
        /// </summary>
        [JsonProperty("comiple_time")]
        public string CompileTime { get; set; }

        /// <summary>
        /// The name of the person that compiled this build
        /// </summary>
        [JsonProperty("compiled_by")]
        public string CompiledBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("compiled_in")]
        public string CompiledIn { get; set; }
    }

    /// <summary>
    /// The Orion version wrapper
    /// </summary>
    public class OrionVersion
    {
        /// <summary>
        /// The Orion version
        /// </summary>
        [JsonProperty("orion")]
        public Orion Orion { get; set; }
    }
}
