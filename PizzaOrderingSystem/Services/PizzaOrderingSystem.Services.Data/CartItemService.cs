 using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using System.Collections.Generic;
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

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await this.cartItemRepo.AllAsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CartItem>> GetAllByOrderAsync(string orderId)
        {
            return await this.cartItemRepo.AllAsNoTracking().Where(c => c.OrderId == orderId).ToListAsync();
        }

        public async Task<CartItem> GetByIdАsync(string id)
        {
            return await this.cartItemRepo.All().FirstOrDefaultAsync(ci => ci.Id == id);
        }
    }
}