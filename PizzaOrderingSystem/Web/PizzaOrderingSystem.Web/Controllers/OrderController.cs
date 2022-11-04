using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Data;
using PizzaOrderingSystem.Web.ViewModels.OrderViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;

        public OrderController(UserManager<ApplicationUser> userManager, ICartService cartService)
        {
            this.userManager = userManager;
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Confirm()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
 
            var user = await this.userManager.FindByIdAsync(userId);

            CreateOrderViewModel viewModel = new CreateOrderViewModel()
            {
                TotalPrice = this.cartService.GetShoppingCartTotal(),
                UserId = userId,
                Cards = user.CreditCards,
                City = user.Address.City,
                Street = user.Address.Street,
                StreetNumber = user.Address.StreetNumber,
                Floor = user.Address.Floor,
                ShopId = null,
            };

            return this.View(viewModel);
        }
    }
}
