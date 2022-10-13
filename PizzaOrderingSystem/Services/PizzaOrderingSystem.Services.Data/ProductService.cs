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

        public Task AddProduct(Product product)
        {
            throw new NotImplementedException();
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

        public IQueryable<Product> GetAllByName(string searchName = "")
        {
            throw new NotImplementedException();
        }

        public ICollection<string> GetAllFurnitureCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdАsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
