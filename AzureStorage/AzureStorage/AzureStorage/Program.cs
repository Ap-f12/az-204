// See https://aka.ms/new-console-template for more information


using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connectionString = config["ConnectionStrings:DefaultConnection"];
var containerName = "containerfromconsoleapp";
var blobName = "testUpload.html";
var blobPath = "C:\\Users\\F12\\source\\repos\\az-204\\AzureStorage\\testupload.html";
Console.WriteLine(blobPath);
Console.ReadLine();
var downloadPath = "./";

var blobServiceClient = new BlobServiceClient(connectionString);
await blobServiceClient.CreateBlobContainerAsync(containerName);
Console.WriteLine("container created");
Console.ReadLine();

var blobContainerClient = new BlobContainerClient(connectionString, containerName);
var blobClient = blobContainerClient.GetBlobClient(blobName);
await blobClient.UploadAsync(blobPath, true);
Console.WriteLine("blob uploaded");
Console.ReadLine();

await foreach(var item in blobContainerClient.GetBlobsAsync())
{
    var contentLength = item.Properties.ContentLength;
    Console.WriteLine("Content length of the blob is {0}", contentLength);
    
}
Console.ReadLine();


await blobClient.DownloadToAsync(blobPath);
Console.WriteLine("blob downloaded");
Console.ReadLine();

var metaData = new Dictionary<string, string>();
metaData.Add("testKey", "testValue");

await blobClient.SetMetadataAsync(metaData);
Console.WriteLine("metadata set");
Console.ReadLine();

BlobProperties blobProperties = await blobClient.GetPropertiesAsync();

foreach( var md in blobProperties.Metadata)
{
    Console.WriteLine("The metadata key and values is {0} and {1}", md.Key, md.Value);
}
Console.ReadLine();

BlobLeaseClient blobLeaseClient = blobClient.GetBlobLeaseClient();
var timeSpan = new TimeSpan(0, 0, 1, 00);
var response = await blobLeaseClient.AcquireAsync(timeSpan);
Console.WriteLine("lease acquired and lease id is {0}", response.Value.LeaseId);

Console.ReadLine();



