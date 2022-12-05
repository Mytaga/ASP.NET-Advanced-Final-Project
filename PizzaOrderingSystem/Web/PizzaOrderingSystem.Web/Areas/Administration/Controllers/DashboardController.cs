namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaOrderingSystem.Services.Data;
    using PizzaOrderingSystem.Web.ViewModels.Administration.Dashboard;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IReviewService reviewService;

        public DashboardController(IUserService userService, IOrderService orderService, IProductService productService, IReviewService reviewService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.productService = productService;
            this.reviewService = reviewService;
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
            var viewModel = new IndexViewModel()
            {
                RegisteredUsers = await this.userService.GetUsersCountAsync(),
                OrdersMade = await this.orderService.GetAllOrdersAsync(),
                AvailableProducts = await this.productService.GetAllProductsCountAsync(),
                ReviewsPublished = await this.reviewService.GetAllReviewsCountAsync(),
            };

            return this.View(viewModel);
        }

        /// <summary>
        /// Shows all registered users that are not in roles : Manager or Admin
        /// </summary>
        /// <returns>
        /// Table with information about registered users.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> ShowRegisteredUsers()
        {
            var viewModel = await this.userService.GetAllRegisterdUsersAsync();

            return this.View(viewModel);
        }
    }
}
