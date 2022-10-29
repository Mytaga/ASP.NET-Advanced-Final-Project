using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PizzaOrderingSystem.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepo;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public IQueryable<Category> All()
        {
            return this.categoryRepo.AllAsNoTracking();
        }

        public bool ExistByName(string name)
        {
            return this.categoryRepo
                .AllAsNoTracking()
                .FirstOrDefault(c => c.Name == name) != null;
        }

        public bool ExistById(int id)
        {
            return this.categoryRepo
            .AllAsNoTracking()
                .FirstOrDefault(c => c.Id == id) != null;
        }

        public async Task AddCategory(CreateCategoryInputModel model)
        {
            var category = AutoMapperConfig.MapperInstance.Map<Category>(model);

            await this.categoryRepo.AddAsync(category);
            await this.categoryRepo.SaveChangesAsync();
        }

        public Category GetById(int id)
        {
            return this.categoryRepo
                .All()
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await this.categoryRepo
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
