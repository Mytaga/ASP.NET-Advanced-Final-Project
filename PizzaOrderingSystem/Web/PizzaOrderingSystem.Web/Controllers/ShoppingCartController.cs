using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
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
        public async Task<IActionResult> Index()
        {
            var allItems = await this.cartService.GetCartItemsAsync();

            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();

            var allViewItems = new HashSet<CartItemViewModel>();

            foreach (var item in allItems)
            {
                var viewItem = new CartItemViewModel()
                {
                    Id = item.Id,
                    ItemName = item.Product.Name,
                    ItemPrice = item.Product.Price,
                    Quantity = item.Quantity,
                    ImageUrl = item.Product.ImageUrl,
                };

                allViewItems.Add(viewItem);
            }

            viewModel.CartItems = allViewItems;

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            var product = await this.productService.GetByIdАsync(id);

            if (product != null)
            {
                await this.cartService.AddToCartAsync(product);
            }

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ProductController);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            var item = await this.cartItemService.GetByIdАsync(id);

            if (item != null)
            {
                await this.cartService.RemoveFromCartAsync(item);
            }

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }

        [HttpGet]
        public async Task<IActionResult> Clear()
        {
            await this.cartService.ClearCartAsync();

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseQuantity(string id)
        {
            var item = await this.cartItemService.GetByIdАsync(id);

            if (item != null)
            {
                await this.cartService.IncreaseQuantity(item);
            }

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }

        [HttpGet]
        public async Task<IActionResult> DecreaseQuantity(string id)
        {
            var item = await this.cartItemService.GetByIdАsync(id);

            if (item != null)
            {
                await this.cartService.DecreaseQuantity(item);
            }

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }
    }
}