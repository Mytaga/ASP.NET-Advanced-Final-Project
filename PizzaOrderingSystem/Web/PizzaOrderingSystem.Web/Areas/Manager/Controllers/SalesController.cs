namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaOrderingSystem.Services.Data;
    using PizzaOrderingSystem.Web.ViewModels.Administration.Dashboard;

    public class SalesController : ManagerController
    {
        private readonly ISettingsService settingsService;

        public SalesController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
