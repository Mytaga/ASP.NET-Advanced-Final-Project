using PizzaOrderingSystem.Data.Models;
using System.Linq;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICartItemService
    {
        IQueryable<CartItem> GetAllByName();
    }
}
