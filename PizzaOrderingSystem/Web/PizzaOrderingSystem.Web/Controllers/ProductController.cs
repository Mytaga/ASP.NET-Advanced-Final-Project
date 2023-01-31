using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
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
        public async Task<IActionResult> Index([FromQuery] string search, AllProductsQueryModel model)
        {
            var viewModel = await this.productService.GetAllByNameAsync(search, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            model.TotalProductsCount = viewModel.TotalProducts;
            model.Products = viewModel.Products;

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexPizza([FromQuery] string search, AllProductsQueryModel model)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.PizzaCategory, search, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);
            model.TotalProductsCount = viewModel.TotalProducts;
            model.Products = viewModel.Products;

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexSalads([FromQuery] string search, AllProductsQueryModel model)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.SaladCategory, search, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);
            model.TotalProductsCount = viewModel.TotalProducts;
            model.Products = viewModel.Products;

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexDesserts([FromQuery] string search, AllProductsQueryModel model)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.DessertCategory, search, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);
            model.TotalProductsCount = viewModel.TotalProducts;
            model.Products = viewModel.Products;

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexDrinks([FromQuery] string search, AllProductsQueryModel model)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.DrinkCategory, search, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);
            model.TotalProductsCount = viewModel.TotalProducts;
            model.Products = viewModel.Products;

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexSauces([FromQuery] string search, AllProductsQueryModel model)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.SauceCategory, search, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);
            model.TotalProductsCount = viewModel.TotalProducts;
            model.Products = viewModel.Products;

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexPasta([FromQuery] string search, AllProductsQueryModel model)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.PastaCategory, search, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);
            model.TotalProductsCount = viewModel.TotalProducts;
            model.Products = viewModel.Products;

            return this.View(model);
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