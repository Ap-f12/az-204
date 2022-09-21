using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueFunction
{
    public class QueueTrigger
    {
        
        [FunctionName("QueueTrigger")]
        [return: Table("Orders", Connection ="connectionString")]
        public TableOrder Run([QueueTrigger("az204queue", Connection = "connectionString")]Order order, ILogger log)
        {
            TableOrder tableOrder = new TableOrder()
            {
                PartitionKey = order.OrderId,
                RowKey = order.Quantity.ToString()
            };

            log.LogInformation(" Queue trigger function processed");
            return tableOrder;
        }
    }
}
