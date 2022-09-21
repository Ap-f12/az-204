using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceBusQueue;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var connectionString = config["ConnectionString"];
var queueName = "servicebusqueue";

var orders = new List<Order>()
{
    new Order(){OrderId ="01", Quantity =200, UnitPrice=7.99F},
    new Order(){OrderId ="02", Quantity =400, UnitPrice=5.99F},
    new Order(){OrderId ="02", Quantity =300, UnitPrice=3.99F}
};

var client = new ServiceBusClient(connectionString);
//var sender = client.CreateSender(queueName);

//var batch = await sender.CreateMessageBatchAsync();

//foreach(var order in orders)
//{
//    if(!batch.TryAddMessage(
//        new ServiceBusMessage(JsonConvert.SerializeObject(order)))        )
//    {
//        throw new Exception("something went wrong");
//    }
//}
//await sender.SendMessagesAsync(batch);
//Console.WriteLine("batch sent");

ServiceBusReceiver receiver = client.CreateReceiver(queueName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock});
IAsyncEnumerable<ServiceBusReceivedMessage> messages =  receiver.ReceiveMessagesAsync();

await foreach(ServiceBusReceivedMessage message in messages)
{
    Order order = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
    Console.WriteLine(order.OrderId);
    Console.WriteLine(order.Quantity);
    Console.WriteLine(order.UnitPrice);
}

