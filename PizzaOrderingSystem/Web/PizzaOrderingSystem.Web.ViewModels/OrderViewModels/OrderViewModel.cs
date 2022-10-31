using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Data.Models.Enums;
using PizzaOrderingSystem.Services.Mapping;
using System;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.OrderViewModels
{
    public class OrderViewModel : IMapFrom<Order>
    {
        public DateTime TimeOfOrder { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderType DeliveryType { get; set; }

        public OrderStatus Status { get; set; }

        public string UserId { get; set; }

        public string ShopId { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
