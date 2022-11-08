using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.Extensions;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
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
                return this.RedirectToAction(GlobalConstants.UpdateProficeAction, GlobalConstants.AccountController);
            }

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

            await this.orderService.AddAsync(viewModel);

            return this.RedirectToAction(GlobalConstants.OrderDetailsAction, GlobalConstants.OrderController);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var order = await this.orderService.GetLastOrderAsync();
            var products = this.cartItemService.GetAllByOrder(order.Id);

            OrderDetailsViewModel viewModel = new OrderDetailsViewModel()
            {
                OrderId = order.Id,
                TimeOfOrder = order.TimeOfOrder.ToString("f", CultureInfo.InvariantCulture),
                TotalPrice = order.TotalPrice.ToString("C"),
                DeliveryType = order.DeliveryType.ToString(),
                Status = order.Status.ToString(),
                PaymentType = order.PaymentType.ToString(),
                Products = products.ToList(),
                Recipient = order.User.FirstName + " " + order.User.LastName,
                RecipientPhone = order.User.PhoneNumber,
                RecipientCity = order.User.Address.City,
                RecipientStreet = order.User.Address.Street,
                RecipientStreetNumber = order.User.Address.StreetNumber.ToString(),
                RecipientPostalCode = order.User.Address.PostCode.ToString(),
            };

            await this.cartService.ClearCartAsync();

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            var userId = this.User.Id();
            var viewModel = await this.orderService.GetUserOrders(userId);

            return this.View(viewModel);
        }
    }
}
