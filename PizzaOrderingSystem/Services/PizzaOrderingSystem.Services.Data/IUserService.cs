using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IUserService
    {
        Task<int> GetUsersCountAsync();
    }
}
