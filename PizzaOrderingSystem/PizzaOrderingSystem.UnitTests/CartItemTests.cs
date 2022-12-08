namespace PizzaOrderingSystem.UnitTests
{
	public class CartItemTests
	{
		private ApplicationDbContext dbContext;
		private IDeletableEntityRepository<CartItem> cartItemRepo;
		private DbContextOptionsBuilder<ApplicationDbContext> options;
		private ICartItemService cartItemService;
		private IEnumerable<CartItem> cartItems;

		[SetUp]
		public void SetUp()
		{
			this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "PizzaTestDb");
			this.dbContext = new ApplicationDbContext(this.options.Options);
			this.cartItemRepo = new EfDeletableEntityRepository<CartItem>(this.dbContext);
			this.cartItemService = new CartItemService(cartItemRepo);			
		}
		
		[Test]
		public async Task GetAllAsyncReturnsCorrectWithEmptyCollection()
		{
			await this.ClearItems();

			var items = await this.cartItemService.GetAllAsync();

			Assert.That(items.Count(), Is.EqualTo(0));
		}

		[Test]
		public async Task GetAllAsyncReturnsCorrect()
		{			
			var items = await this.cartItemService.GetAllAsync();

			Assert.That(items.Count(), Is.EqualTo(3));
		}

		[Test]
		public async Task GetAllByOrderAsyncReturnsCorrect()
		{
			await this.FillUpItems();
			var orderId = "1";

			var result = await this.cartItemService.GetAllByOrderAsync(orderId);

			Assert.That(result.Count(), Is.EqualTo(1));
		}

		[Test]
		public async Task GetAllByOrderAsyncReturnsEmptyWithNonExistingOrder()
		{
			var orderId = "4";

			var result = await this.cartItemService.GetAllByOrderAsync(orderId);

			Assert.That(result.Count(), Is.EqualTo(0));
		}

		[Test]
		public async Task ExistByIdAsyncReturnsCorrectTrue()
		{
			await this.FillUpItems();
			var item = this.dbContext.CartItems.FirstOrDefault(c => c.ProductId == "1");
			var id = item.Id;
			var result = await this.cartItemService.GetByIdАsync(id);
			Assert.That(result, Is.EqualTo(item));
		}

		[Test]
		public async Task ExistByNameAsyncReturnsCorrectFalse()
		{
			var id = Guid.NewGuid().ToString();
			var result = await this.cartItemService.GetByIdАsync(id);
			Assert.That(result, Is.EqualTo(null));
		}

		private async Task FillUpItems()
		{
			this.cartItems = new List<CartItem>()
			{
				new CartItem() { Quantity = 2, ProductId = "1", OrderId = "1", ShoppingCartId = "1"},
				new CartItem() { Quantity = 3, ProductId = "2", OrderId = "2", ShoppingCartId = "1"},
				new CartItem() { Quantity = 4, ProductId = "5", OrderId = "5", ShoppingCartId = "1"},
			};

			await this.dbContext.AddRangeAsync(this.cartItems);
			await this.dbContext.SaveChangesAsync();
		}

		private async Task ClearItems()
		{
			var item = await this.dbContext.CartItems.FirstOrDefaultAsync(c => c.Quantity == 2);
			var item2 = await this.dbContext.CartItems.FirstOrDefaultAsync(c => c.Quantity == 3);
			var item3 = await this.dbContext.CartItems.FirstOrDefaultAsync(c => c.Quantity == 4);

			this.cartItems = new List<CartItem>()
			{
				item,
				item2,
				item3,
			};

			this.dbContext.RemoveRange(this.cartItems);
			await this.dbContext.SaveChangesAsync();
		}
	}
}
