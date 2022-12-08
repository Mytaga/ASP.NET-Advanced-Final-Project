using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography;

namespace PizzaOrderingSystem.UnitTests
{
    public class ProductTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<Product> productRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private IProductService productService;
        private Mock<ICategoryService> mockedICategoryService;

        [SetUp]
        public void SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.productRepo = new EfDeletableEntityRepository<Product>(this.dbContext);
            this.mockedICategoryService = new Mock<ICategoryService>();
            this.productService = new ProductService(productRepo, this.mockedICategoryService.Object);
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

        [Test]
        public async Task EditProductWorksCorrect()
        {
            var model = this.CreateModel();
            string imageUrl = "www.image.com";

            await this.productService.AddProductAsync(model, imageUrl);

            var product = await this.productRepo.All().FirstOrDefaultAsync(p => p.Name == "name");
            var productId = product.Id;

            var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "category");

            var editModel = new EditProductInputModel()
            {
                Name = "newName",
                Price = 30M,
                Description = "newDescription",
                CategoryId = category.Id,
            };

            await this.productService.EditProductAsync(editModel, productId, imageUrl);

            Assert.That(product.Name, Is.EqualTo(editModel.Name));
            Assert.That(product.Price, Is.EqualTo(editModel.Price));
            Assert.That(product.Description, Is.EqualTo(editModel.Description));
            
        }

        [Test]
        public async Task GetAllByCategoriesAsyncReturnsCorrectProducts()
        {
            await this.FlushCollection();
            await this.FillProductCollection();

            var categoryName = "testCategory";

            string searchName = string.Empty;

            var products = await this.productService.GetAllByCategoryAsync(categoryName, searchName);

            Assert.That(products.Products.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllByCategoriesAsyncReturnsCorrectProductsWithSearchString()
        {
            await this.FlushCollection();
            await this.FillProductCollection();

            var categoryName = "testCategory";

            string searchName = "name";

            var products = await this.productService.GetAllByCategoryAsync(categoryName, searchName);

            Assert.That(products.Products.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllByNameAsyncReturnsCorrectProducts()
        {
            await this.FlushCollection();
            await this.FillProductCollection();

            string searchName = string.Empty;

            var products = await this.productService.GetAllByNameAsync(searchName);

            Assert.That(products.Products.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetAllByNameAsyncReturnsCorrectProductsWithSearchString()
        {
            await this.FlushCollection();
            await this.FillProductCollection();

            string searchName = "name";

            var products = await this.productService.GetAllByNameAsync(searchName);

            Assert.That(products.Products.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetAllProductsCountAsyncReturningCorrect()
        {
            await this.FlushCollection();
            await this.FillProductCollection();

            int count = await this.productService.GetAllProductsCountAsync();

            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetAllProductsCountAsyncReturningCorrectWithEmptyCollection()
        {
            await this.FlushCollection();

            int count = await this.productService.GetAllProductsCountAsync();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetByIdAsyncReturningCorrectProduct()
        {
            await this.FlushCollection();
            var model = this.CreateModel();
            string imageUrl = "www.image.net";
            await this.productService.AddProductAsync(model, imageUrl);

            var product = await this.productRepo.All().FirstOrDefaultAsync(p => p.Name == model.Name);
            var testProduct = await this.productService.GetByIdАsync(product.Id);

            Assert.That(testProduct, Is.EqualTo(product));
        }

        [Test]
        public async Task GetByIdAsyncReturningCorrectNull()
        {
            await this.FlushCollection();
           
            var testProduct = await this.productService.GetByIdАsync("0");

            Assert.That(testProduct, Is.Null);
        }

        [Test]
        public async Task GetEditModelAsyncReturningCorrect()
        {
            await this.FlushCollection();
            await this.FillProductCollection();

            var product = await this.productRepo.All().FirstOrDefaultAsync(p => p.Name == "name");

            var model = await this.productService.GetEditModel(product);

            Assert.That(model.Name, Is.EqualTo(product.Name));
            Assert.That(model.Price, Is.EqualTo(product.Price));
            Assert.That(model.Description, Is.EqualTo(product.Description));
        }

        [Test]
        public async Task GetProductDetailsReturningCorrect()
        {
            await this.FlushCollection();
            await this.FillProductCollection();

            var product = await this.productRepo.All().FirstOrDefaultAsync(p => p.Name == "name");

            var model = this.productService.GetProductDetails(product);

            Assert.That(model.Name, Is.EqualTo(product.Name));
            Assert.That(model.Price, Is.EqualTo(product.Price));
            Assert.That(model.Description, Is.EqualTo(product.Description));
            Assert.That(model.ImageUrl, Is.EqualTo(product.ImageUrl));
            Assert.That(model.CategoryName, Is.EqualTo(product.Category.Name));
        }

        private async Task FlushCollection()
        {
            var products = await this.productRepo.All().ToListAsync();

            foreach (var product in products)
            {
                this.productRepo.Delete(product);
            }

            await this.dbContext.SaveChangesAsync();
        }

        private CreateProductInputModel CreateModel()
        {
            var category = new Category()
            {
                Name = "category",
            };

            this.dbContext.Categories.Add(category);
            this.dbContext.SaveChanges();

            var model = new CreateProductInputModel()
            {
                Name = "name",
                Price = 20M,
                Description= "description",
                CategoryId = category.Id,
            };

            return model;
        }
        private async Task FillProductCollection()
        {
            await this.FillUpCategoriesCollection();


            var products = new List<Product>()
            {
                new Product() { Name = "name", Price = 20M, Description = "newDescription", ImageUrl = "www.image.com", Category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "testCategory")},
                new Product() { Name = "name2", Price = 220M, Description = "newDescription2", ImageUrl = "www.image2.com", Category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "testCategory2")},
                new Product() { Name = "name3", Price = 240M, Description = "newDescription3", ImageUrl = "www.image3.com", Category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "testCategory3")},
            };

            await this.dbContext.AddRangeAsync(products);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task FillUpCategoriesCollection()
        {
            var categories = new List<Category>()
            {
                new Category(){ Name = "testCategory" },
                new Category(){ Name = "testCategory2" },
                new Category(){ Name = "testCategory3" },
            };

            await this.dbContext.Categories.AddRangeAsync(categories);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
