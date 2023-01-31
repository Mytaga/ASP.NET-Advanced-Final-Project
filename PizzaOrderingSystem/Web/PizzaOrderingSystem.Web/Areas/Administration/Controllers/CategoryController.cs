using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;
        private readonly ILogger<CategoryController> logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            this.categoryService = categoryService;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateCategoryViewModel viewModel = new CreateCategoryViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.CreateAction);
            }

            try
            {
                if (await this.categoryService.ExistByNameAsync(model.Name))
                {
                    TempData[GlobalConstants.TempDataError] = ErrorConstants.ExistingCategory;
                    return this.RedirectToAction(GlobalConstants.CreateAction);
                }

                await this.categoryService.AddCategoryAsync(model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(GlobalConstants.CreateAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
            
            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.CreateCategory;

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
        }
    }
}