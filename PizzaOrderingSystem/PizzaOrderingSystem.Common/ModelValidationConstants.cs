namespace PizzaOrderingSystem.Common
{
    public static class ModelValidationConstants
    {
        public static class UserValidation
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 30;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 30;
            public const int UsernameMinLength = 1;
            public const int UsernameMaxLength = 30;

            public const string FirstNameRequired = "First name is required";
            public const string LastNameRequired = "Last name is required";
            public const string FirstNameMinLengthError = "First name must be at least 2 letters";
            public const string FirstNameMaxLengthError = "First name must be at no more than 30 letters";
            public const string LastNameMinLengthError = "Last name must be at least 2 letters";
            public const string LastNameMaxLengthError = "Last name must be at no more than 30 letters";
            public const string UsernameRequired = "Username is required";
            public const string UserNameMinLengthError = "Username must be at least 1 letters";
            public const string UserNameMaxLengthError = "Username must be at no more than 30 letters";
        }

        public static class ProductVadidation
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 30;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 100;
            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "79228162514264337593543950335";

            public const string NameRequiredError = "Name is required";
            public const string NameMinLengthError = "Last name must be at least 5 letters";
            public const string NameMaxLengthError = "Last name must be at no more than 30 letters";
            public const string PriceRequiredError = "Price is required";
            public const string DescriptionRequiredError = "Description is required";
            public const string DescriptionMinLengthError = "Description must be at least 10 letters";
            public const string DescriptionMaxLengthError = "Desctiption must be at no more than 100 letters";
            public const string ImageRequiredError = "Image is required";
        }

        public static class CategoryValidation
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const string NameRequiredError = "Name is required";
            public const string NameMinLengthError = "Last name must be at least 3 letters";
            public const string NameMaxLengthError = "Last name must be at no more than 50 letters";
        }

        public static class CreditCardValidation
        {
            public const int CardNumberMinLength = 13;
            public const int CardNumberMaxLength = 19;
            public const int ExpirationDateMinLength = 5;
            public const int ExpirationDateMaxLength = 5;
            public const int CardHolderMinLength = 5;
            public const int CardHolderMaxLength = 61;
            public const int CvcMinLength = 3;
            public const int CvcMaxLength = 3;
        }

        public static class ReviewValidation
        {
            public const int AuthorNameMinLength = 5;
            public const int AuthorNameMaxLength = 60;
            public const int ContentMinLength = 20;
            public const int ContentMaxLength = 600;

            public const string NameRequiredError = "Name is required";
            public const string NameMinLengthError = "Last name must be at least 5 letters";
            public const string NameMaxLengthError = "Last name must be at no more than 60 letters";

            public const string ContentRequiredError = "Content is required";
            public const string ContentMinLengthError = "Content must be at least 20 letters";
            public const string ContentMaxLengthError = "Content must be at no more than 600 letters";
        }

        public static class ShopValidation
        {
            public const int ShopNameMaxLength = 60;
            public const int ShopDescriptionMaxLength = 150;
        }
    }
}