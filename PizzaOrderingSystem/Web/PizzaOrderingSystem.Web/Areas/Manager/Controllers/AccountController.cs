using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.Areas.Administration.Controllers;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Areas.Manager.Controllers
{
    public class AccountController : ManagerController
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.Logout;

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController, new { area = string.Empty });
        }
    }
}