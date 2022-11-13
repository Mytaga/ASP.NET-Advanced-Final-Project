using static PizzaOrderingSystem.Common.ModelValidationConstants.AddressValidation;
using static PizzaOrderingSystem.Common.ModelValidationConstants.ShopValidation;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.ShopViewModels
{
    public class CreateShopViewModel
    {
        [Required(ErrorMessage = NameRequiredError)]
        [MinLength(ShopNameMinLength, ErrorMessage = NameMinLengthError)]
        [MaxLength(ShopNameMaxLength, ErrorMessage = NameMaxLengthError)]
        public string Name { get; set; }

        [Required(ErrorMessage = DescriptionRequiredError)]
        [MinLength(ShopDescriptionMinLength, ErrorMessage = DescriptionMinLengthError)]
        [MaxLength(ShopDescriptionMaxLength, ErrorMessage = DescriptionMaxLengthError)]
        public string Description { get; set; }

        [MinLength(ShopPhoneMinLength, ErrorMessage = PhoneMinLengthError)]
        [MaxLength(ShopPhoneMaxLength, ErrorMessage = PhoneMaxLengthError)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = CityRequiredError)]
        [MinLength(CityMinLength, ErrorMessage = CityMinLengthError)]
        [MaxLength(CityMaxLength, ErrorMessage = CityMaxLengthError)]
        public string City { get; set; }

        [Required(ErrorMessage = StreetRequiredError)]
        [MinLength(StreetMinLength, ErrorMessage = StreetMinLengthError)]
        [MaxLength(StreetMaxLength, ErrorMessage = StreetMaxLengthError)]
        public string Street { get; set; }

        [Required]
        [Range(FloorMinValue, FloorMaxValue, ErrorMessage = FloorRangeLengthError)]
        public int StreetNumber { get; set; }
    }
}
