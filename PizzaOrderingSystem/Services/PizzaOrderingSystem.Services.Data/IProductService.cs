using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IProductService
    {
        Task<AllProductsViewModel> GetAllByNameAsync(string searchName = "");

        Task<AllProductsViewModel> GetAllByCategoryAsync(string categoryName = "", string searchName = "");

        Task<Product> GetByIdАsync(string id);

        Task AddProductAsync(CreateProductInputModel product, string imageUrl);

        Task DeleteProductAsync(Product product);

        Task EditProductAsync(EditProductInputModel model, string id, string imageUrl);

        DetailsProductViewModel GetProductDetails(Product product);

        Task<int> GetAllProductsCountAsync();

        Task<EditProductViewModel> GetEditModel(Product product);
    }
}