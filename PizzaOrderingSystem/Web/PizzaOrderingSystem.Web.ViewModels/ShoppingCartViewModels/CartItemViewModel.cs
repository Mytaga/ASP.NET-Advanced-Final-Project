using AutoMapper;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;

namespace PizzaOrderingSystem.Web.ViewModels.ShoppingCart
{
    public class CartItemViewModel : IMapFrom<CartItem>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal ItemPrice { get; set; }

        public string ImageUrl { get; set; }

        public decimal Amount => this.ItemPrice * this.Quantity;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CartItem, CartItemViewModel>()
               .ForMember(d => d.ItemName, mo => mo.MapFrom(s => s.Product.Name));
            configuration.CreateMap<CartItem, CartItemViewModel>()
               .ForMember(d => d.ItemPrice, mo => mo.MapFrom(s => s.Product.Price));
            configuration.CreateMap<CartItem, CartItemViewModel>()
               .ForMember(d => d.ImageUrl, mo => mo.MapFrom(s => s.Product.ImageUrl));
        }
    }
}
