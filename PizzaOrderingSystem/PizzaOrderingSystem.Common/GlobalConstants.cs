using System.Data;

namespace PizzaOrderingSystem.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "PIZZA ORDERING SYSTEM";
        public const string SystemNameAdmin = "PIZZA ORDERING SYSTEM ADMINISTRATION AREA";

        public const string AdministratorRoleName = "Admin";
        public const string UserRoleName = "User";
        public const string ManagerRoleName = "Manager";

        public const string AdministrationArea = "Administration";
        public const string ManagerArea = "Manager";

        public const string AdminEmail = "admin@onlinepizza.bg";
        public const string ManagerEmail = "manager@onlinepizza.bg";

        public const string PizzaCategory = "Pizza";
        public const string SaladCategory = "Salad";
        public const string DessertCategory = "Dessert";
        public const string DrinkCategory = "Drink";
        public const string SauceCategory = "Sauce";
        public const string PastaCategory = "Pasta";

        public const string ProductController = "Product";
        public const string HomeController = "Home";
        public const string PaymentCardController = "PaymentCard";
        public const string AccountController = "Account";
        public const string OrderController = "Order";
        public const string ShopController = "Shop";
        public const string ShoppingCartController = "ShoppingCart";

        public const string IndexAction = "Index";
        public const string CreateAction = "Create";
        public const string ErrorAction = "Error";
        public const string AddAction = "Add";
        public const string EditAction = "Edit";
        public const string ViewProfileAction = "ViewProfile";
        public const string ConfirmOrderAction = "Confirm";
        public const string OrderDetailsAction = "Details";
        public const string UpdateProfileAction = "Update";
        public const string LoginAction = "Login";
        public const string DetailsAction = "Details";
        public const string UserOrdersAction = "UserOrders";
        public const string RemoveAction = "Remove";
        public const string DeleteAction = "Delete";

        public const string ShopImageUrl = "/img/PizzaShopLogo.jpg";
        public const string ShopImageExternalUrl = "https://static.vecteezy.com/system/resources/thumbnails/006/886/615/small/pizza-logo-cartoon-this-logo-is-highly-suitable-for-any-pizza-related-restaurant-fast-food-delivery-bistro-catering-and-italian-food-related-businesses-vector.jpg";
        public const string MasterCardLogo = "https://img.icons8.com/color/48/000000/mastercard-logo.png";
        public const string VisaCardLogo = "https://img.icons8.com/color/48/000000/visa.png";

        public const string HomeImageUrl1 = "img/CarouselPhoto.jpg";
        public const string HomeImageUrl2 = "img/C1.jpg";
        public const string HomeImageUrl3 = "img/C2.jpg";

        public const string TempDataSuccess = "successMessage";
        public const string TempDataError = "errorMessage";

        public const string BlobConnectionString = "DefaultEndpointsProtocol=https;AccountName=pizzaorderingsystem;AccountKey=wyq1dgqhETYTrOZGWqF0+Z/tBpT3j9NTL4nGEUN1EXrsiTb4GOjWPl6lzEFUtIHc+/pijBVdwbdg+AStep4DjQ==;EndpointSuffix=core.windows.net";
        public const string BlobContainer = "pizzaorderingsystem";

        public const string SendGridApiKey = "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";
        public const string RegisterConfirmContent = "Congratulations! You have registered succesfully to our Online Pizza!";
        public const string RegisterConfirmSubject = "Registration Confirm";
        public const string SendGridEmail = "mitko.kralev@abv.bg";
    }
}