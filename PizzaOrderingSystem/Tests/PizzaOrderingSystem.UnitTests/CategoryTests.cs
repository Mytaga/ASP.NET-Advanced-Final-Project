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
			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public async Task ExistByNameAsyncReturnsCorrectFalse()
		{
			await this.FillUpCategories(4);
			string name = "Category5";
			bool result = await this.categoryService.ExistByNameAsync(name);
			Assert.That(result, Is.EqualTo(false));
		}

		[Test]
		public async Task AllAsyncReturnsCorrectNumberWithEmptyCollection()
		{
			await this.ClearCategories();
			IEnumerable<ListProductCategoriesViewModel> categories = await this.categoryService.AllAsync();
			Assert.That(categories.Count(), Is.EqualTo(0));
		}		

		[Test]
		public async Task ExistByIdAsyncReturnsCorrectTrue()
		{
			await this.FillUpCategories(4);
			var category = this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name.Contains("Category"));
			var id = category.Id;
			bool result = await this.categoryService.ExistByIdAsync(id);
			Assert.That(result == false, Is.True);
		}

		[Test]
		public async Task ExistByIdAsyncReturnsCorrectFalse()
		{
			await this.FillUpCategories(4);
			int id = 6;
			bool result = await this.categoryService.ExistByIdAsync(id);
			Assert.That(result == false, Is.False);
		}

		[Test]
		public async Task GetByIdAsyncReturnsCorrectCategory()
		{
			await this.FillUpCategories(4);

			var category = await this.dbContext.Categories.FindAsync(3);

			var dbCategory = await this.categoryService.GetByIdAsync(3);

			Assert.That(category, Is.EqualTo(dbCategory));
		}

		[Test]
		public async Task GetByIdAsyncReturnsNullWithIncorrectId()
		{
			await this.FillUpCategories(4);

			var dbCategory = await this.categoryService.GetByIdAsync(0);

			Assert.That(dbCategory, Is.EqualTo(null));
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

		private async Task ClearCategories()
		{
			var categories = await this.dbContext.Categories.ToListAsync();

			foreach (var category in categories)
			{
				this.dbContext.Remove(category);
			}

			await this.dbContext.SaveChangesAsync();
		}
	}
}
