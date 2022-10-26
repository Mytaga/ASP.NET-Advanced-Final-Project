using Microsoft.AspNetCore.Mvc;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class OrderController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
