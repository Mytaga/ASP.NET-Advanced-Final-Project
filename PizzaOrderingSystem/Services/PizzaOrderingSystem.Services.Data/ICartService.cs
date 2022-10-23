using PizzaOrderingSystem.Data.Models;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICartService
    {
        Task AddToCartAsync(Product product, int quantity);

        Task RemoveFromCartAsync(Product product);
    }
}
