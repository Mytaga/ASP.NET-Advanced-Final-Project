namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SaleController : ManagerController
    {
        public SaleController()
        {

        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
