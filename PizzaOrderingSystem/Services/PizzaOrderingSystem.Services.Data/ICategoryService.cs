using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ICategoryService
    {
        IQueryable<Category> All();

        bool ExistByName(string name);

        bool ExistById(int id);

        Task AddCategory(CreateCategoryInputModel model);

        Category GetById(int id);

        Task<Category> GetByIdAsync(int id);
    }
}