using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var connectionString = config["ConnectionString"];

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);

IDatabase database = redis.GetDatabase();

database.StringSet("top:3:courses", "AZ-104,AZ-305,AZ-204");

Console.WriteLine("Cache data set");

Console.ReadLine();

if (database.KeyExists("top:3:courses"))
    Console.WriteLine(database.StringGet("top:3:courses"));
else
    Console.WriteLine("key does not exist");