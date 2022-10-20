using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class AboutUsController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
