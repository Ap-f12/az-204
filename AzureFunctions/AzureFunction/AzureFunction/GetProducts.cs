using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AzureFunction
{
    public static class GetProducts
    {
        [FunctionName("GetProducts")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<Product> _productList = new List<Product>();
            string _sqlStatement = "Select * from Products";
            SqlConnection _connection = GetConnection();
            _connection.Open();
            SqlCommand _sqlCommand = new SqlCommand(_sqlStatement, _connection);
            using(SqlDataReader _reader = _sqlCommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)

                    };
                    _productList.Add(_product);
                    
                }
            }
            _connection.Close();
            return new OkObjectResult(_productList);
        
            

           
        }
        private static SqlConnection GetConnection()
        {
            string connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
        
            return new SqlConnection(connectionString);
            
        }
    }
}
