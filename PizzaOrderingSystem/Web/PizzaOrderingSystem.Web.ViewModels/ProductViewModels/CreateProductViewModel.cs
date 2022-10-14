using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<ListProductCategoriesViewModel> Categories { get; set; }
    }
}
