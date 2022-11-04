using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.OrderViewModels
{
    public class CreateOrderViewModel
    {
        public DateTime TimeOfOrder => DateTime.Now;

        public decimal TotalPrice { get; set; }

        public OrderType DeliveryType => OrderType.Delivery;

        public OrderStatus Status => OrderStatus.Active;

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int Floor { get; set; }

        public string UserId { get; set; }

        public string ShopId { get; set; }

        public virtual ICollection<CreditCard> Cards { get; set; }
    }
}
