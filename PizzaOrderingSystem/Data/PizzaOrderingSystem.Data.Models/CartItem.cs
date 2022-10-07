using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingSystem.Data.Models
{
    public class CartItem : BaseDeletableModel<string>
    {
        public CartItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int Quantity { get; set; }

        [ForeignKey(nameof(Pizza))]
        public string PizzaId { get; set; }

        public virtual Pizza Pizza { get; set; }

        [ForeignKey(nameof(Dessert))]
        public string DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }

        [ForeignKey(nameof(Beverage))]
        public string BeverageId { get; set; }

        public virtual Beverage Beverage { get; set; }
    }
}
