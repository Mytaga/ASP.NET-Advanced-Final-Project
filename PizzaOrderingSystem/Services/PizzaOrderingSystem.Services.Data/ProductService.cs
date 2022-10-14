﻿using PizzaOrderingSystem.Data.Common.Repositories;
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

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void EditProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool ExistById(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAllByCategory(string categoryName = "")
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAllByName(string searchName = EmptyString)
        {
            if (searchName != null)
            {
                return this.productRepo.AllAsNoTracking()
                    .Where(p => p.Name.ToLower().Contains(searchName.ToLower()) && p.IsDeleted == false);
            }

            return this.productRepo.AllAsNoTracking();
        }

        public ICollection<string> GetAllProductsCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdАsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
