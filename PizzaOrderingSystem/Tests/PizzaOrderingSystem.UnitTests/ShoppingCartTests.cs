using NuGet.Frameworks;

namespace PizzaOrderingSystem.UnitTests
{
	public class ShoppingCartTests
	{
		private ApplicationDbContext dbContext;
		private IDeletableEntityRepository<CartItem> cartItemRepo;
		private DbContextOptionsBuilder<ApplicationDbContext> options;
		private ICartService cartService;
		private ShoppingCart shoppingCart;

		[SetUp]
		public void SetUp()
		{
			this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "PizzaTestDb");
			this.dbContext = new ApplicationDbContext(this.options.Options);
			this.cartItemRepo = new EfDeletableEntityRepository<CartItem>(this.dbContext);
			this.shoppingCart = new ShoppingCart();
			this.cartService = new CartService(cartItemRepo, shoppingCart);
		}

		[Test]
		public async Task AddToCartAsyncWorksCorrect()
		{
			await this.ClearItems();

			var product = new Product()
			{
				Description = "Test",
				ImageUrl = "www.image.com",
				Name = "Product",
			};

			this.dbContext.Products.Add(product);

			var item = new CartItem()
			{
				ShoppingCartId = this.shoppingCart.ShoppingCartId,
				Quantity = 1,
				Product = product,
				ProductId = product.Id,
			};

			await this.cartService.AddToCartAsync(product);

			Assert.That(dbContext.CartItems.Count(), Is.EqualTo(1));
		}

		[Test]
		public async Task ClearCartAsyncWorksCorrect()
		{
			var cartItems = await this.dbContext.CartItems.Where(c => c.ShoppingCartId == shoppingCart.ShoppingCartId).ToListAsync();
			await this.cartService.ClearCartAsync();

			Assert.IsTrue(cartItems.All(c => c.ShoppingCartId == null));
		}

		private async Task ClearItems()
		{
			var items = await this.dbContext.CartItems.ToListAsync();

			foreach (var item in items)
			{
				this.dbContext.Remove(item);
			}

			await this.dbContext.SaveChangesAsync();
		}
	}
}
