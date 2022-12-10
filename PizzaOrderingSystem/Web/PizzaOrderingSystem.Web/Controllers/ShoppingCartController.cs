using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.ShoppingCart;
using System;
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
        private readonly ILogger<ShoppingCartController> logger;

        public ShoppingCartController(ICartItemService cartItemService, ICartService cartService, IProductService productService, ILogger<ShoppingCartController> logger)
        {
            this.cartItemService = cartItemService;
            this.cartService = cartService;
            this.productService = productService;
            this.logger = logger;
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
            try
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
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.AddAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
            
            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.AddToCart;

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ProductController);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                var item = await this.cartItemService.GetByIdАsync(id);

                if (item != null)
                {
                    await this.cartService.RemoveFromCartAsync(item);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.RemoveAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
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