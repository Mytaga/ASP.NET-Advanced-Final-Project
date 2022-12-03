using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.Account;
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

        public UpdateProfileViewModel GetUpdateProfileView(ApplicationUser user)
        {
            var viewModel = new UpdateProfileViewModel()
            {
                PhoneNumber = user.PhoneNumber,
            };

            if (user.Address != null)
            {
                viewModel.City = user.Address.City;
                viewModel.Street = user.Address.Street;
                viewModel.StreetNumber = user.Address.StreetNumber;
                viewModel.Floor = user.Address.Floor;
                viewModel.PostCode = user.Address.PostCode;
            }

            return viewModel;
        }

        public ProfileViewModel GetUser(ApplicationUser user)
        {
            var viewModel = new ProfileViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                PhoneNumber = user.PhoneNumber,
            };

            if (user.Address != null)
            {
                viewModel.City = user.Address.City;
                viewModel.Street = user.Address.Street;
                viewModel.StreetNumber = user.Address.StreetNumber;
                viewModel.Floor = user.Address.Floor;
                viewModel.PostCode = user.Address.PostCode;
            }

            return viewModel;
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
