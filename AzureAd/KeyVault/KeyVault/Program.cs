using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
var tenantId = config["Credentials:tenantId"];
var clientId = config["Credentials:clientId"];
var clientSecret = config["Credentials:clientSecret"];
var vaultUri = config["VaultUri"];

var clientSecretCredentials = new ClientSecretCredential(tenantId, clientId, clientSecret);

var kvc = new SecretClient(new Uri(vaultUri), clientSecretCredentials);

KeyVaultSecret secret =  kvc.GetSecret("keyVaultSecret1");

Console.WriteLine(secret.Value);
