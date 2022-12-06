﻿using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.Manager.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleViewModel>> GetAllSalesAsync();

        Task AddAsync(Sale sale);

        Task UpdateAsync();
    }
}
