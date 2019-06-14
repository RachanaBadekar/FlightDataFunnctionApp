using System;
using System.Net.Http;
using FlightDataFunctionApp;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FlightDataFunnctionApp
{
    public static class FlightDataPoll
    {
        static HttpClient client = new HttpClient();
        [FunctionName("FlightDataPoll")]
        public static async System.Threading.Tasks.Task RunAsync(
            [TimerTrigger("*/10 * * * * *")]TimerInfo myTimer,
            [CosmosDB(
                databaseName: "flightdb",
                collectionName: "flights",
                ConnectionStringSetting = "AzureCosmosDBConnection")]IAsyncCollector<Flight> documents,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var openSkyUrl = "https://opensky-network.org/api/states/all?lamin=25.20&lomin=-124.40&lamax=49.00&lomax=-66.30";

            using (HttpResponseMessage res = await client.GetAsync(openSkyUrl))
            using (HttpContent content = res.Content)
            {
                var result = JsonConvert.DeserializeObject<Rootobject>(await content.ReadAsStringAsync());
                foreach (var item in result.states)
                {
                    await documents.AddAsync(Flight.CreateFromData(item));
                }

                log.LogInformation($"Total flights processed{result.states.Length}");
            }
        }
    }
}
