using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.Extensions;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.UserRoleName)]
    public class PaymentCardController : Controller
    {
        private readonly IPaymentCardService paymentCardService;
        private readonly ILogger<PaymentCardController> logger;

        public PaymentCardController(IPaymentCardService paymentCardService, ILogger<PaymentCardController> logger)
        {
            this.paymentCardService = paymentCardService;
            this.logger = logger;
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

            try
            {
                model.UserId = this.User.Id();
                await this.paymentCardService.AddAsync(model);
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.AddAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
            
            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.AddCreditCard;

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

            try
            {
                await this.paymentCardService.Delete(card);
            }
            catch (Exception ex)
            {
                logger.LogError(GlobalConstants.DeleteAction, ex);
                throw new ApplicationException(ErrorConstants.ExceptionMessage, ex);
            }
           
            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.DeleteCreditCard;

            return this.RedirectToAction(GlobalConstants.AddAction, GlobalConstants.PaymentCardController);
        }
    }
}