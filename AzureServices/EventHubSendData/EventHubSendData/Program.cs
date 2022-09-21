using Azure.Core.Pipeline;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHubSendData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connectionString = config["ConnectionString"];

var eventHubName = "appeventhub";

var client = new EventHubProducerClient(connectionString, eventHubName);

var batch = await client.CreateBatchAsync();

var deviceList = new List<Device>()
    {
        new Device(){DeviceId ="D1", Temprature= 39.1F},
        new Device(){DeviceId ="D2", Temprature= 39.2F},
        new Device(){DeviceId ="D4", Temprature= 39.3F},
        new Device(){DeviceId ="D3", Temprature= 39.4F},
        new Device(){DeviceId ="D5", Temprature= 39.5F}
    };

foreach(var device in deviceList)
{
    var data = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(device)));
    if (!batch.TryAdd(data))
        Console.WriteLine("Error occured");

    await client.SendAsync(batch);

   
}


await client.DisposeAsync();