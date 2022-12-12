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
        private readonly ICategoryService categoryService;

        public ProductService(IDeletableEntityRepository<Product> productRepo, ICategoryService categoryService)
        {
            this.productRepo = productRepo;
            this.categoryService = categoryService;
        }

        public async Task AddProductAsync(CreateProductInputModel model, string imageUrl)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = imageUrl,
                CategoryId = model.CategoryId,
            };

            await this.productRepo.AddAsync(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            this.productRepo.Delete(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task EditProductAsync(EditProductInputModel model, string id, string imageUrl)
        {
            Product product = await this.productRepo.All().FirstOrDefaultAsync(p => p.Id == id);

            Category category = await this.categoryService.GetByIdAsync(model.CategoryId);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Category = category;

            if (imageUrl != "")
            {
                product.ImageUrl = imageUrl;
            }

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
                Products = await products.Select(p => new ListAllProductsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                })
                .OrderByDescending(p => p.Price)
                .ToListAsync(),
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
                Products = await products.Select(p => new ListAllProductsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                })
                .OrderByDescending(p => p.Price)
                .ToListAsync(),
                SearchQuery = searchName,
            };

            return viewModel;
        }

		public async Task<int> GetAllProductsCountAsync()
		{
            return await this.productRepo
                .AllAsNoTracking()
                .CountAsync();
		}

		public async Task<Product> GetByIdАsync(string id)
        {
            return await this.productRepo.All()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<EditProductViewModel> GetEditModel(Product product)
        {
            var allCategories = await
               this.categoryService.AllAsync();

            EditProductViewModel viewModel = new EditProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Categories = allCategories,
            };

            return viewModel;
        }

        public DetailsProductViewModel GetProductDetails(Product product)
        {
            DetailsProductViewModel viewModel = new DetailsProductViewModel()
            {
                Name= product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryName = product.Category.Name,
            };

            return viewModel;
        }
    }
}