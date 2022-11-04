using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PizzaOrderingSystem.Web.ViewModels.OrderViewModels
{
    public class CreateOrderViewModel
    {
        public DateTime TimeOfOrder => DateTime.Now;

        [Required]
        public string TotalPrice { get; set; }

        public OrderType DeliveryType => OrderType.Delivery;

        public OrderStatus Status => OrderStatus.Active;

        public PaymentType PaymentType { get; set; }

        [Required]
        public string ExpectedDelivery => DateTime.Now.AddHours(1).ToString("f", CultureInfo.InvariantCulture);

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        public int Floor { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ICollection<CreditCard> Cards { get; set; }
    }
}
