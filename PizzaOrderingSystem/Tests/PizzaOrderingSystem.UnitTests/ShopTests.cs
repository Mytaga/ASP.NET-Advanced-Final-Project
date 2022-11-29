using PizzaOrderingSystem.Web.ViewModels.ShopViewModels;

namespace PizzaOrderingSystem.UnitTests
{
    public class ShopTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<Shop> shopRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private IShopService shopService;
        private IEnumerable<Shop> shops;
        private CreateShopViewModel shop;

        [SetUp]
        public void SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.shopRepo = new EfDeletableEntityRepository<Shop>(this.dbContext);
            this.shopService = new ShopService(shopRepo);
        }

        [Test]
        public async Task CreateAsyncAddsCorrect()
        {
            var address = new Address()
            {
                City = "Sofia",
                Street = "TestStreet",
                StreetNumber = 2,
                PostCode = "1232",
            };

            shop = new CreateShopViewModel()
            {
                Name = "Test",
                PhoneNumber = "0898898782",
                Description = "TestDescription",
                City = address.City,
                Street = address.Street,
                StreetNumber = address.StreetNumber,
            };

            await this.shopService.CreateAsync(shop);

            Assert.That(this.dbContext.Shops.Count(), Is.EqualTo(1));
        }


        [Test]
        public async Task CreateAsyncAddsCorrectShop()
        {
            var address = new Address()
            {
                City = "Sofia2",
                Street = "TestStreet",
                StreetNumber = 2,
                PostCode = "1232",
            };

            shop = new CreateShopViewModel()
            {
                Name = "Test2",
                PhoneNumber = "0898892782",
                Description = "TestDescription",
                City = address.City,
                Street = address.Street,
                StreetNumber = address.StreetNumber,
            };

            await this.shopService.CreateAsync(shop);

            Assert.That(await this.dbContext.Shops.AnyAsync(s => s.Name == "Test2"), Is.True);
        }

        [Test]
        public async Task GetAllAsyncReturnsCorrectCount()
        {
            await this.FillCollection();

            await this.shopService.GetAllAsync();

            Assert.That(this.dbContext.Shops.Count(), Is.EqualTo(5));
        }

        [Test]
        public async Task GetAllAsyncReturnsCorrectCountEmpty()
        {
            await this.FlushCollection();
            await this.shopService.GetAllAsync();

            Assert.That(this.dbContext.Shops.Count(), Is.EqualTo(0));
        }

        private async Task FillCollection()
        {

            this.shops = new List<Shop>()
            {
                new Shop() { Name = "First", Description = "A very short one", PhoneNumber = "0896674396", AddressId = "addressid"},
                new Shop() { Name = "Second", Description = "A very short one", PhoneNumber = "0896674396", AddressId = "addressid2"},
                new Shop() { Name = "Third", Description = "A very short one", PhoneNumber = "0896674396",AddressId = "addressid3" },
            };

            await this.dbContext.AddRangeAsync(this.shops);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task FlushCollection()
        {
            var shops = await this.shopRepo.All().ToListAsync();

            foreach (var shop in shops)
            {
                this.dbContext.Shops.Remove(shop);
            }

            await this.shopRepo.SaveChangesAsync();
        }
    }
}