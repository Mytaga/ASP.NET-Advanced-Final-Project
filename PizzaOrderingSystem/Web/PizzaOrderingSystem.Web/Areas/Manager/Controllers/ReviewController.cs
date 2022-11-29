using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    public class ReviewController : ManagerController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.reviewService.GetAll();

            return this.View(viewModel);
        }
    }
}