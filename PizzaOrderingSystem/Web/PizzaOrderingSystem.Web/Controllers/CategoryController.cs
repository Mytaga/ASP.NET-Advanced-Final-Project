using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            CreateCategoryViewModel viewModel = new CreateCategoryViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            var category = AutoMapperConfig.MapperInstance.Map<Category>(model);

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Create", "Category");
            }

            if (this.categoryService.ExistById(category.Id))
            {
                return this.RedirectToAction("Create", "Category");
            }

            await this.categoryService.AddCategory(category);

            return this.RedirectToAction("Index", "Product");
        }
    }
}
