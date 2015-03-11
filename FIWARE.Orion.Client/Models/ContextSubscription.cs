using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Orion.Client.Models
{
    public class NotifyCondition
    {
        public string type { get; set; }
        public List<string> condValues { get; set; }
    }

    public class ContextSubscription
    {
        public string subscriptionId { get; set; }
        public List<ContextEntity> entities { get; set; }
        public List<string> attributes { get; set; }
        public string reference { get; set; }
        public string duration { get; set; }
        public List<NotifyCondition> notifyConditions { get; set; }
        public string throttling { get; set; }
    }

    public class SubscribeResponse
    {
        public string duration { get; set; }
        public string subscriptionId { get; set; }
        public string throttling { get; set; }
    }

    public class ContextSubscriptionResponse
    {
        public SubscribeResponse subscribeResponse { get; set; }
        public ErrorCode errorCode { get; set; }
    }

    public class ContextUnsubscribeResponse
    {
        public string subscriptionId { get; set; }
        public StatusCode statusCode { get; set; }
    }
}
