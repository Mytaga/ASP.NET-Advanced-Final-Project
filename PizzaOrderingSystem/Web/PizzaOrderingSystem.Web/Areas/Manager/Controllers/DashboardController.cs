namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaOrderingSystem.Services.Data;
    using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
    using PizzaOrderingSystem.Web.ViewModels.Manager.Dashboard;

    public class DashboardController : ManagerController
    {    
        public DashboardController()
        {
            
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            return this.View(viewModel);
        }
    }
}
