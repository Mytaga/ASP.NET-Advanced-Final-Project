using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalProductsCount { get; set; }

        public IEnumerable<ListAllProductsViewModel> Products { get; set; }

        public string SearchQuery { get; set; }
    }
}
