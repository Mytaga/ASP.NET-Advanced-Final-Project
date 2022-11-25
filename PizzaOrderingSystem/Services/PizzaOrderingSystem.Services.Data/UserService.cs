using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepo;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await this.userRepo
                .AllAsNoTracking()
                .Where(u => u.Email != GlobalConstants.AdminEmail && u.Email != GlobalConstants.ManagerEmail)
                .CountAsync();
        }
    }
}
