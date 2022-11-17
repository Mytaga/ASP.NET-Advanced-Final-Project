using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.Extensions;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.UserRoleName)]
    public class PaymentCardController : Controller
    {
        private readonly IPaymentCardService paymentCardService;

        public PaymentCardController(IPaymentCardService paymentCardService)
        {
            this.paymentCardService = paymentCardService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = this.User.Id();

            AddCardViewModel viewModel = await this.paymentCardService.GetAllАsync(userId);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddCardViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.AddAction, GlobalConstants.PaymentCardController);
            }

            model.UserId = this.User.Id();
            await this.paymentCardService.AddAsync(model);

            return this.RedirectToAction(GlobalConstants.AddAction, GlobalConstants.PaymentCardController);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var card = await this.paymentCardService.GetByIdAsync(id);

            if (card == null)
            {
                return this.NotFound();
            }

            await this.paymentCardService.Delete(card);

            return this.RedirectToAction(GlobalConstants.AddAction, GlobalConstants.PaymentCardController);
        }
    }
}