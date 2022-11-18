using PizzaOrderingSystem.Data.Common.Models;
using PizzaOrderingSystem.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingSystem.Data.Models
{
    public class Sale : BaseDeletableModel<string>
    {
        public Sale()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public decimal Amount { get; set; }

        public DateTime SaleDate { get; set; }

        [ForeignKey(nameof(Order))]
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
