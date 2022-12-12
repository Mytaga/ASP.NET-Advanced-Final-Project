using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Creates a register view model
        /// </summary>
        /// <returns>
        /// Register form view
        /// </returns>

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return this.View(model);
        }

        /// <summary>
        /// Retrieves data from the register form and creates application user in DB. 
        /// Automatically sets a User role if the email is not : admin or manager. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// Login form view
        /// </returns>
        /// 
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
                if (user.Email != GlobalConstants.AdminEmail && user.Email != GlobalConstants.ManagerEmail)
                {
                    await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                }
              
                TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.RegisterUser;
                return this.RedirectToAction(GlobalConstants.LoginAction);
            }

            foreach (var item in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, item.Description);
            }

            TempData[GlobalConstants.TempDataError] = ErrorConstants.RegisterFail;

            return this.View(model);
        }

        /// <summary>
        /// Creates a login view model
        /// </summary>
        /// <returns>
        /// Login form view
        /// </returns>

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

        /// <summary>
        /// Checks if the user email is registered in the database /authentication/.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// Home page view if login is successfull. Based on user role redirects to a specific area.
        /// </returns> 

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

            return this.View(model);
        }

        /// <summary>
        /// Logs out the user from the application.
        /// </summary>
        /// <returns>
        /// Home page view for unauthenticated users.
        /// </returns>
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.Logout;

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
        }

        /// <summary>
        /// Creates user details view model
        /// </summary>
        /// <returns>
        /// User details page with form with info
        /// </returns>

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

            var viewModel = this.userService.GetUpdateProfileView(user);

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

            string uniqueFileName = await this.UploadPhoto(model.ImageUrl);

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

            if (uniqueFileName != "")
            {
                user.ImageUrl = uniqueFileName;
            }

            user.Address = address;

            await this.userManager.UpdateAsync(user);

            TempData[GlobalConstants.TempDataSuccess] = SuccessConstants.UpdatedUser;

            return this.RedirectToAction(GlobalConstants.ViewProfileAction, GlobalConstants.AccountController);
        }


        [AllowAnonymous]
        public async Task<IActionResult> CreateRoles()
        {
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.AdministratorRoleName));
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.UserRoleName));
            await this.roleManager.CreateAsync(new ApplicationRole(GlobalConstants.ManagerRoleName));

            return this.RedirectToAction(GlobalConstants.IndexAction, GlobalConstants.HomeController);
        }

        [AllowAnonymous]
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

        //private string UploadFile(IFormFile imageUrl)
        //{
        //    string uniqueFileName = null;

        //    if (imageUrl != null)
        //    {
        //        string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "img");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + imageUrl.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            imageUrl.CopyTo(fileStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}
    }
}