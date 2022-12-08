using PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels;

namespace PizzaOrderingSystem.UnitTests
{
    public class PaymentCardTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<CreditCard> cardRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private IPaymentCardService paymentCardService;


        [SetUp]
        public void SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.cardRepo = new EfDeletableEntityRepository<CreditCard>(this.dbContext);
            this.paymentCardService = new PaymentCardService(cardRepo);
        }

        [Test]
        public async Task AddAsyncAddsCorrect()
        {
            var model = await this.CreateModel();
            await this.paymentCardService.AddAsync(model);

            Assert.That(this.dbContext.CreditCards.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task AddAsyncAddsCorrectCard()
        {
            var model = await this.CreateModel();
            await this.paymentCardService.AddAsync(model);

            Assert.That(this.dbContext.CreditCards.Any(c => c.CardHolder == "Petar"), Is.True);
        }

        [Test]
        public async Task DeleteAsyncDeletesCorrect()
        {
            var card = this.cardRepo.All().FirstOrDefault(c => c.CardHolder == "Petar");
            await this.paymentCardService.Delete(card);

            Assert.That(this.dbContext.CreditCards.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteAsyncDeletesCorrectCard()
        {
            var card = this.cardRepo.All().FirstOrDefault(c => c.CardHolder == "Petar");
            await this.paymentCardService.Delete(card);

            Assert.That(this.dbContext.CreditCards.Any(c => c.CardHolder == "Petar"), Is.False);
        }

        [Test]
        public async Task GetAllAsyncReturningCorrect()
        {
            var model = await this.CreateModel();

            await this.paymentCardService.AddAsync(model);
            var cards = await this.paymentCardService.GetAllАsync(model.UserId);

            Assert.That(cards.SavedCards.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetByIdAsyncReturningCorrect()
        {
            var model = await this.CreateModel();
            await this.paymentCardService.AddAsync(model);

            var card = await this.cardRepo.All().FirstOrDefaultAsync(c => c.CardNumber == model.CardNumber);

            await this.paymentCardService.GetByIdAsync(card.Id);

            Assert.That(card, !Is.Null);
        }

        private async Task<AddCardViewModel> CreateModel()
        {
            var user = new ApplicationUser()
            {
                FirstName = "Mitko",
                LastName = "Kralev",
            };

            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

            var addCardViewModel = new AddCardViewModel()
            {
                CardHolder = "Petar",
                CardNumber = "1234 2654 6524 8541",
                ExpirationDate = "12/24",
                Cvc = "213",
                UserId = user.Id,
            };

            return addCardViewModel;
        }
    }
}
