using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IPaymentCardService
    {
        Task AddAsync(AddCardViewModel viewModel);

        Task<AddCardViewModel> GetAll();

        Task Delete(CreditCard card);
    }
}
