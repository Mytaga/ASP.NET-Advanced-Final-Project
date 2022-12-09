using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.UserRoleName)]
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

            var viewModel = this.cartService.GetShoppingCart(allItems);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                TempData[GlobalConstants.TempDataError] = ErrorConstants.UnauthorizedAction;
                return this.RedirectToAction(GlobalConstants.LoginAction, GlobalConstants.AccountController);
            }

            var product = await this.productService.GetByIdАsync(id);

            if (product == null)
            {
                TempData[GlobalConstants.TempDataError] = ErrorConstants.UnexistingProduct;
                return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ProductController);
            }

            await this.cartService.AddToCartAsync(product);
            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.AddToCart;

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