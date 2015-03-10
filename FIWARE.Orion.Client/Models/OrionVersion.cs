using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    public class Orion
    {
        public string version { get; set; }
        public string uptime { get; set; }
        public string git_hash { get; set; }
        public string compile_time { get; set; }
        public string compiled_by { get; set; }
        public string compiled_in { get; set; }
    }

    public class OrionVersion
    {
        public Orion orion { get; set; }
    }
}
