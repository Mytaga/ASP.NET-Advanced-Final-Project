using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;

namespace PizzaOrderingSystem.Services.Data
{
    public class SaleService : ISaleService
    {
        private readonly IDeletableEntityRepository<Sale> saleRepo;

        public SaleService(IDeletableEntityRepository<Sale> saleRepo)
        {
            this.saleRepo = saleRepo;
        }
    }
}
