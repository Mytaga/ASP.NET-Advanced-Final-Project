using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                TotalPrice = Convert.ToDecimal(viewModel.TotalPrice),
                DeliveryType = viewModel.DeliveryType,
                Status = viewModel.Status,
                PaymentType = viewModel.PaymentType,
                UserId = viewModel.UserId,
                OrderProducts = await this.cartService.GetCartProductsAsync(),
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

        public async Task<OrderDetailsViewModel> GetUserOrderDetailsAsync(string userId, string orderId)
        {
            return await this.orderRepo
                .All()
                .Where(o => o.UserId == userId && o.Id == orderId)
                .Select(o => new OrderDetailsViewModel
                {
                    OrderId = orderId,
                    TimeOfOrder = o.TimeOfOrder.ToString("f", CultureInfo.InvariantCulture),
                    TotalPrice = o.TotalPrice.ToString("C"),
                    DeliveryType = o.DeliveryType.ToString(),
                    PaymentType = o.PaymentType.ToString(),
                    Status = o.Status.ToString(),
                    Products = o.OrderProducts,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetUserOrders(string userId)
        {
            return await this.orderRepo
                .All()
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedOn)
                .Select(o => new OrderViewModel
                {
                    TimeOfOrder = o.TimeOfOrder.ToString("f", CultureInfo.InvariantCulture),
                    TotalPrice = o.TotalPrice.ToString("C"),
                    DeliveryType = o.DeliveryType.ToString(),
                    Status = o.Status.ToString(),
                    OrderId = o.Id,
                })
                .ToListAsync();
        }
    }
}