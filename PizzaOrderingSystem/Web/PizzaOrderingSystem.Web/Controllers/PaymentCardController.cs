using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
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
            AddCardViewModel viewModel = await this.paymentCardService.GetAll();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Add(AddCardViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.AddAction, GlobalConstants.PaymentCardController);
            }

            await this.paymentCardService.AddAsync(model);

            return this.RedirectToAction(GlobalConstants.ViewProfileAction, GlobalConstants.AccountController);
        }

        public async Task<IActionResult> Delete()
        {
            return this.View();
        }
    }
}
