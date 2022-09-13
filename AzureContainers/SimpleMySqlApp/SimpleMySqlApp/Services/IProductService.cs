using SimpleMySqlApp.Model;

namespace SimpleMySqlApp.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
