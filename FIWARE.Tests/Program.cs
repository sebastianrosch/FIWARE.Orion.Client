using FIWARE.Orion.Client;
using FIWARE.Orion.Client.Models;
using FIWARE.Orion.Client.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = ConfigurationSettings.AppSettings["Token"];

            OrionClient.OrionConfig config = new OrionClient.OrionConfig()
            {
                Token = token,
                BaseUrl = "http://160.85.2.21:1026"
            };

            OrionClient client = new OrionClient(config);

            OrionVersion version = client.GetOrionVersionAsync().Result;
            Debug.WriteLine(version.orion.version);

            ContextUpdate create = new ContextUpdate()
            {
                updateAction = UpdateActionTypes.APPEND,
                contextElements = new List<ContextElement>(){
                    new ContextElement(){
                        type = "Room",
                        isPattern = false,
                        id = "Room-sebastian-123",
                        attributes = new List<Orion.Client.Models.ContextAttribute>(){
                            new Orion.Client.Models.ContextAttribute(){
                                name = "temperature",
                                type = "float",
                                value = "23",
                            }
                        }
                    },
                }
            };

            ContextResponses createResponses = client.UpdateContextAsync(create).Result;
            Debug.WriteLine(createResponses.contextResponses.First().statusCode.reasonPhrase);

            ContextUpdate update = new ContextUpdate()
            {
                updateAction = UpdateActionTypes.UPDATE,
                contextElements = new List<ContextElement>(){
                    new ContextElement(){
                        type = "Room",
                        isPattern = false,
                        id = "Room-sebastian-123",
                        attributes = new List<Orion.Client.Models.ContextAttribute>(){
                            new Orion.Client.Models.ContextAttribute(){
                                name = "temperature",
                                type = "float",
                                value = "230",
                            }
                        }
                    },
                }
            };

            ContextResponses updateResponses = client.UpdateContextAsync(update).Result;
            Debug.WriteLine(updateResponses.contextResponses.First().statusCode.reasonPhrase);

            ContextQuery query = new ContextQuery()
            {
                entities = new List<ContextEntity>(){
                    new ContextEntity(){
                        type = "Room",
                        isPattern = true,
                        id = "Room.*",
                    },
                },
            };

            ContextResponses queryResponses = client.QueryAsync(query).Result;
            foreach (var item in queryResponses.contextResponses)
            {
                Debug.WriteLine(item.contextElement.id);
            }

            ContextQuery query2 = new ContextQuery()
            {
                entities = new List<ContextEntity>(){
                    new ContextEntity(){
                        type = "Room",
                        isPattern = true,
                        id = "Room.*",
                    },
                },
                attributes = new List<string>()
                {
                    "temperature",
                }
            };

            ContextResponses queryResponses2 = client.QueryAsync(query2).Result;
            foreach (var item in queryResponses2.contextResponses)
            {
                Debug.WriteLine(item.contextElement.id);
            }

            ContextSubscription subscription = new ContextSubscription()
            {
                Entities = new List<ContextEntity>()
                {
                    new ContextEntity(){
                        type = "Room",
                        isPattern = true,
                        id = "Room.*"
                    },
                },
                Attributes = new List<string>() { 
                    "temperature"
                },
                Reference = "http://smarthome.cloudapp.net/api/UserContext/Broker",
                Duration = SubscriptionDurations.OneMonth,
                NotifyConditions = new List<NotifyCondition>()
                {
                    new NotifyCondition(){
                        Type = NotifyConditionTypes.ONCHANGE,
                        ConditionValues = new List<string>(){ "temperature"}
                    }
                },
                Throttling = SubscriptionThrottlingTypes.PT5S
            };

            ContextSubscriptionResponse subscriptionResponse = client.SubscribeAsync(subscription).Result;
            Debug.WriteLine(subscriptionResponse.SubscribeResponse.SubscriptionId);

            ContextResponses allEntities = client.GetAllEntitiesAsync().Result;
            foreach (var entity in allEntities.contextResponses)
            {
                Debug.WriteLine(entity.contextElement.id);
            }

            ContextResponse car1 = client.GetEntityAsync("Car1").Result;
            Debug.WriteLine(car1.contextElement.id);

            ContextTypesResponse types = client.GetTypesAsync().Result;
            foreach (var type in types.types)
            {
                Debug.WriteLine(type.name);
            }

            ContextAttributesResponse attributes = client.GetAttributesForTypeAsync("User").Result;
            foreach (var attribute in attributes.attributes)
            {
                Debug.WriteLine(attribute.name);
            }

            ContextUnsubscribeResponse unsubscribe = client.UnsubscribeAsync(subscriptionResponse.SubscribeResponse.SubscriptionId).Result;
            Debug.WriteLine(unsubscribe.SubscriptionId);
        }
    }
}
