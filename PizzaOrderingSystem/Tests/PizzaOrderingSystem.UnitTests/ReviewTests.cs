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
    }
}
