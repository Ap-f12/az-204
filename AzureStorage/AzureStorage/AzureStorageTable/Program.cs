
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connSting = config["ConnectionStrings:DefaultConnection"];
var tableName = "Products";
Console.WriteLine(connSting);
Console.ReadLine();

TableClient tableClient = new TableClient(connSting, tableName);



async Task AddEntitiesToTable(string orderId, string category, int quantity)
{
    var tableEntity = new TableEntity(category, orderId)
    {
        {"quantity", quantity }
    };
  await   tableClient.AddEntityAsync(tableEntity);
}


await AddEntitiesToTable("01", "laptops", 200);
await AddEntitiesToTable("02", "Mobiles", 200);
await AddEntitiesToTable("03", "Desktops", 200);
await AddEntitiesToTable("04", "laptops", 200);
await AddEntitiesToTable("05", "laptops", 200);

Console.WriteLine("table rows added");
Console.ReadLine();


Pageable<TableEntity> results = tableClient.Query<TableEntity>(entity => entity.PartitionKey == "laptops");
foreach(TableEntity tableEntity in results)
{
    Console.WriteLine("Row key is {0}",tableEntity.RowKey);
    Console.WriteLine("Quantity is {0}", tableEntity.GetInt32("quantity"));
}

Console.ReadLine();
await tableClient.DeleteEntityAsync("laptops", "01");
Console.WriteLine("row deleted");
Console.ReadLine();


var updatedTableEntity = new TableEntity("laptops", "05")
    {
        {"quantity", 500 }
    };
Console.WriteLine("row updated");
Console.ReadLine();


await tableClient.UpdateEntityAsync<TableEntity>(updatedTableEntity, ETag.All, TableUpdateMode.Replace);
Pageable<TableEntity> updatedResults = tableClient.Query<TableEntity>(entity => entity.PartitionKey == "laptops");

foreach (TableEntity tableEntity in updatedResults)
{
    Console.WriteLine("Row key is {0}", tableEntity.RowKey);
    Console.WriteLine("Quantity is {0}", tableEntity.GetInt32("quantity"));
}

Console.ReadLine();
