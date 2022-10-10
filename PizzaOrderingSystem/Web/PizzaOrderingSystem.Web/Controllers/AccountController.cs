using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.Account;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class AccountController : Controller
    { 
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                EmailConfirmed = true,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.UserName,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);
                return this.RedirectToAction("Index", "Home");
            }

            this.ModelState.AddModelError(null, "Something went wrong");

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            return this.Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            return this.Ok();
        }
    }
}
