using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<CartItem> GetByIdАsync(string id)
        {
            return await this.cartItemRepo.All().FirstOrDefaultAsync(ci => ci.Id == id);
        }
    }
}