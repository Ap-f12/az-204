using AzAuthWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzAuthWebApi.Controllers

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ProductService productService;
        

        public HomeController(IConfiguration configuration )
        {
           
            productService = new ProductService(configuration);
        }

       
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(productService.GetProducts());
        }
    }
}
