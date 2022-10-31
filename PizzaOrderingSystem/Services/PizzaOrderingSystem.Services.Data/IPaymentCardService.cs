using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IPaymentCardService
    {
        Task Add(AddCardViewModel viewModel);

        Task<AllCardsViewModel> GetAll();

        Task Delete(CreditCard card);
    }
}
