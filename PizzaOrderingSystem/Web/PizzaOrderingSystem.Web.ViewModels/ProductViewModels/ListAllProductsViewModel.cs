using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class ListAllProductsViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
