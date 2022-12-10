using System.Data;

namespace PizzaOrderingSystem.Common
{
    public static class ErrorConstants
    {
        public const string InvalidLogin = "Invalid login";
        public const string OrderMissing = "Order doesn't exist!";
        public const string AddressMissing = "Address is missing ! Please update your profile!";
        public const string RegisterFail = "You failed to register!";
        public const string UnauthorizedAction = "Please sign in or register!";
        public const string UnexistingProduct = "Product doesn't exist!";
        public const string EmptyCart = "Shopping cart is empty! Please add products to confirm your order!";
        public const string ExistingCategory = "Category name already exists! Choose a different one!";
        public const string UnexistingCategory = "Category doesn't exist!";

        public const string ExceptionMessage = "Something went wrong!";
    }
}
