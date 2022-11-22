using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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

            if (await this.categoryService.ExistByNameAsync(model.Name))
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            await this.categoryService.AddCategoryAsync(model);

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ProductController);
        }
    }
}