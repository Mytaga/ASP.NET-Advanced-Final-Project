using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepo;
        private readonly ICartService cartService;

        public OrderService(IDeletableEntityRepository<Order> orderRepo, ICartService cartService)
        {
            this.orderRepo = orderRepo;
            this.cartService = cartService;
        }

        public async Task AddAsync(CreateOrderViewModel viewModel)
        {
            Order order = new Order()
            {
                TimeOfOrder = viewModel.TimeOfOrder,
                TotalPrice = viewModel.TotalPrice,
                DeliveryType = viewModel.DeliveryType,
                Status = viewModel.Status,
                PaymentType = viewModel.PaymentType,
                UserId = viewModel.UserId,
                Products = await this.cartService.GetCartProductsAsync(),
            };

            await this.orderRepo.AddAsync(order);
            await this.orderRepo.SaveChangesAsync();
        }

        public async Task<Order> GetLastOrderAsync()
        {
            return await this.orderRepo
                .All()
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefaultAsync();
        }
    }
}
