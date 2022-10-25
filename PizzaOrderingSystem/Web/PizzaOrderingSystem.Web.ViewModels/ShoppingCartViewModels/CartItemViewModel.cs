using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;

namespace PizzaOrderingSystem.Web.ViewModels.ShoppingCart
{
    public class CartItemViewModel : IMapFrom<CartItem>
    {
        public string Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal ItemPrice { get; set; }

        public string ImageUrl { get; set; }

        public decimal Amount => this.ItemPrice * this.Quantity;
    }
}
