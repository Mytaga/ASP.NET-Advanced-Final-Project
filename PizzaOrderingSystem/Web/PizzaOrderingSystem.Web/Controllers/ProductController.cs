using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create()
        {
            CreateProductViewModel viewModel = new CreateProductViewModel();

            IEnumerable<ListProductCategoriesViewModel> allCategories = await
                this.categoryService.AllAsync();

            viewModel.Categories = allCategories;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            if (!await this.categoryService.ExistByIdAsync(model.CategoryId))
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            string uniqueFileName = this.UploadFile(model);

            Product product = AutoMapperConfig.MapperInstance.Map<Product>(model);
            product.ImageUrl = uniqueFileName;

            await this.productService.AddProductAsync(product);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController);
            }

            EditProductViewModel viewModel = new EditProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };

            var allCategories = await
               this.categoryService.AllAsync();

            viewModel.Categories = allCategories;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Edit));
            }

            if (!await this.categoryService.ExistByIdAsync(model.CategoryId))
            {
                return this.RedirectToAction(nameof(this.Edit));
            }

            Product product = await this.productService.GetByIdАsync(id);
            Category category = await this.categoryService.GetByIdAsync(model.CategoryId);

            string uniqueFileName = this.UploadFile(model);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Category = category;

            if (uniqueFileName != null)
            {
                product.ImageUrl = uniqueFileName;
            }

            await this.productService.EditProductAsync(product);

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ProductController);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                return this.NotFound();
            }

            await this.productService.DeleteProductAsync(product);

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ProductController);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            Product product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                return this.RedirectToAction(GlobalConstants.ErrorAction, GlobalConstants.HomeController);
            }

            var viewModel = this.productService.GetProductDetails(product);

            return this.View(viewModel);
        }

        private string UploadFile(CreateProductInputModel model)
        {
            string uniqueFileName = null;

            if (model.ImageUrl != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        private string UploadFile(EditProductInputModel model)
        {
            string uniqueFileName = null;

            if (model.ImageUrl != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}