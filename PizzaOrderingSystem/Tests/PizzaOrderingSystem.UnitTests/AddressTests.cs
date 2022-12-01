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
			var Address = new Address()
			{
				City = "A",
				Street = "S",
				StreetNumber = 1,
				Floor = 1,
				PostCode = "1234",
			};

			await this.addressService.AddAddressAsync(Address);

			Assert.True(this.dbContext.Addresses.Count() == 2);
		}

		[Test]
		public async Task AddAddressAsyncShouldAddCorrectAddress()
		{
            var Address = new Address()
            {
                City = "B",
                Street = "Sa",
                StreetNumber = 12,
                Floor = 11,
                PostCode = "1334",
            };

            await this.addressService.AddAddressAsync(Address);

			Assert.That(this.addressRepo.All().Any(a => a.City == Address.City), Is.True);
        }
	}
}