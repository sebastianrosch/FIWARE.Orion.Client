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
        [JsonProperty("uptime")]
        public string Uptime { get; set; }
        [JsonProperty("git_hash")]
        public string GitHash { get; set; }
        [JsonProperty("comiple_time")]
        public string CompileTime { get; set; }
        [JsonProperty("compiled_by")]
        public string CompiledBy { get; set; }
        [JsonProperty("compiled_in")]
        public string CompiledIn { get; set; }
    }

    /// <summary>
    /// The Orion version wrapper
    /// </summary>
    public class OrionVersion
    {

        [JsonProperty("orion")]
        public Orion Orion { get; set; }
    }
}
