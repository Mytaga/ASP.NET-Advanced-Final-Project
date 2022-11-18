using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICategoryService
    {
        Task<IEnumerable<ListProductCategoriesViewModel>> AllAsync();

        Task<bool> ExistByNameAsync(string name);

        Task<bool> ExistByIdAsync(int id);

        Task AddCategoryAsync(CreateCategoryInputModel model);

        Task<Category> GetByIdAsync(int id);
    }
}