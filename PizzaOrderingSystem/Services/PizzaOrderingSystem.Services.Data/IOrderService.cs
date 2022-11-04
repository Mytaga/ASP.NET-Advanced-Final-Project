using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IOrderService
    {
        Task AddAsync(CreateOrderViewModel viewModel);
    }
}
