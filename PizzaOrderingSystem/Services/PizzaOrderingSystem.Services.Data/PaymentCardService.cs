using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class PaymentCardService : IPaymentCardService
    {
        private readonly IDeletableEntityRepository<CreditCard> creditCardRepo;

        public PaymentCardService(IDeletableEntityRepository<CreditCard> creditCardRepo)
        {
            this.creditCardRepo = creditCardRepo;
        }

        public async Task Add(AddCardViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(CreditCard card)
        {
            throw new NotImplementedException();
        }

        public async Task<AllCardsViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
