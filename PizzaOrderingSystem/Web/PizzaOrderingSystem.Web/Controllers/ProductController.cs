using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string search)
        {
            IQueryable<Product> allProducts = this.productService.GetAllByName(search);

            AllProductsViewModel viewModel = new AllProductsViewModel()
            {
                Products = allProducts.To<ListAllProductsViewModel>().ToArray(),
                SearchQuery = search,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            ICollection<ListProductCategoriesViewModel> allCategories =
                this.categoryService.All()
                .To<ListProductCategoriesViewModel>()
                .ToArray();

            CreateProductViewModel viewModel = new CreateProductViewModel();

            viewModel.Categories = allCategories;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Create", "Product");
            }

            if (!this.categoryService.ExistById(model.CategoryId))
            {
                return this.RedirectToAction("Create", "Product");
            }

            Product product = AutoMapperConfig.MapperInstance.Map<Product>(model);

            await this.productService.AddProduct(product);

            return this.RedirectToAction("Index", "Product");
        }
    }
}
