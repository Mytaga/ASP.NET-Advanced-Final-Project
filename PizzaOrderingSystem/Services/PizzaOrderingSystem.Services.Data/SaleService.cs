using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.SaleViewModels;
using System;
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

        public async Task UpdateAsync()
        {
            var sales = await this.saleRepo.All().Where(s => s.Amount == 0.00M).ToListAsync();

            foreach (var sale in sales)
            {
                sale.Amount = sale.Order.TotalPrice;
                sale.SaleDate = sale.Order.TimeOfOrder;
                sale.PaymentType = sale.Order.PaymentType;
                this.saleRepo.Update(sale);
            }

            await this.saleRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<SaleViewModel>> GetAllSalesAsync()
        {
            return await this.saleRepo
                .AllAsNoTracking()
                .Where(s => s.ModifiedOn != null)
                .Select(s => new SaleViewModel
                {
                    Id = s.Id,
                    Amount = s.Amount.ToString("C"),
                    SaleDate = s.SaleDate,
                    PaymentType = s.PaymentType.ToString(),
                })
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task AddAsync(Sale sale)
        {
            await this.saleRepo.AddAsync(sale);
            await this.saleRepo.SaveChangesAsync();
        }
    }
}
