namespace PizzaOrderingSystem.UnitTests
{
    [TestFixture]
    public class UserTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<ApplicationUser> userRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private IUserService userService;
        private IEnumerable<ApplicationUser> users;

        [SetUp]
        public async Task SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.userRepo = new EfDeletableEntityRepository<ApplicationUser>(this.dbContext);
            this.userService = new UserService(userRepo);          
        }

        [Test]
        public async Task GetUsersCountAsyncReturnsCorrect()
        {
            await this.FlushCollection();

            await this.FillCollection();

            var usersCount = await this.userService.GetUsersCountAsync();
            
            Assert.That(usersCount, Is.EqualTo(3));
        }

        [Test]
        public async Task GetUsersCountAsyncReturnsCorrectWithEmptyCollection()
        {
            await this.FlushCollection();

            var usersCount = await this.userService.GetUsersCountAsync();

            Assert.That(usersCount, Is.EqualTo(0));
        }

        private async Task FillCollection()
        {
            this.users = new List<ApplicationUser>()
            {
                new ApplicationUser(){ FirstName = "First", LastName = "Last"},
                new ApplicationUser(){ FirstName = "Second", LastName = "LastSecond"},
                new ApplicationUser(){ FirstName = "Third", LastName = "LastThird"},
            };

            this.dbContext.Users.AddRange(users);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task FlushCollection()
        {
            var users = await this.userRepo.All().ToListAsync();

            foreach (var user in users)
            {
                this.dbContext.Users.Remove(user);
            }

            await this.userRepo.SaveChangesAsync();
        }
    }
}
