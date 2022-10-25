using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PizzaOrderingSystem.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
           this.CartItems = new HashSet<CartItemViewModel>();
        }

        public ICollection<CartItemViewModel> CartItems { get; set; }

        public decimal Total => this.CartItems.Sum(ci => ci.Amount);

        public string ExpectedDelivery => DateTime.Now.AddHours(1).ToString("f", CultureInfo.InvariantCulture);
    }
}