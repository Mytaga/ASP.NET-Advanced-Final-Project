using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepo;

        public OrderService(IDeletableEntityRepository<Order> orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        public async Task AddAsync(CreateOrderViewModel viewModel)
        {
            Order order = new Order()
            {
                TimeOfOrder = viewModel.TimeOfOrder,
                TotalPrice = decimal.Parse(viewModel.TotalPrice),
                DeliveryType = viewModel.DeliveryType,
                Status = viewModel.Status,
                PaymentType = viewModel.PaymentType,
                UserId = viewModel.UserId,
            };

            await this.orderRepo.AddAsync(order);
            await this.orderRepo.SaveChangesAsync();
        }
    }
}
