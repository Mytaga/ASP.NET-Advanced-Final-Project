using PizzaOrderingSystem.Data.Common.Models;
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

        [ForeignKey(nameof(Shop))]
        public string ShopId { get; set; }

        public virtual Shop Shop { get; set; }
    }
}
