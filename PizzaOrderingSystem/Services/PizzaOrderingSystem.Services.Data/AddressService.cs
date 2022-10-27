using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class AddressService : IAddressService
    {
        private IDeletableEntityRepository<Address> addressRepo;

        public AddressService(IDeletableEntityRepository<Address> addressRepo)
        {
            this.addressRepo = addressRepo;
        }

        public async Task AddAddress(Address address)
        {
            await this.addressRepo.AddAsync(address);
            await this.addressRepo.SaveChangesAsync();
        }
    }
}
