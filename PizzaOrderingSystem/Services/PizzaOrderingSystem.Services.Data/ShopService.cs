using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ShopViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class ShopService : IShopService
    {
        private readonly IDeletableEntityRepository<Shop> shopRepo;

        public ShopService(IDeletableEntityRepository<Shop> shopRepo)
        {
            this.shopRepo = shopRepo;
        }

        public async Task CreateAsync(CreateShopViewModel viewModel)
        {
            var adress = new Address()
            {
                City = viewModel.City,
                Street = viewModel.Street,
                StreetNumber = viewModel.StreetNumber,
            };

            Shop shop = new Shop()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                PhoneNumber = viewModel.PhoneNumber,
                Address = adress,
            };

            await this.shopRepo.AddAsync(shop);
            await this.shopRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShopViewModel>> GetAllAsync()
        {
            return await this.shopRepo
                .AllAsNoTracking().Select(s => new ShopViewModel
                {
                Name = s.Name,
                Description = s.Description,
                PhoneNumber = s.PhoneNumber,
                City = s.Address.City,
                Street = s.Address.Street,
                StreetNumber = s.Address.StreetNumber,
                })
                .ToListAsync();
        }
    }
}
