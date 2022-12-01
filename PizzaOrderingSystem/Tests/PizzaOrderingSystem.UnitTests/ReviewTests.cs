using PizzaOrderingSystem.Web.ViewModels.CategoryViewModels;

namespace PizzaOrderingSystem.UnitTests
{
    public class ReviewTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<Review> reviewRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private IReviewService reviewService;

        [SetUp]
        public void SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.reviewRepo = new EfDeletableEntityRepository<Review>(this.dbContext);
            this.reviewService = new ReviewService(reviewRepo);
        }

        [Test]
        public async Task AddReviewAddsCorrect()
        {
            var model = await this.CreateModel();
            await this.reviewService.AddReview(model, model.UserId);

            Assert.That(this.reviewRepo.All().Count(), Is.EqualTo(1));  
        }

        [Test]
        public async Task AddReviewAddsCorrectReview()
        {
            var model = await this.CreateModel();
            await this.reviewService.AddReview(model, model.UserId);

            Assert.That(this.reviewRepo.All().Any(r => r.AuthorName == "Mitko"), Is.EqualTo(true));
        }

        [Test]
        public async Task GetAllAsyncReturnsCorrect()
        {
            var reviews = await this.reviewService.GetAllAsync();

            Assert.That(reviews.Reviews.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllAsyncReturnsCorrectWithEmptyCollection()
        {
            await this.FlushCollection();

            var reviews = await this.reviewService.GetAllAsync();

            Assert.That(reviews.Reviews.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllReviewsCountAsyncReturnsCorrectWithEmptyCollection()
        {
            await this.FlushCollection();

            int count = await this.reviewService.GetAllReviewsCountAsync();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllReviewsCountAsyncReturnsCorrect()
        {
            var model = await this.CreateModel();

            await this.reviewService.AddReview(model, model.UserId);

            int count = await this.reviewService.GetAllReviewsCountAsync();

            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetByIdReturnsCorrectReview()
        {
            var createModel = await this.CreateModel();

            await this.reviewService.AddReview(createModel, createModel.UserId);

            var model = await this.reviewRepo.AllAsNoTracking().FirstOrDefaultAsync(r => r.AuthorName == "Mitko");

            var testModel = await this.reviewService.GetByIdAsync(model.Id);

            Assert.That(testModel, Is.Not.Null);
        }

        [Test]
        public async Task DeleteReviewWorksCorrect()
        {
            var createModel = await this.CreateModel();

            await this.reviewService.AddReview(createModel, createModel.UserId);

            var review = await this.reviewRepo.AllAsNoTracking().FirstOrDefaultAsync(r => r.AuthorName == "Mitko");

            await this.reviewService.DeleteReview(review);

            Assert.That(this.reviewRepo.All().Contains(review), Is.False);
        }

        private async Task<CreateReviewInputModel> CreateModel()
        {
            var user = new ApplicationUser()
            {
                FirstName = "Mitko",
                LastName = "Kralev",
            };

            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

            var model = new CreateReviewInputModel()
            {
                AuthorName = user.FirstName, 
                Content = "Bla bla bla such a dumb comment!",              
                UserId = user.Id,
            };

            return model;
        }

        public async Task FlushCollection()
        {
            var reviews = await this.reviewRepo.All().ToListAsync();

            foreach (var review in reviews)
            {
                this.dbContext.Reviews.Remove(review);
            }

            await this.reviewRepo.SaveChangesAsync();
        }
    }
}
