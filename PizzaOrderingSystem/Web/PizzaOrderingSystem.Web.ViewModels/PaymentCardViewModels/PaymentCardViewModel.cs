using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;

namespace PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels
{
    public class PaymentCardViewModel : IMapFrom<CreditCard>
    {
        public string Id { get; set; }

        public string CardNumber { get; set; }

        public string ImageUrl { get; set; }
    }
}
