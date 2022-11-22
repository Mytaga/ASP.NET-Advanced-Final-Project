using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string search)
        {
            var viewModel = await this.productService.GetAllByNameAsync(search);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexPizza(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.PizzaCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexSalads(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.SaladCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexDesserts(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.DessertCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexDrinks(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.DrinkCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexSauces(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.SauceCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            Product product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                return this.NotFound();
            }

            var viewModel = this.productService.GetProductDetails(product);

            return this.View(viewModel);
        }
    }
}