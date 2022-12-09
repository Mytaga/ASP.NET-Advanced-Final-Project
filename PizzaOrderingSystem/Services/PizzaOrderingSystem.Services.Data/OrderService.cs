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
        private readonly ISaleService saleService;

        public OrderService(IDeletableEntityRepository<Order> orderRepo, ICartService cartService, ISaleService saleService)
        {
            this.orderRepo = orderRepo;
            this.cartService = cartService;
            this.saleService = saleService;
        }

        public async Task AddAsync(CreateOrderViewModel viewModel)
        {
            Sale sale = new Sale();
            await this.saleService.AddAsync(sale);

            Order order = new Order()
            {
                TimeOfOrder = viewModel.TimeOfOrder,
                TotalPrice = Convert.ToDecimal(viewModel.TotalPrice),
                DeliveryType = viewModel.DeliveryType,
                Status = viewModel.Status,
                PaymentType = viewModel.PaymentType,
                UserId = viewModel.UserId,
                OrderProducts = await this.cartService.GetCartProductsAsync(),
                Sale = sale,
            };

            sale.OrderId = order.Id;

            await this.orderRepo.AddAsync(order);
            await this.orderRepo.SaveChangesAsync();
        }

        public async Task<int> GetAllOrdersAsync()
        {
            return await this.orderRepo.AllAsNoTracking()
                .Where(o => o.Status != PizzaOrderingSystem.Data.Models.Enums.OrderStatus.Canceled)
                .CountAsync();
        }

        public async Task<Order> GetLastOrderAsync()
        {
            return await this.orderRepo
                .All()
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefaultAsync();
        }

        public OrderDetailsViewModel GetOrderDetails(Order order)
        {
            OrderDetailsViewModel viewModel = new OrderDetailsViewModel()
            {
                OrderId = order.Id,
                TimeOfOrder = order.TimeOfOrder.ToString("f", CultureInfo.InvariantCulture),
                TotalPrice = order.TotalPrice.ToString("C"),
                DeliveryType = order.DeliveryType.ToString(),
                Status = order.Status.ToString(),
                PaymentType = order.PaymentType.ToString(),
                Products = order.OrderProducts,
                Recipient = order.User.FirstName + " " + order.User.LastName,
                RecipientPhone = order.User.PhoneNumber,
                RecipientCity = order.User.Address.City,
                RecipientStreet = order.User.Address.Street,
                RecipientStreetNumber = order.User.Address.StreetNumber.ToString(),
                RecipientPostalCode = order.User.Address.PostCode.ToString(),
            };

            return viewModel;
        }

        public CreateOrderViewModel GetOrderView(ApplicationUser user)
        {
            CreateOrderViewModel viewModel = new CreateOrderViewModel()
            {
                TotalPrice = this.cartService.GetShoppingCartTotal().ToString("F"),
                UserId = user.Id,
                Cards = user.CreditCards,
                City = user.Address.City,
                Street = user.Address.Street,
                StreetNumber = user.Address.StreetNumber,
                Floor = user.Address.Floor,
            };

            return viewModel;
        }

        public async Task<OrderDetailsViewModel> GetUserOrderDetailsAsync(string userId, string orderId)
        {
            var order = await this.orderRepo.All().FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId);

            var model = new OrderDetailsViewModel()
            {
                OrderId = orderId,
                TimeOfOrder = order.TimeOfOrder.ToString("f", CultureInfo.InvariantCulture),
                TotalPrice = order.TotalPrice.ToString("C"),
                DeliveryType = order.DeliveryType.ToString(),
                PaymentType = order.PaymentType.ToString(),
                Status = order.Status.ToString(),
                Products = order.OrderProducts,
            };

            return model;
        }

        public async Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId)
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