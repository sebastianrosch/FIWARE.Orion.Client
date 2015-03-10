using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    public class StatusCode
    {
        public string code { get; set; }
        public string reasonPhrase { get; set; }
    }
    public class ErrorCode
    {
        public string code { get; set; }
        public string reasonPhrase { get; set; }
        public string details { get; set; }
    }

    public class ContextResponse
    {
        public ContextElement contextElement { get; set; }
        public StatusCode statusCode { get; set; }
    }

    public class ContextResponses
    {
        public string subscriptionId { get; set; }
        public string originator { get; set; }
        public List<ContextResponse> contextResponses { get; set; }
        public ErrorCode errorCode { get; set; }
    }
}
