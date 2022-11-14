using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;

namespace PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels
{
    public class PaymentCardViewModel : IMapFrom<CreditCard>
    {
        public string Id { get; set; }

        public string CardNumber { get; set; }

        public string ImageUrl => "https://img.icons8.com/color/48/000000/mastercard-logo.png";

    }
}
