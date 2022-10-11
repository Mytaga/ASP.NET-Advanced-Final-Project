using PizzaOrderingSystem.Data.Common.Models;
using System;

namespace PizzaOrderingSystem.Data.Models
{
    public class ShoppingCart : BaseDeletableModel<string>
    {
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
