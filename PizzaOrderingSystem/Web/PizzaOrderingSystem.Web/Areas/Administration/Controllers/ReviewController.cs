using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class ReviewController : AdministrationController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.reviewService.GetAllAsync();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var review = await this.reviewService.GetByIdAsync(id);

            if (review == null)
            {
                return this.NotFound();
            }

            await this.reviewService.DeleteReview(review);

            TempData["message"] = "You have successfully deleted this review!";

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }
    }
}