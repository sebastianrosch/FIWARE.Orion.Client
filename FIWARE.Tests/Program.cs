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
            };

            OrionClient client = new OrionClient(config);

            OrionVersion version = client.GetOrionVersionAsync().Result;
            Debug.WriteLine(version.Orion.Version);

            ContextUpdate create = new ContextUpdate()
            {
                UpdateAction = UpdateActionTypes.APPEND,
                ContextElements = new List<ContextElement>(){
                    new ContextElement(){
                        Type = "User",
                        IsPattern = false,
                        Id = "76afe5ed-a2b1-49f8-ba53-92eef732d265",
                        Attributes = new List<Orion.Client.Models.ContextAttribute>(){
                            new Orion.Client.Models.ContextAttribute(){
                                Name = "userlocation",
                                Type = "string",
                                Value = "23",
                            }
                        }
                    },
                }
            };

            ContextResponses createResponses = client.UpdateContextAsync(create).Result;
            Debug.WriteLine(createResponses.Responses.First().StatusCode.ReasonPhrase);

            ContextUpdate update = new ContextUpdate()
            {
                UpdateAction = UpdateActionTypes.UPDATE,
                ContextElements = new List<ContextElement>(){
                    new ContextElement(){
                        Type = "Room",
                        IsPattern = false,
                        Id = "Room-sebastian-123",
                        Attributes = new List<Orion.Client.Models.ContextAttribute>(){
                            new Orion.Client.Models.ContextAttribute(){
                                Name = "temperature",
                                Type = "float",
                                Value = "230",
                                Metadata = new List<ContextAttributeMetadata>(){
                                    new ContextAttributeMetadata(){
                                        Name = "AcquisitionKey",
                                        Type = "string",
                                        Value = "user.app.iphone"
                                    }
                                }
                            }
                        }
                    },
                }
            };

            ContextResponses updateResponses = client.UpdateContextAsync(update).Result;
            Debug.WriteLine(updateResponses.Responses.First().StatusCode.ReasonPhrase);

            ContextQuery query = new ContextQuery()
            {
                Entities = new List<ContextQueryEntity>(){
                    new ContextQueryEntity(){
                        Type = "Room",
                        IsPattern = true,
                        Id = "Room.*",
                    },
                },
            };

            ContextResponses queryResponses = client.QueryAsync(query).Result;
            foreach (var item in queryResponses.Responses)
            {
                Debug.WriteLine(item.ContextElement.Id);
            }

            ContextQuery query2 = new ContextQuery()
            {
                Entities = new List<ContextQueryEntity>(){
                    new ContextQueryEntity(){
                        Type = "Room",
                        IsPattern = true,
                        Id = "Room.*",
                    },
                },
                Attributes = new List<string>()
                {
                    "temperature",
                }
            };

            ContextResponses queryResponses2 = client.QueryAsync(query2).Result;
            foreach (var item in queryResponses2.Responses)
            {
                Debug.WriteLine(item.ContextElement.Id);
            }

            ContextSubscription subscription = new ContextSubscription()
            {
                Entities = new List<ContextQueryEntity>()
                {
                    new ContextQueryEntity(){
                        Type = "Room",
                        IsPattern = true,
                        Id = "Room.*"
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
            foreach (var entity in allEntities.Responses)
            {
                Debug.WriteLine(entity.ContextElement.Id);
            }

            ContextResponse car1 = client.GetEntityAsync("Car1").Result;
            Debug.WriteLine(car1.ContextElement.Id);

            ContextTypesResponse types = client.GetTypesAsync().Result;
            foreach (var type in types.Types)
            {
                Debug.WriteLine(type.Name);
            }

            ContextAttributesResponse attributes = client.GetAttributesForTypeAsync("User").Result;
            foreach (var attribute in attributes.attributes)
            {
                Debug.WriteLine(attribute.Name);
            }

            ContextUnsubscribeResponse unsubscribe = client.UnsubscribeAsync(subscriptionResponse.SubscribeResponse.SubscriptionId).Result;
            Debug.WriteLine(unsubscribe.SubscriptionId);
        }
    }
}
