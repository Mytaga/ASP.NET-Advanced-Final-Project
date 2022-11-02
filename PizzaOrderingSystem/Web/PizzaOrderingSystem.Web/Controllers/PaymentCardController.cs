using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System.Linq;
using System.Security.Claims;
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
            string userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            AddCardViewModel viewModel = await this.paymentCardService.GetAll(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Add(AddCardViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(GlobalConstants.AddAction, GlobalConstants.PaymentCardController);
            }

            model.UserId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await this.paymentCardService.AddAsync(model);

            return this.RedirectToAction(GlobalConstants.AddAction, GlobalConstants.PaymentCardController);
        }

        public async Task<IActionResult> Delete()
        {
            return this.View();
        }
    }
}
