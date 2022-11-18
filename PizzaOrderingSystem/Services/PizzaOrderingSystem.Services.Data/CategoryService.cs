using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepo;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<ListProductCategoriesViewModel>> AllAsync()
        {
            return await this.categoryRepo.AllAsNoTracking().Select(c => new ListProductCategoriesViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync();
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await this.categoryRepo
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == name) != null;
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            return await this.categoryRepo
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id) != null;
        }

        public async Task AddCategoryAsync(CreateCategoryInputModel model)
        {
            var category = AutoMapperConfig.MapperInstance.Map<Category>(model);

            await this.categoryRepo.AddAsync(category);
            await this.categoryRepo.SaveChangesAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await this.categoryRepo
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
