using PizzaOrderingSystem.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItem>> GetAllAsync();

        Task<IEnumerable<CartItem>> GetAllByOrderAsync(string orderId);

        Task<CartItem> GetByIdАsync(string id);
    }
}