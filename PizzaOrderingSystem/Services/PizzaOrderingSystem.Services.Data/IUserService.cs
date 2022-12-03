using PizzaOrderingSystem.Data;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.Account;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IUserService
    {
        Task<int> GetUsersCountAsync();

        ProfileViewModel GetUser(ApplicationUser user);

        UpdateProfileViewModel GetUpdateProfileView(ApplicationUser user);
    }
}
