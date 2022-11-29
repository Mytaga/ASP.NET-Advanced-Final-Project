using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
using PizzaOrderingSystem.Web.ViewModels;
using System.Diagnostics;

namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    public class HomeController : ManagerController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}