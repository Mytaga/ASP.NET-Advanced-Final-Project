namespace PizzaOrderingSystem.UnitTests
{
    [TestFixture]
    public class OrderTests
    {
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<Order> orderRepo;
        private DbContextOptionsBuilder<ApplicationDbContext> options;
        private IOrderService orderService;
        private Mock<ICartService> cartServiceMock;
        private Mock<ISaleService> saleServiceMock;

        [SetUp]
        public void SetUp()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "PizzaTestDb");
            this.dbContext = new ApplicationDbContext(this.options.Options);
            this.orderRepo = new EfDeletableEntityRepository<Order>(this.dbContext);
            this.cartServiceMock = new Mock<ICartService>();
            this.saleServiceMock = new Mock<ISaleService>();
            this.orderService = new OrderService(orderRepo, cartServiceMock.Object, saleServiceMock.Object);          
        }

        [Test]
        public async Task AddAsyncWorkCorrect()
        {

        }
    }
}
