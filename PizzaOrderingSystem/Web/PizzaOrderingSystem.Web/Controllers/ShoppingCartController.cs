using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ShoppingCart;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly ICartItemService cartItemService;
        private readonly ICartService cartService;
        private readonly IProductService productService;

        public ShoppingCartController(ICartItemService cartItemService, ICartService cartService, IProductService productService)
        {
            this.cartItemService = cartItemService;
            this.cartService = cartService;
            this.productService = productService;
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

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            var product = await this.productService.GetByIdАsync(id);

            if (product != null)
            {
                await this.cartService.AddToCartAsync(product, 1);
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}