using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Exceptions;
using PizzaOrderingSystem.Web.Extensions;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.UserRoleName)]
    public class OrderController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;
        private readonly IOrderService orderService;
        private readonly IGuard guard;
        private readonly ILogger<OrderController> logger;

        public OrderController(UserManager<ApplicationUser> userManager, ICartService cartService, IOrderService orderService, IGuard guard, ILogger<OrderController> logger)
        {
            this.userManager = userManager;
            this.cartService = cartService;
            this.orderService = orderService;
            this.guard = guard;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Confirm()
        {
            if (this.cartService.GetShoppingCartItemCount() == 0)
            {
                TempData[GlobalConstants.TempDataError] = ErrorConstants.EmptyCart;
                return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ShoppingCartController);
            }

            try
            {
                var userId = this.User.Id();

                var user = await this.userManager.FindByIdAsync(userId);

                if (user.Address != null)
                {
                    CreateOrderViewModel viewModel = this.orderService.GetOrderView(user);

                    return this.View(viewModel);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.ConfirmOrderAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
            
            TempData[GlobalConstants.TempDataError] = ErrorConstants.AddressMissing;     
            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ShoppingCartController);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.ConfirmOrderAction, GlobalConstants.OrderController);
            }

            try
            {
                await this.orderService.AddAsync(viewModel);              
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.CreateAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.OrderPlaced;
            return this.RedirectToAction(GlobalConstants.OrderDetailsAction, GlobalConstants.OrderController);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            try
            {
                var order = await this.orderService.GetLastOrderAsync();

                var viewModel = this.orderService.GetOrderDetails(order);

                await this.cartService.ClearCartAsync();

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.DetailsAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }           
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            try
            {
                var userId = this.User.Id();
                var viewModel = await this.orderService.GetUserOrdersAsync(userId);

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.UserOrdersAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserOrderDetails(string id)
        {
            var userId = this.User.Id();

            this.guard.AgainstNull(id, ErrorConstants.OrderMissing);

            var viewModel = await this.orderService.GetUserOrderDetailsAsync(userId, id);

            return this.View(viewModel);
        }
    }
}