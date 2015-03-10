using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    public class ContextAttribute
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class ContextElement
    {
        public string type { get; set; }
        public bool isPattern { get; set; }
        public string id { get; set; }
        public List<ContextAttribute> attributes { get; set; }
    }

    public class ContextUpdate
    {
        public List<ContextElement> contextElements { get; set; }
        public string updateAction { get; set; }
    }
}
