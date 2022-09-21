
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var eventHubConnectionString = config["eventHubConnectionString"];
var consumerGroupName = "$Default";
var blobConnectionString = config["blobConnectionString"];
var containerName = "checkpoint";

var blobClient = new BlobContainerClient(blobConnectionString, containerName);
var processorClient = new EventProcessorClient(blobClient, consumerGroupName, eventHubConnectionString);
processorClient.ProcessEventAsync += ProcessEvents;
processorClient.ProcessErrorAsync += ErrorHandler;

processorClient.StartProcessing();
Console.ReadLine();
processorClient.StopProcessing();

async Task ProcessEvents(ProcessEventArgs processEvent)
{
    Console.WriteLine(processEvent.Data.EventBody.ToString());
}

static Task ErrorHandler(ProcessErrorEventArgs errorEvent)
{
    Console.WriteLine(errorEvent.Exception.Message);
    return Task.CompletedTask;
}