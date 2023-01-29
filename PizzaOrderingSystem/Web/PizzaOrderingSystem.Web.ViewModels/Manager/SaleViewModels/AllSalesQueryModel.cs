using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.ViewModels.Manager.SaleViewModels
{
    public class AllSalesQueryModel
    {
        public const int SalesPerPage = 8;

        public int CurrentPage { get; set; } = 1;

        public int TotalSalesCount { get; set; }
        
        public IEnumerable<SaleViewModel> Sales { get; set; } = new List<SaleViewModel>();
    }
}
