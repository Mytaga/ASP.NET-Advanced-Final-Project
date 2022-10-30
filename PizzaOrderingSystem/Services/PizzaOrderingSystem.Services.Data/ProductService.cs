using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public async Task AddProduct(Product product)
        {
            await this.productRepo.AddAsync(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            this.productRepo.Delete(product);
            await this.productRepo.SaveChangesAsync();
        }

        public async Task EditProduct(Product product)
        {
            this.productRepo.Update(product);
            await this.productRepo.SaveChangesAsync();
        }

        public bool ExistById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<AllProductsViewModel> GetAllByCategory(string categoryName = EmptyString, string searchName = EmptyString)
        {
            var products = this.productRepo.AllAsNoTracking()
                .Where(p => p.Category.Name == categoryName && p.IsDeleted == false);

            if (searchName != null)
            {
                products = this.productRepo.AllAsNoTracking().Where(p => p.Category.Name == categoryName &&
                p.Name.ToLower().StartsWith(searchName.ToLower()) && p.IsDeleted == false);
            }

            AllProductsViewModel viewModel = new AllProductsViewModel()
            {
                Products = await products.To<ListAllProductsViewModel>().ToListAsync(),
                SearchQuery = searchName,
            };

            return viewModel;
        }

        public async Task<AllProductsViewModel> GetAllByName(string searchName = EmptyString)
        {
            var products = this.productRepo.AllAsNoTracking();

            if (searchName != null)
            {
                products = this.productRepo.AllAsNoTracking().Where(p => p.Name.ToLower().StartsWith(searchName.ToLower()) && p.IsDeleted == false);
            }

            AllProductsViewModel viewModel = new AllProductsViewModel()
            {
                Products = await products.To<ListAllProductsViewModel>().ToListAsync(),
                SearchQuery = searchName,
            };

            return viewModel;
        }

        public ICollection<string> GetAllProductsCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdАsync(string id)
        {
            return await this.productRepo.All()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Product GetById(string id)
        {
            return this.productRepo
               .All()
               .FirstOrDefault(f => f.Id == id);
        }
    }
}