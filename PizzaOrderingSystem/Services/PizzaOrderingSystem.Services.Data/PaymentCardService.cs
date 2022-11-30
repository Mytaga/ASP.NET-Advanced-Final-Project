using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;
using System;
using System.Linq;
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
            CreditCard creditCard = new CreditCard()
            {
                CardHolder = viewModel.CardHolder,
                CardNumber = viewModel.CardNumber,
                Cvc = viewModel.Cvc,
                ExpirationDate = viewModel.ExpirationDate,
                UserId = viewModel.UserId,
            };

            await this.creditCardRepo.AddAsync(creditCard);
            await this.creditCardRepo.SaveChangesAsync();
        }

        public async Task Delete(CreditCard card)
        {
            this.creditCardRepo.Delete(card);
            await this.creditCardRepo.SaveChangesAsync();
        }

        public async Task<AddCardViewModel> GetAllАsync(string userId)
        {
            var cards = this.creditCardRepo
                .AllAsNoTracking()
                .Where(c => c.UserId == userId);

            AddCardViewModel viewModel = new AddCardViewModel
            {
                SavedCards = await cards.Select(c => new PaymentCardViewModel()
                {
                    CardNumber = c.CardNumber,
                    Id = c.Id,
                })
                .ToListAsync(),
            };

            return viewModel;
        }

        public async Task<CreditCard> GetByIdAsync(string id)
        {
            return await this.creditCardRepo
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}