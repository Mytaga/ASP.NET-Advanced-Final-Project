using Azure.Storage.Blobs;
using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Security.Policy;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class ProductController : AdministrationController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<ProductController> logger;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, ILogger logger)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var viewModel = await this.productService.GetAllByNameAsync(search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> IndexPizza(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.PizzaCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> IndexSalads(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.SaladCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> IndexDesserts(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.DessertCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> IndexDrinks(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.DrinkCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> IndexSauces(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.SauceCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> IndexPasta(string search)
        {
            var viewModel = await this.productService.GetAllByCategoryAsync(GlobalConstants.PastaCategory, search);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateProductViewModel viewModel = new CreateProductViewModel();

            IEnumerable<ListProductCategoriesViewModel> allCategories = await
                this.categoryService.AllAsync();

            viewModel.Categories = allCategories;

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.CreateAction);
            }

            try
            {
                if (!await this.categoryService.ExistByIdAsync(model.CategoryId))
                {
                    TempData[GlobalConstants.TempDataError] = ErrorConstants.UnexistingCategory;
                    return this.RedirectToAction(GlobalConstants.CreateAction);
                }

                string uniqueFileName = await this.UploadPhoto(model.ImageUrl);

                await this.productService.AddProductAsync(model, uniqueFileName);
            }
            catch (Exception ex)
            {
                this.logger.LogError(GlobalConstants.CreateAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
            
            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.CreateProduct;

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                return this.NotFound();
            }

            var viewModel = await this.productService.GetEditModel(product);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.EditAction);
            }

            try
            {
                if (!await this.categoryService.ExistByIdAsync(model.CategoryId))
                {
                    TempData[GlobalConstants.TempDataError] = ErrorConstants.UnexistingCategory;
                    return this.RedirectToAction(GlobalConstants.EditAction);
                }

                string uniqueFileName = await this.UploadPhoto(model.ImageUrl);

                await this.productService.EditProductAsync(model, id, uniqueFileName);
            }
            catch (Exception ex)
            {
                this.logger.LogError(GlobalConstants.EditAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
            
            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.EditProduct;

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                return this.NotFound();
            }

            try
            {
                await this.productService.DeleteProductAsync(product);
            }
            catch (Exception ex)
            {
                this.logger.LogError(GlobalConstants.DeleteAction, ex); 
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.DeleteProduct;

            return this.RedirectToAction(GlobalConstants.IndexAction);
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

        //private string UploadFile(IFormFile imageUrl)
        //{
        //    string uniqueFileName = null;

        //    if (imageUrl != null)
        //    {
        //        string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "img");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + imageUrl.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            imageUrl.CopyTo(fileStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}

       
    }
}