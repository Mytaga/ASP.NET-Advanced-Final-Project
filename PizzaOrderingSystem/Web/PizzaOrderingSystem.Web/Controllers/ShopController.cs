using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.ShopViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ShopController : BaseController
    {
        private readonly IShopService shopService;

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            CreateShopViewModel viewModel = new CreateShopViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateShopViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.CreateAction, GlobalConstants.ShopController);
            }

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ShopController);
        }
    }
}
