using System;

namespace PizzaOrderingSystem.Web.ViewModels.SaleViewModels
{
    public class SaleViewModel
    {
        public string Id { get; set; }

        public string Amount { get; set; }

        public DateTime SaleDate { get; set; }

        public string PaymentType { get; set; }
    }
}
