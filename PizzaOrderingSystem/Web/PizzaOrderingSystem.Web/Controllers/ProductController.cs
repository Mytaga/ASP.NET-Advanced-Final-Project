using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Services.Data;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
