﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.Account;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
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
                return this.RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, item.Description);
            }

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

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login");

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = AutoMapperConfig.MapperInstance.Map<ProfileViewModel>(user);

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = AutoMapperConfig.MapperInstance.Map<UpdateProfileViewModel>(user);

            return this.View(viewModel);
        }


        public async Task<IActionResult> CreateRoles()
        {
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.AdministratorRoleName));
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.UserRoleName));
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.ManagerRoleName));

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddUsersToRoles()
        {
            string adminEmail = GlobalConstants.AdminEmail;
            string managerEmail = GlobalConstants.ManagerEmail;

            var admin = await this.userManager.FindByEmailAsync(adminEmail);
            var manager = await this.userManager.FindByEmailAsync(managerEmail);

            await this.userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
            await this.userManager.AddToRoleAsync(manager, GlobalConstants.ManagerRoleName);

            return this.RedirectToAction("Index", "Home");
        }
    }
}