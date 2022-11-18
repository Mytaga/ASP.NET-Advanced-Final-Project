using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.SaleViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class SaleService : ISaleService
    {
        private readonly IDeletableEntityRepository<Sale> saleRepo;

        public SaleService(IDeletableEntityRepository<Sale> saleRepo)
        {
            this.saleRepo = saleRepo;
        }

        public Task AddSaleAsync(CreateSaleViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<SaleViewModel>> GetAllSalesAsync()
        {
            return await this.saleRepo
                .AllAsNoTracking()
                .Select(s => new SaleViewModel
                {
                    Id = s.Id,
                    Amount = s.Amount.ToString("C"),
                    SaleDate = s.SaleDate,
                    PaymentType = s.PaymentType.ToString(),

                })
                .ToListAsync();
        }
    }
}
