using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
using PizzaOrderingSystem.Web.ViewModels;
using PizzaOrderingSystem.Web.ViewModels.Manager.Home;
using System.Diagnostics;

namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    public class HomeController : ManagerController
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            return this.View(viewModel);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            this.logger.LogError(feature.Error, "TraceIdentifier: {0}", HttpContext.TraceIdentifier);

            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}