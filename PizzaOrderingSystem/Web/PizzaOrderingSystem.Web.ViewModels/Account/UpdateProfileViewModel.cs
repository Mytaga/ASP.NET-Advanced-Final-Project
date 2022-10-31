using Microsoft.AspNetCore.Http;
using static PizzaOrderingSystem.Common.ModelValidationConstants.UserValidation;
using static PizzaOrderingSystem.Common.ModelValidationConstants.AddressValidation;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.Account
{
    public class UpdateProfileViewModel
    {
        [MinLength(PhoneNumberMinLength, ErrorMessage = PhoneNumberMinLengthError)]
        [MaxLength(PhoneNumberMaxLength, ErrorMessage = PhoneNumberMaxLengthError)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageUrl { get; set; }

        [Required(ErrorMessage = CityRequiredError)]
        [MinLength(CityMinLength, ErrorMessage = CityMinLengthError)]
        [MaxLength(CityMaxLength, ErrorMessage = CityMaxLengthError)]
        public string City { get; set; }

        [Required(ErrorMessage = StreetRequiredError)]
        [MinLength(StreetMinLength, ErrorMessage = StreetMinLengthError)]
        [MaxLength(StreetMaxLength, ErrorMessage = StreetMaxLengthError)]
        public string Street { get; set; }

        [Range(StreetNumberMinValue, StreetNumberMaxValue, ErrorMessage = StreetNumberRangeLengthError)]
        public int StreetNumber { get; set; }

        [Range(FloorMinValue, FloorMaxValue, ErrorMessage = FloorRangeLengthError)]
        public int Floor { get; set; }

        [MinLength(PostalCodeMinLength, ErrorMessage = PostalCodeMinLengthError)]
        [MaxLength(PostalCodeMaxLength, ErrorMessage = PostalCodeMaxLengthError)]
        public string PostCode { get; set; }
    }
}