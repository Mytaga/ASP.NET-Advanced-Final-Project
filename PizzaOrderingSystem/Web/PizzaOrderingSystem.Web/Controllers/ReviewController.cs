using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ReviewViewModels;
using System.Linq;
using System.Security.Claims;
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
           var viewModel = await this.reviewService.GetAll();

           return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateReviewViewModel viewModel = new CreateReviewViewModel();

            viewModel.AuthorName = this.User.Identity.Name;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            Review review = AutoMapperConfig.MapperInstance.Map<Review>(model);
            review.UserId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await this.reviewService.AddReview(review);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete(string id)
        {
            var product = this.reviewService.GetById(id);

            if (product == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await this.reviewService.GetByIdAsync(id);

            if (product == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            await this.reviewService.DeleteReview(product);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
