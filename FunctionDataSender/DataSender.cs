using System;
using System.Globalization;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace FunctionDataSender
{
    public static class DataSender
    {
        [FunctionName("DataSender")]
        public static void Run([TimerTrigger("*/1 * * * *")]TimerInfo myTimer, 
            ILogger log,
            [ServiceBus("weatherqueue", Connection = "ServiceBusConnection", EntityType = EntityType.Queue)] out string queueMessage)
        {
            log.LogInformation($"[{DateTime.Now}] Data was sent to Azure Service Bus");
            queueMessage = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        }
    }
}
