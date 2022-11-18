using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class ProductService : IProductService
    {
        private const string EmptyString = "";
        private readonly IDeletableEntityRepository<Product> productRepo;

        public ProductService(IDeletableEntityRepository<Product> productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task AddProductAsync(Product product)
        {
            await this.productRepo.AddAsync(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            this.productRepo.Delete(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task EditProductAsync(Product product)
        {
            this.productRepo.Update(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task<AllProductsViewModel> GetAllByCategoryAsync(string categoryName = EmptyString, string searchName = EmptyString)
        {
            var products = this.productRepo.AllAsNoTracking()
                .Where(p => p.Category.Name == categoryName);

            if (searchName != null)
            {
                products = this.productRepo
                    .AllAsNoTracking().Where(p => p.Category.Name == categoryName &&
                p.Name.ToLower().StartsWith(searchName.ToLower()) && p.IsDeleted == false)
                    .OrderByDescending(p => p.CreatedOn);
            }

            AllProductsViewModel viewModel = new AllProductsViewModel()
            {
                Products = await products.To<ListAllProductsViewModel>().OrderByDescending(p => p.Price).ToListAsync(),
                SearchQuery = searchName,
            };

            return viewModel;
        }

        public async Task<AllProductsViewModel> GetAllByNameAsync(string searchName = EmptyString)
        {
            var products = this.productRepo.AllAsNoTracking();

            if (searchName != null)
            {
                products = this.productRepo
                    .AllAsNoTracking().Where(p => p.Name.ToLower().StartsWith(searchName.ToLower()) && p.IsDeleted == false)
                    .OrderByDescending(p => p.CreatedOn);
            }

            AllProductsViewModel viewModel = new AllProductsViewModel()
            {
                Products = await products.To<ListAllProductsViewModel>().OrderByDescending(p => p.Price).ToListAsync(),
                SearchQuery = searchName,
            };

            return viewModel;
        }

        public async Task<Product> GetByIdАsync(string id)
        {
            return await this.productRepo.All()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public DetailsProductViewModel GetProductDetails(Product product)
        {
            DetailsProductViewModel viewModel = AutoMapperConfig.MapperInstance.Map<DetailsProductViewModel>(product);

            return viewModel;
        }
    }
}