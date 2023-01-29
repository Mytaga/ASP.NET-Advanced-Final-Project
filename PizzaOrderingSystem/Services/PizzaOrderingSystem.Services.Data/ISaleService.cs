using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Helpers;
using PizzaOrderingSystem.Web.ViewModels.Manager.Sales;
using PizzaOrderingSystem.Web.ViewModels.Manager.SaleViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleViewModel>> GetAllSalesAsync();

        Task<SalesQueryModel> GetQuerySalesAsync(int currentPage = 1, int salesPerPage = 1);

        Task AddAsync(Sale sale);

        Task UpdateAsync();

        Task<decimal> GetTotalAmountAsync();

        Task<int> GetTotalCountAsync();

        Task<SalesInfoViewModel> GetStatisticsAsync();
    }
}
