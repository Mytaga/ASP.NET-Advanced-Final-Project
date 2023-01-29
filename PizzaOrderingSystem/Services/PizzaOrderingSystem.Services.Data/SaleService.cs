using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Helpers;
using PizzaOrderingSystem.Web.ViewModels.Manager.Sales;
using PizzaOrderingSystem.Web.ViewModels.Manager.SaleViewModels;
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
                .Where(s => s.Amount != 0.00M)
                .Select(s => new SaleViewModel
                {
                    Id = s.Id,
                    Amount = s.Amount.ToString() + "лв.",
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

        public async Task<decimal> GetTotalAmountAsync()
        {
            return await this.saleRepo
                .AllAsNoTracking()
                .SumAsync(s => s.Amount);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await this.saleRepo
                .AllAsNoTracking()
                .CountAsync();
        }

        public async Task<SalesInfoViewModel> GetStatisticsAsync()
        {
            var totalAmount = await this.GetTotalAmountAsync();
            var totalCount = await this.GetTotalCountAsync();

            var viewModel = new SalesInfoViewModel
            {
                TotalAmount = totalAmount.ToString() + "лв.",
                TotalCount = totalCount.ToString(),
            };

            return viewModel;
        }

        public async Task<SalesQueryModel> GetQuerySalesAsync(int currentPage = 1, int salesPerPage = 1)
        {
            var result = new SalesQueryModel();

            var sales = await this.saleRepo
                .AllAsNoTracking()
                .Where(s => s.Amount != 0.00M)
                .Select(s => new SaleViewModel
                {
                    Id = s.Id,
                    Amount = s.Amount.ToString() + "лв.",
                    SaleDate = s.SaleDate,
                    PaymentType = s.PaymentType.ToString(),
                })
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();

            result.Sales = sales.Skip((currentPage - 1) * salesPerPage)
                .Take(salesPerPage);

            result.TotalSales = sales.Count();

            return result;
        }
    }
}
