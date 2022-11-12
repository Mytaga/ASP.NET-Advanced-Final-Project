using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IProductService
    {
        Task<AllProductsViewModel> GetAllByName(string searchName = "");

        Task<AllProductsViewModel> GetAllByCategory(string categoryName = "", string searchName = "");

        ICollection<string> GetAllProductsCategories();

        Task<Product> GetByIdАsync(string id);

        Product GetById(string id);

        Task AddProduct(Product product);

        Task DeleteProduct(Product product);

        Task EditProduct(Product product);

        bool ExistById(string id);
    }
}