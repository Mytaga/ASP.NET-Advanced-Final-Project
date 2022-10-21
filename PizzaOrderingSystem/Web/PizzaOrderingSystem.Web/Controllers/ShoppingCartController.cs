using Microsoft.AspNetCore.Mvc;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
