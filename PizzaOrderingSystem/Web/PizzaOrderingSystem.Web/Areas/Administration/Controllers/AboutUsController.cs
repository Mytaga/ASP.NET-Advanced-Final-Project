using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaOrderingSystem.Web.Areas.Administration.Controllers
{
    public class AboutUsController : AdministrationController
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
