
using AzureCosmosDb;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connString = config["ConnectionStrings:PrimaryConnection"];
var dbName = "cosmosDb";
var containerName = "Orders";
var partitionKeyPath = "/Category";

var cosmosClient = new CosmosClient(connString);

await cosmosClient.CreateDatabaseAsync(dbName, 400);
await cosmosClient.GetDatabase(dbName).CreateContainerAsync(containerName, partitionKeyPath);   
Console.WriteLine("container created");
Console.ReadLine();



var order1 = new Order
{
    id = Guid.NewGuid().ToString(),
    OrderId = "O1",
    Category = "Laptops",
    Quantity = 200


};
Console.WriteLine(order1.id);

var order2= new Order
{
    id = Guid.NewGuid().ToString(),
    OrderId = "O2",
    Category = "Laptops",
    Quantity = 240


};

Console.WriteLine(order2.id);
Console.ReadLine();
var order3 = new Order
{
    id = Guid.NewGuid().ToString(),
    OrderId = "O3",
    Category = "Mobiles",
    Quantity = 300


};
var order4 = new Order
{
    id = Guid.NewGuid().ToString(),
    OrderId = "O4",
    Category = "Desktops",
    Quantity = 240

        
};

var cosmosContainer = cosmosClient.GetDatabase(dbName).GetContainer(containerName);
await cosmosContainer.CreateItemAsync<Order>(order1);
await cosmosContainer.CreateItemAsync<Order>(order2);
await cosmosContainer.CreateItemAsync<Order>(order3);
await cosmosContainer.CreateItemAsync<Order>(order4);



var sqlQuery = "SELECT o.OrderID, o.Category, o.Quantity FROM Orders o";
var queryDefinition = new QueryDefinition(sqlQuery);
var items = cosmosContainer.GetItemQueryIterator<Order>(queryDefinition);

while (items.HasMoreResults)
{
    var response = await items.ReadNextAsync();
    foreach(Order order in response)
    {
        Console.WriteLine(order.Category);
    }

}

var response1 = await cosmosContainer.ReadItemAsync<Order>(order1.id, new PartitionKey(order1.Category));
var itemToBeUpdated = response1.Resource;
itemToBeUpdated.Quantity = 299;

await cosmosContainer.ReplaceItemAsync<Order>(itemToBeUpdated, order1.id);
Console.WriteLine("item updated");
Console.ReadLine();





