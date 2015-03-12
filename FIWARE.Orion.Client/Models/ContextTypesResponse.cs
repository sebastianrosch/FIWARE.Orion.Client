using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    public class Type
    {
        public string name { get; set; }
        public List<ContextAttribute> attributes { get; set; }
    }

    public class ContextTypesResponse
    {
        public List<Type> types { get; set; }
        public StatusCode statusCode { get; set; }
    }
}
