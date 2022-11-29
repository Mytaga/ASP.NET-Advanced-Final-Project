using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Web.Areas.Administration.Controllers;

namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    public class AboutUsController : ManagerController
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
