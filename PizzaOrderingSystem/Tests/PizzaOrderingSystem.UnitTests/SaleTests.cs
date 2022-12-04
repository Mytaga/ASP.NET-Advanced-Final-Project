using System.Runtime.CompilerServices;

namespace PizzaOrderingSystem.UnitTests
{
    public class SaleTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<Sale> saleRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private ISaleService saleService;

        [SetUp]
        public void SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.saleRepo = new EfDeletableEntityRepository<Sale>(this.dbContext);
            this.saleService = new SaleService(saleRepo);
        }

        [Test]
        public async Task AddAsyncAddsCorrect()
        {
            await this.FlushCollection();

            Sale sale = new Sale();

            await this.saleService.AddAsync(sale);

            Assert.That(dbContext.Sales.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task AddAsyncAddsCorrectSale()
        {
            Sale sale = new Sale();

            await this.saleService.AddAsync(sale);

            Assert.That(dbContext.Sales.Contains(sale), Is.EqualTo(true));
        }

        [Test]
        public async Task UpdateSalesWorksCorrect()
        {
            await this.FlushCollection();

            Sale sale = await this.CreateSale();

            await this.saleService.AddAsync(sale);
            await this.saleService.UpdateAsync();

            Assert.That(sale.Amount, Is.EqualTo(25M));
        }

        [Test]
        public async Task GetAllSalesAsyncReturnsCorrect()
        {
            await this.FlushCollection();

            Sale sale = await this.CreateSale();

            await this.saleService.AddAsync(sale);
            await this.saleService.UpdateAsync();

            var sales = await this.saleService.GetAllSalesAsync();

            Assert.That(sales.Count(), Is.EqualTo(1));
        }

        private async Task<Sale> CreateSale()
        {
            var order = new Order()
            {
                TotalPrice = 25M,
                TimeOfOrder = DateTime.Now,
                PaymentType = Data.Models.Enums.PaymentType.Cash,
                UserId = "2",
            };

            await this.dbContext.Orders.AddAsync(order);

            var sale = new Sale()
            {
                Order = order,
            };

            return sale;
        }

        private async Task FlushCollection()
        {
            var sales = await this.saleRepo.All().ToListAsync();

            foreach (var sale in sales)
            {
                this.dbContext.Sales.Remove(sale);
            }

            await this.saleRepo.SaveChangesAsync();
        }
    }
}