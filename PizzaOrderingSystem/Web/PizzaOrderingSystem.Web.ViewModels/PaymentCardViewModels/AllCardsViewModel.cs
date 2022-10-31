using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels
{
    public class AllCardsViewModel
    {
        public virtual ICollection<PaymentCardViewModel> Cards { get; set; }
    }
}
