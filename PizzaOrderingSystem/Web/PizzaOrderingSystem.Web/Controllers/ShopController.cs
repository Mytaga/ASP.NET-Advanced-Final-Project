using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Services.Data;
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
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.shopService.GetAllAsync();

            return this.View(viewModel);
        }
    }
}
