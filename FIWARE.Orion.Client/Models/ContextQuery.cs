using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{

    public class ContextQueryElement
    {
        public string type { get; set; }
        public bool isPattern { get; set; }
        public string id { get; set; }
    }

    public class ContextQuery
    {
        public List<ContextQueryElement> entities { get; set; }
        public List<string> attributes { get; set; }
    }
}
