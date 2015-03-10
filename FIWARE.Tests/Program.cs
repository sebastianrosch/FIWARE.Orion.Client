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

            OrionVersion version = client.GetOrionVersion().Result;
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

            ContextResponses createResponses = client.UpdateContext(create).Result;
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

            ContextResponses updateResponses = client.UpdateContext(update).Result;
            Debug.WriteLine(updateResponses.contextResponses.First().statusCode.reasonPhrase);

            ContextQuery query = new ContextQuery()
            {
                entities = new List<ContextQueryElement>(){
                    new ContextQueryElement(){
                        type = "Room",
                        isPattern = true,
                        id = "Room.*",
                    },
                },
            };

            ContextResponses queryResponses = client.Query(query).Result;
            foreach (var item in queryResponses.contextResponses)
            {
                Debug.WriteLine(item.contextElement.id);
            }

            ContextQuery query2 = new ContextQuery()
            {
                entities = new List<ContextQueryElement>(){
                    new ContextQueryElement(){
                        type = "Room",
                        isPattern = true,
                        id = "Room.*",
                    },
                },
                attributes = new List<string>()
                {
                    "temp",
                }
            };

            ContextResponses queryResponses2 = client.Query(query2).Result;
            foreach (var item in queryResponses2.contextResponses)
            {
                Debug.WriteLine(item.contextElement.id);
            }



        }
    }
}
