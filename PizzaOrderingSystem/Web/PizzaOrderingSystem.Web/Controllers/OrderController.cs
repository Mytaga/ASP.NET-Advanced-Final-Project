using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.Extensions;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Globalization;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.UserRoleName)]
    public class OrderController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;
        private readonly IOrderService orderService;
        private readonly ICartItemService cartItemService;

        public OrderController(UserManager<ApplicationUser> userManager, ICartService cartService, IOrderService orderService, ICartItemService cartItemService)
        {
            this.userManager = userManager;
            this.cartService = cartService;
            this.orderService = orderService;
            this.cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            var userId = this.User.Id();

            var user = await this.userManager.FindByIdAsync(userId);

            if (user.Address == null)
            {
                return this.RedirectToAction(GlobalConstants.UpdateProfileAction, GlobalConstants.AccountController);
            }

            CreateOrderViewModel viewModel = new CreateOrderViewModel()
            {
                TotalPrice = this.cartService.GetShoppingCartTotal().ToString("F"),
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.ConfirmOrderAction, GlobalConstants.OrderController);
            }

            await this.orderService.AddAsync(viewModel);

            return this.RedirectToAction(GlobalConstants.OrderDetailsAction, GlobalConstants.OrderController);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var order = await this.orderService.GetLastOrderAsync();

            var viewModel = this.orderService.GetOrderDetails(order);

            await this.cartService.ClearCartAsync();

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            var userId = this.User.Id();
            var viewModel = await this.orderService.GetUserOrdersAsync(userId);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserOrderDetails(string id)
        {
            var userId = this.User.Id();
            var viewModel = await this.orderService.GetUserOrderDetailsAsync(userId, id);

            return this.View(viewModel);
        }
    }
}