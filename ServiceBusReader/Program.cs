using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using SharedModels.Models;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceBusReader
{
    class Program
    {
        private static IQueueClient _queueClient;
        private const string QueueName = "weatherqueue";

        static async Task Main(string[] args)
        {
            _queueClient = new QueueClient("Endpoint=sb://weather-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=4RHOxOxLsfQL8VW4G98dEUPyyv0cmupjFmUKVyhR6FI=",
                QueueName);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            Console.ReadLine();

            await _queueClient.CloseAsync();
        }

        private static async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var json = Encoding.UTF8.GetString(message.Body);
            var weathers = JsonSerializer.Deserialize<List<Weather>>(json);

            if (weathers is {Count: > 0})
            {
                Console.WriteLine("Location: WFiIS AGH Cracow");
                Console.WriteLine($"Time (UTC): {weathers[0].Utc}");
                Console.WriteLine($"Temperature (C): {weathers[0].Data.Ta}");
                Console.WriteLine($"Dew point (C): {weathers[0].Data.T0}");
                Console.WriteLine($"Pressure (HPa): {weathers[0].Data.P0}");
                Console.WriteLine($"Humidity (%): {weathers[0].Data.Ha}");
                Console.WriteLine($"Precipitation in the last hour: {weathers[0].Data.R1}");
                Console.WriteLine($"Precipitation: {weathers[0].Data.Ra}");
                Console.WriteLine($"Wind direction: {weathers[0].Data.Wd}");
                Console.WriteLine($"Wind speed: {weathers[0].Data.Ws}");
                Console.WriteLine($"Current wind speed: {weathers[0].Data.Wg}");
                Console.WriteLine($"Height above sea level: {weathers[0].Data.H0}");
                Console.WriteLine("==============================================");
                Console.WriteLine();
            }

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler exception: {arg.Exception}");
            return Task.CompletedTask;
        }

        public static string GetAppSettingsJsonValue(string appSettingsJsonValue)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();
            return configuration[appSettingsJsonValue];
        }
    }
}
