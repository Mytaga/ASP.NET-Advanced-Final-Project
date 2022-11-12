using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ShopViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IShopService
    {
        Task<IEnumerable<ShopViewModel>> GetAllAsync();

        Task CreateAsync(CreateShopViewModel viewModel);
    }
}
