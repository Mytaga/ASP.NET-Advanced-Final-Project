using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using System.Linq;

namespace PizzaOrderingSystem.Services.Data
{
    public class CartItemService : ICartItemService
    {
        private readonly IDeletableEntityRepository<CartItem> cartItemRepo;

        public CartItemService(IDeletableEntityRepository<CartItem> cartItemRepo)
        {
            this.cartItemRepo = cartItemRepo;
        }

        public IQueryable<CartItem> GetAllByName()
        {
            return this.cartItemRepo.All();
        }
    }
}