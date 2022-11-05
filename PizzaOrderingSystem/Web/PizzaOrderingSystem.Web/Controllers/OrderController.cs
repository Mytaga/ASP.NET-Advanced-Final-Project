using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;
        private readonly IOrderService ordertService;

        public OrderController(UserManager<ApplicationUser> userManager, ICartService cartService, IOrderService orderService)
        {
            this.userManager = userManager;
            this.cartService = cartService;
            this.ordertService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
 
            var user = await this.userManager.FindByIdAsync(userId);

            CreateOrderViewModel viewModel = new CreateOrderViewModel()
            {
                TotalPrice = this.cartService.GetShoppingCartTotal(),
                UserId = userId,
                Cards = user.CreditCards,
                City = user.Address.City,
                Street = user.Address.Street,
                StreetNumber = user.Address.StreetNumber,
                Floor = user.Address.Floor,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.ConfirmOrderAction, GlobalConstants.OrderController);
            }

            await this.ordertService.AddAsync(viewModel);
            await this.cartService.ClearCartAsync();

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
        }
    }
}
