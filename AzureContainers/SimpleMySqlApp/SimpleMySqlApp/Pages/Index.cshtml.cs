using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleMySqlApp.Model;
using SimpleMySqlApp.Services;

namespace SimpleMySqlApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> Products;
        public void OnGet()
        {
            Products = _productService.GetProducts();

        }
    }
}