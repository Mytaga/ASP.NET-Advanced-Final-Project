using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels;
using PizzaOrderingSystem.Web.ViewModels.Administration.Home;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class HomeController : AdministrationController
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IReviewService reviewService;
        private readonly ILogger<HomeController> logger;

        public HomeController(IUserService userService, IOrderService orderService, IProductService productService, IReviewService reviewService, ILogger<HomeController> logger)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.productService = productService;
            this.reviewService = reviewService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets statistic counts info about the application useful for the admin.
        /// </summary>
        /// <returns> 
        /// Count for : registered users, orders made, available products, reviews published.
        /// </returns>

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = new IndexViewModel()
                {
                    RegisteredUsers = await this.userService.GetUsersCountAsync(),
                    OrdersMade = await this.orderService.GetAllOrdersAsync(),
                    AvailableProducts = await this.productService.GetAllProductsCountAsync(),
                    ReviewsPublished = await this.reviewService.GetAllReviewsCountAsync(),
                };

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(GlobalConstants.IndexAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }          
        }

        /// <summary>
        /// Shows all registered users that are not in roles : Manager or Admin
        /// </summary>
        /// <returns>
        /// Table with information about registered users in descending order by orders made.
        /// </returns>
        
        [HttpGet]
        public async Task<IActionResult> ShowRegisteredUsers()
        {
            var viewModel = await this.userService.GetAllRegisterdUsersAsync();

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            this.logger.LogError(feature.Error, "TraceIdentifier: {0}", HttpContext.TraceIdentifier);

            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}