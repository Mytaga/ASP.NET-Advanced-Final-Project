using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels;
using System.Diagnostics;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class HomeController : DashboardController
    {
		public HomeController(IUserService userService, IOrderService orderService, IProductService productService, IReviewService reviewService) 
            : base(userService, orderService, productService, reviewService)
		{

		}

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}