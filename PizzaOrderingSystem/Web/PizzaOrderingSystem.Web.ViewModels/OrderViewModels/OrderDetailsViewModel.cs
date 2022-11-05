using PizzaOrderingSystem.Data.Models;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.OrderViewModels
{
    public class OrderDetailsViewModel
    {
        public string OrderId { get; set; }

        public string TimeOfOrder { get; set; }

        public string TotalPrice { get; set; }

        public string DeliveryType { get; set; }

        public string Status { get; set; }

        public string PaymentType { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
