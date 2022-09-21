using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusFunction
{
    public class ServiceBusFunction
    {
        [FunctionName("ServiceBusFunction")]
        public void Run([ServiceBusTrigger("servicebusqueue", Connection = "ConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
