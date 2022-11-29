namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaOrderingSystem.Common;
    using PizzaOrderingSystem.Services.Data;
    using System.Threading.Tasks;

    public class SaleController : ManagerController
    {
        private readonly ISaleService saleService;

        public SaleController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.saleService.GetAllSalesAsync();
            return this.View(viewModel);
        }

        public async Task<IActionResult> UpdateSales()
        {
            await this.saleService.UpdateAsync();
            return this.RedirectToAction(GlobalConstants.IndexAction);
        }
    }
}