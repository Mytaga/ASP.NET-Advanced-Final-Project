using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.Extensions;
using PizzaOrderingSystem.Web.ViewModels.ReviewViewModels;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly ILogger<ReviewController> logger;

        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            this.reviewService = reviewService;
            this.logger = logger;
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
                return this.RedirectToAction(GlobalConstants.CreateAction);
            }

            try
            {
                string userId = this.User.Id();
                await this.reviewService.AddReview(model, userId);                           
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.CreateAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage);
            }

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.AddReview;
            return this.RedirectToAction(GlobalConstants.IndexAction);
        }
    }
}