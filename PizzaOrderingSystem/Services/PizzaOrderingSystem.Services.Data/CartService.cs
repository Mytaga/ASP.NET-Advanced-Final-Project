using PizzaOrderingSystem.Data.Models;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class CartService : ICartService
    {
        public Task AddToCartAsync(Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromCartAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
