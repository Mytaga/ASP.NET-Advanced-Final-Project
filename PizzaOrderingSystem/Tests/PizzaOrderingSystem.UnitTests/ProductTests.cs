using System.Security.Cryptography;

namespace PizzaOrderingSystem.UnitTests
{
    public class ProductTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<Product> productRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private IProductService productService;
        private Mock<ICategoryService> categoryService;

        [SetUp]
        public void SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.productRepo = new EfDeletableEntityRepository<Product>(this.dbContext);
            this.categoryService= new Mock<ICategoryService>();
            this.productService = new ProductService(productRepo, categoryService.Object);
        }

        [Test]
        public async Task AddProductWorksCorrect()
        {
            var model = this.CreateModel();
            string imageUrl = "www.image.com";

            await this.productService.AddProductAsync(model, imageUrl);

            Assert.That(this.productRepo.All().Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task AddProductWorksCorrectProduct()
        {
            var model = this.CreateModel();
            string imageUrl = "www.image.com";

            await this.productService.AddProductAsync(model, imageUrl);

            Assert.That(this.productRepo.All().Any(p => p.Name == model.Name), Is.True);
        }

        [Test]
        public async Task DeleteProduct()
        {
            var product = await this.productRepo.All().FirstOrDefaultAsync(p => p.Name == "name");

            await this.productService.DeleteProductAsync(product);

            Assert.That(this.productRepo.All().Contains(product), Is.False);
        }


        private CreateProductInputModel CreateModel()
        {
            var model = new CreateProductInputModel()
            {
                Name = "name",
                Price = 20M,
                Description= "description",
                CategoryId = 2,
            };

            return model;
        }
    }
}
