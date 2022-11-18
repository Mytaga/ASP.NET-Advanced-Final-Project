using PizzaOrderingSystem.Web.ViewModels.SaleViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleViewModel>> GetAllSalesAsync();
        Task AddSaleAsync(CreateSaleViewModel viewModel);
    }
}
