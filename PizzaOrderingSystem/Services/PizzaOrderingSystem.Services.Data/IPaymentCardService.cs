using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IPaymentCardService
    {
        Task AddAsync(AddCardViewModel viewModel);

        Task<AddCardViewModel> GetAllАsync(string userId);

        Task<CreditCard> GetByIdAsync(string id);

        Task Delete(CreditCard card);
    }
}