using PizzaOrderingSystem.Data.Common.Models;
using PizzaOrderingSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingSystem.Data.Models
{
    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.OrderProducts = new HashSet<CartItem>();
        }

        public DateTime TimeOfOrder { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderType DeliveryType { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentType PaymentType { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CartItem> OrderProducts { get; set; }
    }
}
