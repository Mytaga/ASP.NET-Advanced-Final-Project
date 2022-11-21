namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaOrderingSystem.Services.Data;
    using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
    using PizzaOrderingSystem.Web.ViewModels.Manager.Dashboard;

    public class DashboardController : ManagerController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            return this.View(viewModel);
        }
    }
}
