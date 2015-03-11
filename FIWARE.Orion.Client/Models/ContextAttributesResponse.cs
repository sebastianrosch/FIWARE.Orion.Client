using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    public class ContextAttributesResponse
    {
        public string name { get; set; }
        public List<ContextAttribute> attributes { get; set; }
        public StatusCode statusCode { get; set; }
    }
}
