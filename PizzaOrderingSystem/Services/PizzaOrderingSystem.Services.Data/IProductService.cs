using PizzaOrderingSystem.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IProductService
    {
        IQueryable<Product> GetAllByName(string searchName = "");

        IQueryable<Product> GetAllByCategory(string categoryName = "");

        ICollection<string> GetAllProductsCategories();

        Task<Product> GetByIdАsync(string id);

        Task AddProduct(Product product);

        void DeleteProduct(Product product);

        void EditProduct(Product product);

        bool ExistById(string id);
    }
}