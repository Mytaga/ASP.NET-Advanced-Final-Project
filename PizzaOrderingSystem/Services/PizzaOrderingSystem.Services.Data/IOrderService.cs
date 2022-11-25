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

        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId);

        Task<OrderDetailsViewModel> GetUserOrderDetailsAsync(string userId, string prderId);

        OrderDetailsViewModel GetOrderDetails(Order order);

        Task<int> GetAllOrdersAsync();
    }
}