using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class AllProductsViewModel
    {
        public ICollection<ListAllProductsViewModel> Products { get; set; }

        public string SearchQuery { get; set; }
    }
}