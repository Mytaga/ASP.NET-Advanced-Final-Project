using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

            string uniqueFileName = this.UploadFile(model);

            Product product = AutoMapperConfig.MapperInstance.Map<Product>(model);
            product.ImageUrl = uniqueFileName;

            await this.productService.AddProduct(product);

            return this.RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            EditProductViewModel viewModel = new EditProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };

            ICollection<ListProductCategoriesViewModel> allCategories =
               this.categoryService.All()
               .To<ListProductCategoriesViewModel>()
               .ToArray();

            viewModel.Categories = allCategories;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(string id, EditProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", "Product");
            }

            if (!this.categoryService.ExistById(model.CategoryId))
            {
                return this.RedirectToAction("Edit", "Product");
            }

            Product product = await this.productService.GetByIdАsync(id);
            Category category = this.categoryService.GetById(model.CategoryId);

            string uniqueFileName = this.UploadFile(model);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Category = category;

            if (uniqueFileName != null)
            {
                product.ImageUrl = uniqueFileName;
            }

            this.productService.EditProduct(product);

            return this.RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete(string id)
        {
            var product = this.productService.GetById(id);

            if (product == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult DeleteConfirmed(string id)
        {
            var product = this.productService.GetById(id);

            if (product == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            this.productService.DeleteProduct(product);

            return this.RedirectToAction("Index", "Product");
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