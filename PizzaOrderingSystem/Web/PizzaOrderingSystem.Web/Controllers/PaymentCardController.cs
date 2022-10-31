using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Services.Data;
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

        public async Task<IActionResult> Add()
        {
            return this.View();
        }
    }
}
