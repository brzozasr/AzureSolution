using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace FunctionDataSender
{
    public static class DataSender
    {
        private static readonly HttpClient Client = new HttpClient();

        [FunctionName("DataSender")]
        public static async Task Run([TimerTrigger("*/1 * * * *")]TimerInfo myTimer, 
            ILogger log,
            [ServiceBus("weatherqueue", Connection = "ServiceBusConnection", EntityType = EntityType.Queue)] IAsyncCollector<string> queueMessage)
        {
            log.LogInformation("Data was sent to Azure Service Bus");

            using (var response = await Client.GetAsync(
                "http://mech.fis.agh.edu.pl/meteo/rest/json/last/s000"))
            {
                var content = await response.Content.ReadAsStringAsync();
                await queueMessage.AddAsync(content);
            }
        }
    }
}
