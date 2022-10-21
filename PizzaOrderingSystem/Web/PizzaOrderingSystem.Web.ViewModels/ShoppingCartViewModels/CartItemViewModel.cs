using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;

namespace PizzaOrderingSystem.Web.ViewModels.ShoppingCart
{
    public class CartItemViewModel : IMapFrom<CartItem>
    {
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal ItemPrice { get; set; }

        public decimal Amount => this.ItemPrice * this.Quantity;
    }
}
