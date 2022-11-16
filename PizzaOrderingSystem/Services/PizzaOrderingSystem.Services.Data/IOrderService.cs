using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IOrderService
    {
        Task AddAsync(CreateOrderViewModel viewModel);

        Task<Order> GetLastOrderAsync();

        Task<IEnumerable<OrderViewModel>> GetUserOrders(string userId);

        Task<OrderDetailsViewModel> GetUserOrderDetailsAsync(string userId, string prderId);
    }
}
