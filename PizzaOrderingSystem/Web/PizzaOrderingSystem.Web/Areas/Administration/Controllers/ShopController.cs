using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.ShopViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class ShopController : AdministrationController
    {
        private readonly IShopService shopService;

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.shopService.GetAllAsync();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateShopViewModel viewModel = new CreateShopViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShopViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.CreateAction, GlobalConstants.ShopController);
            }

            await this.shopService.CreateAsync(viewModel);

            TempData["message"] = "You have successfully added a new restaurant!";

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ShopController);
        }
    }
}
