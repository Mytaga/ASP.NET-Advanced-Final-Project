using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
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

        public async Task AddAsync(AddCardViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(CreditCard card)
        {
            throw new NotImplementedException();
        }

        public async Task<AddCardViewModel> GetAll()
        {
            var cards = this.creditCardRepo.AllAsNoTracking();

            AddCardViewModel viewModel = new AddCardViewModel
            {
                SavedCards = await cards.To<PaymentCardViewModel>().ToListAsync(),
            };

            return viewModel;
        }
    }
}
