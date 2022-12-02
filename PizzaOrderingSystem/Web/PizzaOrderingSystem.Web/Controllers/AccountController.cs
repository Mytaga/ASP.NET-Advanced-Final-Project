using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.Account;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserService userService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IWebHostEnvironment webHostEnvironment,
            IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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
                UserName = model.Username,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);
                await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
            }

            foreach (var item in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, item.Description);
            }

            TempData["message"] = "Congratulations you have successfully registered as a user!";

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            };

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await this.signInManager.PasswordSignInAsync(user, model.Password, true, false);

                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return this.Redirect(model.ReturnUrl);
                    }

                    if (user.Email == GlobalConstants.AdminEmail)
                    {
                        return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController, new { area = GlobalConstants.AdministrationArea });
                    }
                    else if (user.Email == GlobalConstants.ManagerEmail)
                    {
                        return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController, new { area = GlobalConstants.ManagerArea });
                    }

                    return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
                }
            }

            this.ModelState.AddModelError(string.Empty, ErrorConstants.InvalidLogin);

            TempData["message"] = "You have successfully logged in!";

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            TempData["message"] = "You have successfully logged out!";

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> ViewProfile()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.userService.GetUser(user);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new UpdateProfileViewModel()
            {
                PhoneNumber = user.PhoneNumber,
            };

            if (user.Address != null)
            {
                viewModel.City = user.Address.City;
                viewModel.Street = user.Address.Street;
                viewModel.StreetNumber = user.Address.StreetNumber;
                viewModel.Floor = user.Address.Floor;
                viewModel.PostCode = user.Address.PostCode;
            }           

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateProfileViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            string uniqueFileName = this.UploadFile(model);

            var address = new Address()
            {
                City = model.City,
                Street = model.Street,
                StreetNumber = model.StreetNumber,
                Floor = model.Floor,
                PostCode = model.PostCode,
                User = user,
            };

            user.PhoneNumber = model.PhoneNumber;

            if (uniqueFileName != null)
            {
                user.ImageUrl = uniqueFileName;
            }

            user.Address = address;

            await this.userManager.UpdateAsync(user);

            TempData["message"] = "You have successfully updated your user info!";

            return this.RedirectToAction(GlobalConstants.ViewProfileAction, GlobalConstants.AccountController);
        }

        public async Task<IActionResult> CreateRoles()
        {
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.AdministratorRoleName));
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.UserRoleName));
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.ManagerRoleName));

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
        }

        public async Task<IActionResult> AddUsersToRoles()
        {
            string adminEmail = GlobalConstants.AdminEmail;
            string managerEmail = GlobalConstants.ManagerEmail;

            var admin = await this.userManager.FindByEmailAsync(adminEmail);
            var manager = await this.userManager.FindByEmailAsync(managerEmail);

            await this.userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
            await this.userManager.AddToRoleAsync(manager, GlobalConstants.ManagerRoleName);

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
        }

        private string UploadFile(UpdateProfileViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageUrl != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}