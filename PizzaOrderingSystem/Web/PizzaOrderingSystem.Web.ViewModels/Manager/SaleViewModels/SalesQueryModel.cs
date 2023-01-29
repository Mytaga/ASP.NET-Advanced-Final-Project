using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.ViewModels.Manager.SaleViewModels
{
    public class SalesQueryModel
    {
        public int TotalSales { get; set; }

        public IEnumerable<SaleViewModel> Sales { get; set; } = new List<SaleViewModel>();
    }
}
