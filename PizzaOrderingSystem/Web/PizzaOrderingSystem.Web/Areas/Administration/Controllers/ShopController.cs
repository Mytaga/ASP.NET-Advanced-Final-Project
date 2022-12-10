using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.ShopViewModels;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class ShopController : AdministrationController
    {
        private readonly IShopService shopService;
        private readonly ILogger<ShopController> logger;

        public ShopController(IShopService shopService, ILogger<ShopController> logger)
        {
            this.shopService = shopService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.shopService.GetAllAsync();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateShopViewModel viewModel = new CreateShopViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShopViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.CreateAction, GlobalConstants.ShopController);
            }

            try
            {
                await this.shopService.CreateAsync(viewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(GlobalConstants.CreateAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.CreateRestaurant;

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.ShopController);
        }
    }
}
