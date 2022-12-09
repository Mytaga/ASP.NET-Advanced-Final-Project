using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICartService
    {
        Task AddToCartAsync(Product product);

        Task RemoveFromCartAsync(CartItem item);

        Task<IEnumerable<CartItem>> GetCartItemsAsync();

        Task ClearCartAsync();

        Task IncreaseQuantity(CartItem item);

        Task DecreaseQuantity(CartItem item);

        decimal GetShoppingCartTotal();

        Task<IEnumerable<CartItem>> GetCartProductsAsync();

        int GetShoppingCartItemCount();

        ShoppingCartViewModel GetShoppingCart(IEnumerable<CartItem> items);
    }
}