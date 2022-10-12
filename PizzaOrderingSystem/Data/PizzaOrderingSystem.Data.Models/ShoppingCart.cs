using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Data.Models
{
    public class ShoppingCart : BaseDeletableModel<string>
    {
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<CartItem> Items { get; set; }
    }
}
