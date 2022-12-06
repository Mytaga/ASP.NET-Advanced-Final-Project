﻿using System.Data;

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

        public const string ProductController = "Product";
        public const string HomeController = "Home";
        public const string PaymentCardController = "PaymentCard";
        public const string AccountController = "Account";
        public const string OrderController = "Order";
        public const string ShopController = "Shop";

        public const string IndexAction = "Index";
        public const string CreateAction = "Create";
        public const string ErrorAction = "Error";
        public const string AddAction = "Add";
        public const string ViewProfileAction = "ViewProfile";
        public const string ConfirmOrderAction = "Confirm";
        public const string OrderDetailsAction = "Details";
        public const string UpdateProfileAction = "Update";
        public const string LoginAction = "Login";

        public const string ShopImageUrl = "/img/PizzaShopLogo.jpg";
        public const string ShopImageExternalUrl = "C:/Users/mitko/Downloads/ASP.NET-Advanced-Final-Project/PizzaOrderingSystem/Web/PizzaOrderingSystem.Web/wwwroot/img/PizzaShopLogo.jpg";
        public const string MasterCardLogo = "https://img.icons8.com/color/48/000000/mastercard-logo.png";

        public const string HomeImageUrl1 = "img/CarouselPhoto.jpg";
        public const string HomeImageUrl2 = "img/C1.jpg";
        public const string HomeImageUrl3 = "img/C2.jpg";

        public const string TempDataSuccess = "successMessage";
    }
}