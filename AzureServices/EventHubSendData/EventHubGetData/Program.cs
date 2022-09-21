using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connectionString = config["ConnectionString"];

var consumerGroupName = "$Default";


var client = new EventHubConsumerClient(consumerGroupName, connectionString);

var partitionIds = await client.GetPartitionIdsAsync();
foreach(var partitionId in partitionIds)
{
    Console.WriteLine(partitionId);
}

var cancellationTokenSource = new CancellationTokenSource();
cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(300));
await foreach (PartitionEvent partitionEvent in client.ReadEventsAsync(cancellationTokenSource.Token))
{
    Console.WriteLine(partitionEvent.Partition.PartitionId);
    Console.WriteLine(partitionEvent.Data.Offset);
    Console.WriteLine(partitionEvent.Data.EventBody);
}