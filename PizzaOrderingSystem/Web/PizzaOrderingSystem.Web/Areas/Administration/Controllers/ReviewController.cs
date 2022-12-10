using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class ReviewController : AdministrationController
    {
        private readonly IReviewService reviewService;
        private readonly ILogger<ReviewController> logger;

        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            this.reviewService = reviewService;
            this.logger = logger;
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

            try
            {
                await this.reviewService.DeleteReview(review);
            }
            catch (Exception ex)
            {
                this.logger.LogError(GlobalConstants.DeleteAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.DeleteReview; 

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }
    }
}