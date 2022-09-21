
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using AzureStorageQueue;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connectionString = config["ConnectionString"];
var queueName = "az204queue";
var queueClient = new QueueClient(connectionString, queueName);

var order1 = new Order { OrderId = "o1", Quantity = 300 };
var order2 = new Order { OrderId = "o2", Quantity = 400 };
var order3 = new Order { OrderId = "o3", Quantity = 300 };
var order4 = new Order { OrderId = "o4", Quantity = 400 };

string ConvertOrderToBase64(Order order)
{
    var jsonObj = JsonConvert.SerializeObject(order);
    var bytes = Encoding.UTF8.GetBytes(jsonObj);
    var message = System.Convert.ToBase64String(bytes);
    return message;
}

if (queueClient.Exists())
{
    //queueClient.SendMessage("Test Message 6");
    //queueClient.SendMessage("Test Message 7");
    var order1B64 = ConvertOrderToBase64(order1);
    var order2B64 = ConvertOrderToBase64(order2);
    var order3B64 = ConvertOrderToBase64(order3);
    var order4B64 = ConvertOrderToBase64(order4);
    queueClient.SendMessage(order1B64);
    queueClient.SendMessage(order2B64);
    queueClient.SendMessage(order3B64);
    queueClient.SendMessage(order4B64);
    Console.WriteLine("Orders sent");

    //PeekedMessage message =  queueClient.PeekMessage();
    //var orderFromMessage = JsonConvert.DeserializeObject<Order>(message.Body.ToString());
    //Console.WriteLine(orderFromMessage.OrderId);
    //Console.WriteLine(orderFromMessage.Quantity);
    //Console.ReadLine();
    //QueueMessage queueMessage = queueClient.ReceiveMessage();
    //Console.WriteLine(queueMessage.Body);

    //queueClient.DeleteMessage(queueMessage.MessageId, queueMessage.PopReceipt);

    //QueueProperties properties = queueClient.GetProperties();
    //Console.WriteLine(properties.ApproximateMessagesCount);

    

}
else
{
    Console.WriteLine("queue doesnt exist");
}
