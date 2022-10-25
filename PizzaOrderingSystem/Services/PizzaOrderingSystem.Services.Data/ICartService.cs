using PizzaOrderingSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICartService
    {
        Task AddToCartAsync(Product product, int quantity);

        Task RemoveFromCartAsync(CartItem item);

        Task<IEnumerable<CartItem>> GetCartItemsAsync();

        Task ClearCartAsync();
    }
}
