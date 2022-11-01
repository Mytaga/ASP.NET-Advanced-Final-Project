using System.Data;

namespace PizzaOrderingSystem.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Pizza Ordering System";

        public const string AdministratorRoleName = "Admin";
        public const string UserRoleName = "User";
        public const string ManagerRoleName = "Manager";

        public const string AdminEmail = "mitko.kralev@abv.bg";
        public const string ManagerEmail = "manager@onlinepizza.bg";

        public const string PizzaCategory = "Pizza";
        public const string SaladCategory = "Salad";
        public const string DessertCategory = "Dessert";
        public const string DrinkCategory = "Drink";
        public const string SauceCategory = "Sauce";

        public const string ProductController = "Product";
        public const string HomeController = "Home";
        public const string PaymentCardController = "PaymentCard";
        public const string AccountController = "Account";

        public const string IndexAction = "Index";
        public const string CreateAction = "Create";
        public const string ErrorAction = "Error";
        public const string AddAction = "Add";
        public const string ViewProfileAction = "ViewProfile";
    }
}