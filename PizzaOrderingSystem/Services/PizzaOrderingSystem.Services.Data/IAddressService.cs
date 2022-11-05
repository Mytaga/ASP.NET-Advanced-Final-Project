using PizzaOrderingSystem.Data.Models;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IAddressService
    {
        Task AddAddress(Address address);
    }
}
