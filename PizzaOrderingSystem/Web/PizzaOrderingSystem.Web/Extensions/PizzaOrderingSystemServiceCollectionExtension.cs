using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data;
using PizzaOrderingSystem.Data.Common;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Data.Repositories;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Services.Exceptions;
using PizzaOrderingSystem.Services.Messaging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PizzaOrderingSystemServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(
               serviceProvider => new SendGridEmailSender(GlobalConstants.SendGridApiKey));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IPaymentCardService, PaymentCardService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IGuard, Guard>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped(sp => ShoppingCart.GetCart(sp));

            return services;
        }
    }
}
