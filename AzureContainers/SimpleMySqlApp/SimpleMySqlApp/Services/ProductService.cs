using MySql.Data.MySqlClient;
using SimpleMySqlApp.Model;

namespace SimpleMySqlApp.Services
{
    public class ProductService : IProductService
    {
        private readonly string _connectionString;
        public ProductService(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public List<Product> GetProducts()
            
        {
            var products = new List<Product>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Products";
                    var reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            ProductID = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Quantity = reader.GetInt32(2)
                        };
                        products.Add(product);
                    }
                    connection.Close();
                    return products;
                }
            }
            catch(Exception ex)
            {
                var product = new Product
                {
                    ProductID = 0,
                    ProductName = "empty",
                    Quantity = 0
                };
                products.Add(product);
                return products;

            }
           
        }
    }
 
}
