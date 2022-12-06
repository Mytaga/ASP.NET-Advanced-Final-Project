using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.Extensions;
using PizzaOrderingSystem.Web.ViewModels.ReviewViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ReviewController : BaseController
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
            var viewModel = await this.reviewService.GetAllAsync();

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public IActionResult Create()
        {
            CreateReviewViewModel viewModel = new CreateReviewViewModel();

            viewModel.AuthorName = this.User.Identity.Name;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            string userId = this.User.Id();

            await this.reviewService.AddReview(model, userId);

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.AddReview;

            return this.RedirectToAction(GlobalConstants.IndexAction);
        }
    }
}