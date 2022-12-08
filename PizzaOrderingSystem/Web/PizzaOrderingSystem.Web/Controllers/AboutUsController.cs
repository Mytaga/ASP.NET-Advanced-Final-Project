using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class AboutUsController : BaseController
    {
        /// <summary>
        /// The action returns static info about the pizza company
        /// </summary>
        /// <returns>
        /// Information about the company
        /// </returns>
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
