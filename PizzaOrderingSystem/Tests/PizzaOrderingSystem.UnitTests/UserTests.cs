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

        [Test]
        public void GetUserReturnsCorrectInfo()
        {
            var address = new Address()
            {
                City = "Sofia",
                Street = "Street",
                StreetNumber = 1,
                Floor = 1,
                PostCode = "1231",
            };

            var appUser = new ApplicationUser()
            {
                FirstName = "Hristo",
                LastName = "Stoichkov",
                Address = address,
            };

            var user = this.userService.GetUser(appUser);

            Assert.That(user, Is.Not.Null);
            Assert.That(user.FirstName, Is.EqualTo(appUser.FirstName));
            Assert.That(user.LastName, Is.EqualTo(appUser.LastName));
            Assert.That(user.Email, Is.EqualTo(appUser.Email));
            Assert.That(user.PhoneNumber, Is.EqualTo(appUser.PhoneNumber));
            Assert.That(user.City, Is.EqualTo(appUser.Address.City));
            Assert.That(user.Street, Is.EqualTo(appUser.Address.Street));
            Assert.That(user.StreetNumber, Is.EqualTo(appUser.Address.StreetNumber));
            Assert.That(user.Floor, Is.EqualTo(appUser.Address.Floor));
            Assert.That(user.PostCode, Is.EqualTo(appUser.Address.PostCode));
        }

        [Test]
        public void GetUserReturnsCorrectInfoWithoutAddress()
        {
            var appUser = new ApplicationUser()
            {
                FirstName = "Hristo",
                LastName = "Stoichkov",
            };

            var user = this.userService.GetUser(appUser);

            Assert.That(user, Is.Not.Null);
            Assert.That(user.FirstName, Is.EqualTo(appUser.FirstName));
            Assert.That(user.LastName, Is.EqualTo(appUser.LastName));
            Assert.That(user.Email, Is.EqualTo(appUser.Email));
            Assert.That(user.PhoneNumber, Is.EqualTo(appUser.PhoneNumber));
            Assert.That(user.City, Is.Null);
            Assert.That(user.Street, Is.Null);
            Assert.That(user.StreetNumber, Is.EqualTo(0));
            Assert.That(user.Floor, Is.EqualTo(0));
            Assert.That(user.PostCode, Is.Null);
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
