namespace PizzaOrderingSystem.UnitTests
{
	public class AddressTests
	{
		private ApplicationDbContext dbContext;
		private IDeletableEntityRepository<Address> addressRepo;
		private DbContextOptionsBuilder<ApplicationDbContext> options;
		private IAddressService addressService;

		[SetUp]
		public void Setup()
		{
			this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "PizzaTestDb");
			this.dbContext = new ApplicationDbContext(this.options.Options);
			this.addressRepo = new EfDeletableEntityRepository<Address>(this.dbContext);
			this.addressService = new AddressService(addressRepo);
		}

		[Test]
		public async Task AddAddressAsyncShouldAddCorrectly()
		{
			var address = new Address()
			{
				City = "A",
				Street = "S",
				StreetNumber = 1,
				Floor = 1,
				PostCode = "1234",
			};

			var addressCountBeforeTest = this.addressRepo.All().Count();

			await this.addressService.AddAddressAsync(address);

			var addressCountAfterTest = this.addressRepo.All().Count();

			Assert.True(addressCountAfterTest == 2);
			Assert.True(addressCountBeforeTest + 1 == addressCountAfterTest);
		}

		[Test]
		public async Task AddAddressAsyncShouldAddCorrectAddress()
		{
            var address = new Address()
            {
                City = "B",
                Street = "Sa",
                StreetNumber = 12,
                Floor = 11,
                PostCode = "1334",
            };

            await this.addressService.AddAddressAsync(address);

			var newAddress = this.addressRepo.All().FirstOrDefault(a => a.Id == address.Id);

            Assert.That(newAddress, Is.Not.Null);
            Assert.That(newAddress.City, Is.EqualTo(address.City));
			Assert.That(newAddress.Street, Is.EqualTo(address.Street));
			Assert.That(newAddress.StreetNumber, Is.EqualTo(address.StreetNumber));
			Assert.That(newAddress.Floor, Is.EqualTo(address.Floor));
			Assert.That(newAddress.PostCode, Is.EqualTo(address.PostCode));
        }
	}
}