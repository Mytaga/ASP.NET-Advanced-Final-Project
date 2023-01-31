using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class ProductsQueryModel
    {
        public int TotalProducts { get; set; }

        public IEnumerable<ListAllProductsViewModel> Products { get; set; } = new List<ListAllProductsViewModel>();
    }
}
