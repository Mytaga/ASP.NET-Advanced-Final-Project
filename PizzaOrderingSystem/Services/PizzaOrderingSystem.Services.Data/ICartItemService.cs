using PizzaOrderingSystem.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICartItemService
    {
        IQueryable<CartItem> GetAll();

        IQueryable<CartItem> GetAllByOrder(string orderId);

        Task<CartItem> GetByIdАsync(string id);
    }
}