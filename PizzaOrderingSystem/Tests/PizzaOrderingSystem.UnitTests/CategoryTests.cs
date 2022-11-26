namespace PizzaOrderingSystem.UnitTests
{
	public class CategoryTests
	{
		private ApplicationDbContext dbContext;
		private IDeletableEntityRepository<Category> categoryRepo;
		private DbContextOptionsBuilder<ApplicationDbContext> options;
		private ICategoryService categoryService;

		[SetUp]
		public void SetUp()
		{
			this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "PizzaTestDb");
			this.dbContext = new ApplicationDbContext(this.options.Options);
			this.categoryRepo = new EfDeletableEntityRepository<Category>(this.dbContext);
			this.categoryService = new CategoryService(categoryRepo);			
		}

		[Test]
		public async Task AddCategoryAsyncShouldAddCorrectly()
		{
			CreateCategoryInputModel category = new CreateCategoryInputModel()
			{
				Name = "Category5",
			};

			await this.categoryService.AddCategoryAsync(category);

			Assert.That(dbContext.Categories.Count(), Is.EqualTo(1));
		}

		[Test]
		public async Task AllAsyncReturnsCorrectNumber() 
		{
			await this.FillUpCategories(4);
			IEnumerable<ListProductCategoriesViewModel> categories = await this.categoryService.AllAsync();
			Assert.That(categories.Count(), Is.EqualTo(5));
		}

		[Test]
		public async Task ExistByNameAsyncReturnsCorrectTrue()
		{
			await this.FillUpCategories(4);
			string name = "Category1";
			bool result = await this.categoryService.ExistByNameAsync(name);
			Assert.That(result == true, Is.True);
		}

		[Test]
		public async Task ExistByNameAsyncReturnsCorrectFalse()
		{
			await this.FillUpCategories(4);
			string name = "Category4";
			bool result = await this.categoryService.ExistByNameAsync(name);
			Assert.That(result == true, Is.True);
		}

		private async Task FillUpCategories(int categoriesCount)
		{
			for (var i = 1; i <= categoriesCount; i++)
			{
				await this.categoryService
					.AddCategoryAsync(new CreateCategoryInputModel
					{
						Name = $"Category{i}",
					});
			}
		}
	}
}
