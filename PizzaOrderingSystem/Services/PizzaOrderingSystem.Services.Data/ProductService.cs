using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using System;
using System.Collections.Generic;
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

        public IQueryable<Product> GetAllByCategory(string categoryName = "")
        {
            return this.productRepo.AllAsNoTracking()
                .Where(p => p.Category.Name == categoryName && p.IsDeleted == false);
        }

        public IQueryable<Product> GetAllByName(string searchName = EmptyString)
        {
            if (searchName != null)
            {
                return this.productRepo.AllAsNoTracking()
                    .Where(p => p.Name.ToLower().Contains(searchName.ToLower()) && p.IsDeleted == false);
            }

            return this.productRepo.All();
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