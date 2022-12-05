using System.Globalization;

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
        public async Task AddAsyncWorksCorrect()
        {
            var model = this.CreateModel();

            await this.orderService.AddAsync(model);

            var order = await this.orderRepo.AllAsNoTracking().FirstOrDefaultAsync(o => o.TotalPrice.ToString() == model.TotalPrice);

            Assert.That(this.orderRepo.AllAsNoTracking().Count(), Is.EqualTo(1));
            Assert.That(order, Is.Not.Null);
            Assert.That(order.DeliveryType, Is.EqualTo(model.DeliveryType));
            Assert.That(order.PaymentType, Is.EqualTo(model.PaymentType));
            Assert.That(order.Status, Is.EqualTo(model.Status));
            Assert.That(order.UserId, Is.EqualTo(model.UserId));
        }

        [Test]
        public async Task GetAllOrdersAsyncReturnsCorrect()
        {
            await this.FlushCollection();
            await this.FillCollection();

            var orders = await this.orderService.GetAllOrdersAsync();

            Assert.That(orders, Is.EqualTo(3));
        }

        [Test]
        public async Task GetAllOrdersAsyncReturnsCorrectZero()
        {
            await this.FlushCollection();
        
            var orders = await this.orderService.GetAllOrdersAsync();

            Assert.That(orders, Is.EqualTo(0));
        }

        [Test]
        public async Task GetLastOrderAsyncReturnsCorrectOrder()
        {
            await this.FlushCollection();
            await this.FillCollection();

            var model = this.CreateModel();

            await this.orderService.AddAsync(model);

            var order = await this.orderService.GetLastOrderAsync();

            var lastOrder = this.orderRepo.All().Last();

            Assert.That(order, Is.Not.Null);
            Assert.That(order, Is.EqualTo(lastOrder));
            Assert.That(order.DeliveryType, Is.EqualTo(model.DeliveryType));
            Assert.That(order.PaymentType, Is.EqualTo(model.PaymentType));
            Assert.That(order.Status, Is.EqualTo(model.Status));
            Assert.That(order.UserId, Is.EqualTo(model.UserId));
        }

        [Test]
        public async Task GetLastOrderAsyncReturnsNull()
        {
            await this.FlushCollection();
           
            var order = await this.orderService.GetLastOrderAsync();

            Assert.That(order, Is.Null);
        }

        [Test]
        public async Task GetOrderDetailsAsyncWorksCorrect()
        {
            await this.FlushCollection();
            await this.FillCollection();

            var order = await this.orderService.GetLastOrderAsync();
            var detailsModel = this.orderService.GetOrderDetails(order);

            Assert.That(detailsModel.OrderId, Is.EqualTo(order.Id));
            Assert.That(detailsModel.TimeOfOrder, Is.EqualTo(order.TimeOfOrder.ToString("f", CultureInfo.InvariantCulture)));
            Assert.That(detailsModel.TotalPrice, Is.EqualTo(order.TotalPrice.ToString("C")));
            Assert.That(detailsModel.DeliveryType, Is.EqualTo(order.DeliveryType.ToString()));
            Assert.That(detailsModel.Status, Is.EqualTo(order.Status.ToString()));
            Assert.That(detailsModel.PaymentType, Is.EqualTo(order.PaymentType.ToString()));
            Assert.That(detailsModel.RecipientCity, Is.EqualTo(order.User.Address.City));
        }

        [Test]
        public async Task GetUserOrderDetailsAsyncWorksCorrect()
        {
            await this.FlushCollection();
            await this.FillCollection();

            var order = await this.orderService.GetLastOrderAsync();
            var detailsModel = await this.orderService.GetUserOrderDetailsAsync(order.UserId, order.Id);

            Assert.That(detailsModel.OrderId, Is.EqualTo(order.Id));
            Assert.That(detailsModel.TimeOfOrder, Is.EqualTo(order.TimeOfOrder.ToString("f", CultureInfo.InvariantCulture)));
            Assert.That(detailsModel.TotalPrice, Is.EqualTo(order.TotalPrice.ToString("C")));
            Assert.That(detailsModel.DeliveryType, Is.EqualTo(order.DeliveryType.ToString()));
            Assert.That(detailsModel.Status, Is.EqualTo(order.Status.ToString()));
            Assert.That(detailsModel.PaymentType, Is.EqualTo(order.PaymentType.ToString()));
        }

        private CreateOrderViewModel CreateModel()
        {
            var model = new CreateOrderViewModel()
            {
                TotalPrice = "20",
                PaymentType = Data.Models.Enums.PaymentType.Cash,
                City = "sofia",
                Street = "street",
                StreetNumber = 2,
                Floor = 2,
                UserId = "1",
            };

            return model;
        }

        private async Task FlushCollection()
        {
            var orders = await this.orderRepo.All().ToListAsync();

            foreach (var order in orders)
            {
                this.orderRepo.Delete(order);
            }

            await this.dbContext.SaveChangesAsync();
        }

        private async Task FillCollection()
        {
            var address = new Address()
            {
                City = "sofia",
                Street = "street",
                StreetNumber = 2,
                Floor = 1,
                PostCode = "1",
            };

            await this.dbContext.Addresses.AddAsync(address);

            var user = new ApplicationUser()
            {
                FirstName = "Test",
                LastName = "Test",
                Address = address,
            };

            await this.dbContext.Users.AddAsync(user);

            var orders = new List<Order>()
            {
                new Order() { TotalPrice = 30M, PaymentType = Data.Models.Enums.PaymentType.Cash, Status = Data.Models.Enums.OrderStatus.Active, DeliveryType = Data.Models.Enums.OrderType.OnSite, UserId = user.Id},
                new Order() { TotalPrice = 50M, PaymentType = Data.Models.Enums.PaymentType.Cash, Status = Data.Models.Enums.OrderStatus.Active, DeliveryType = Data.Models.Enums.OrderType.OnSite, UserId = user.Id},
                new Order() { TotalPrice = 10M, PaymentType = Data.Models.Enums.PaymentType.Cash, Status = Data.Models.Enums.OrderStatus.Active, DeliveryType = Data.Models.Enums.OrderType.OnSite, UserId = user.Id},
            };

            await this.dbContext.AddRangeAsync(orders);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
