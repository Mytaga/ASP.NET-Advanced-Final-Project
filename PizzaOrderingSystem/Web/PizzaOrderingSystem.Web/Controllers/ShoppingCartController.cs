using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ShoppingCart;
using System.Linq;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly ICartItemService cartItemService;
        private readonly ICartService cartService;

        public ShoppingCartController(ICartItemService cartItemService, ICartService cartService)
        {
            this.cartItemService = cartItemService;
            this.cartService = cartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<CartItem> allItems = this.cartItemService.GetAllByName();

            ShoppingCartViewModel viewModel = new ShoppingCartViewModel()
            {
                CartItems = allItems.To<CartItemViewModel>().ToArray(),
            };

            return this.View(viewModel);
        }
    }
}
