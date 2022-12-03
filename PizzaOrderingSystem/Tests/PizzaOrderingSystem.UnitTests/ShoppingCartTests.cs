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
                CategoryId = 2,
            };

            this.dbContext.Products.Add(product);
            await this.dbContext.SaveChangesAsync();

            await this.cartService.AddToCartAsync(product);

            Assert.That(dbContext.CartItems.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task AddToCartAsyncWorksCorrectWithSameProduct()
        {
            await this.ClearItems();

            var product = new Product()
            {
                Description = "Test",
                ImageUrl = "www.image.com",
                Name = "Product",
                CategoryId = 2,
            };

            this.dbContext.Products.Add(product);
            await this.dbContext.SaveChangesAsync();

            await this.cartService.AddToCartAsync(product);
            await this.cartService.AddToCartAsync(product);

            var item = await this.cartItemRepo.All().FirstOrDefaultAsync(ci => ci.ProductId == product.Id);

            Assert.That(item.Quantity, Is.EqualTo(2));
        }
        
        [Test]
        public async Task ClearCartAsyncWorksCorrect()
        {
            var cartItems = await this.dbContext.CartItems.Where(c => c.ShoppingCartId == shoppingCart.ShoppingCartId).ToListAsync();
            await this.cartService.ClearCartAsync();

            Assert.IsTrue(cartItems.All(c => c.ShoppingCartId == null));
        }

        [Test]
        public async Task DecreaseQtyWorkingCorrectWithItemQtyBiggerThenOne()
        {
            await this.ClearItems();

            var item = await this.CreateItem();

            await this.cartService.DecreaseQuantity(item);

            Assert.That(item.Quantity, Is.EqualTo(1));
        }

        [Test]
        public async Task DecreaseQtyWorkingCorrectWithItemQtyEqualToOne()
        {
            await this.ClearItems();

            var item = await this.CreateItem();

            await this.cartService.DecreaseQuantity(item);
            await this.cartService.DecreaseQuantity(item);

            Assert.That(this.shoppingCart.Items.Contains(item), Is.EqualTo(false));
        }

        [Test]
        public async Task IncreaseQtyWorkingCorrect()
        {
            await this.ClearItems();

            var item = await this.CreateItem();

            await this.cartService.IncreaseQuantity(item);

            Assert.That(item.Quantity, Is.EqualTo(3));
        }

        [Test]
        public async Task GetCartItemsAsyncWorkingCorrect()
        {
            await this.ClearItems();
            var item = await this.CreateItem();

            var items = await this.cartService.GetCartItemsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(items.Count(), Is.EqualTo(1));
                Assert.That(items, Does.Contain(item));
            });
        }

        [Test]
        public async Task GetCartItemsAsyncWorkingCorrectWithEmptyCollection()
        {
            await this.ClearItems();

            var items = await this.cartService.GetCartItemsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(items.Count(), Is.EqualTo(0));
                Assert.That(shoppingCart.Items, Is.Empty);
            });
        }

        [Test]
        public async Task GetCartProductsAsyncWorkingCorrect()
        {
            await this.ClearItems();

            var item = await this.CreateItem();

            var items = await this.cartService.GetCartProductsAsync();
            Assert.Multiple(() =>
            {
                Assert.That(items.Count(), Is.EqualTo(1));
                Assert.That(items, Does.Contain(item));
            });
        }

        [Test]
        public async Task GetCartProductsAsyncWorkingtWithEmptyCollection()
        {
            await this.ClearItems();

            var items = await this.cartService.GetCartProductsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(items.Count(), Is.EqualTo(0));
                Assert.That(shoppingCart.Items, Is.Empty);
            });
        }

        [Test]
        public async Task RemoveFromCartAsyncWorkingCorrect()
        {
            await this.ClearItems();

            var item = await this.CreateItem();

            await this.cartService.RemoveFromCartAsync(item);
            Assert.Multiple(() =>
            {
                Assert.That(shoppingCart.Items, Is.Empty);
                Assert.That(this.dbContext.CartItems.Count(), Is.EqualTo(0));
            });
        }

        [Test]
        public async Task GetShoppingCartTotalReturnsCorrectValue()
        {
            await this.ClearItems();

            await this.CreateItem();

            var total = this.cartService.GetShoppingCartTotal();

            Assert.That(total, Is.EqualTo(40));
        }


        [Test]
        public async Task GetShoppingCartTotalReturnsCorrectZero()
        {
            await this.ClearItems();

            var total = this.cartService.GetShoppingCartTotal();

            Assert.That(total, Is.EqualTo(0));
        }

        [Test]
        public async Task GetShoppingCartItemsCountReturnsCorrect()
        {
            await this.ClearItems();

            await this.CreateItem();

            var total = this.cartService.GetShoppingCartItemCount();

            Assert.That(total, Is.EqualTo(2));
        }

        [Test]
        public async Task GetShoppingCartItemsCountReturnsCorrectZero()
        {
            await this.ClearItems();

            var total = this.cartService.GetShoppingCartItemCount();

            Assert.That(total, Is.EqualTo(0));
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

        private async Task<CartItem> CreateItem()
        {
            var product = new Product()
            {
                Description = "Test",
                ImageUrl = "www.image.com",
                Name = "Product",
                Price = 20M,
                CategoryId = 2,
            };

            this.dbContext.Products.Add(product);

            var item = new CartItem()
            {
                ShoppingCartId = this.shoppingCart.ShoppingCartId,
                Quantity = 2,
                Product = product,
                ProductId = product.Id,
            };

            await this.cartItemRepo.AddAsync(item);
            await this.cartItemRepo.SaveChangesAsync();

            return item;
        }
    }
}