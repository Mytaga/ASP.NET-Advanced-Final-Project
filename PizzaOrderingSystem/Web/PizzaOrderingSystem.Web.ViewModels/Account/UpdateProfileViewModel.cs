using Microsoft.AspNetCore.Http;
using PizzaOrderingSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.Account
{
    public class UpdateProfileViewModel
    {
        [MinLength(ModelValidationConstants.UserValidation.PhoneNumberMinLength, ErrorMessage = ModelValidationConstants.UserValidation.PhoneNumberMinLengthError)]
        [MaxLength(ModelValidationConstants.UserValidation.PhoneNumberMaxLength, ErrorMessage = ModelValidationConstants.UserValidation.PhoneNumberMaxLengthError)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageUrl { get; set; }

        [Required(ErrorMessage = ModelValidationConstants.AddressValidation.CityRequiredError)]
        [MinLength(ModelValidationConstants.AddressValidation.CityMinLength, ErrorMessage = ModelValidationConstants.AddressValidation.CityMinLengthError)]
        [MaxLength(ModelValidationConstants.AddressValidation.CityMaxLength, ErrorMessage = ModelValidationConstants.AddressValidation.CityMaxLengthError)]
        public string City { get; set; }

        [Required(ErrorMessage = ModelValidationConstants.AddressValidation.StreetRequiredError)]
        [MinLength(ModelValidationConstants.AddressValidation.StreetMinLength, ErrorMessage = ModelValidationConstants.AddressValidation.StreetMinLengthError)]
        [MaxLength(ModelValidationConstants.AddressValidation.StreetMaxLength, ErrorMessage = ModelValidationConstants.AddressValidation.StreetMaxLengthError)]
        public string Street { get; set; }

        [Range(ModelValidationConstants.AddressValidation.StreetNumberMinValue, ModelValidationConstants.AddressValidation.StreetNumberMaxValue, ErrorMessage = ModelValidationConstants.AddressValidation.StreetNumberRangeLengthError)]
        public int StreetNumber { get; set; }

        [Range(ModelValidationConstants.AddressValidation.FloorMinValue, ModelValidationConstants.AddressValidation.FloorMaxValue, ErrorMessage = ModelValidationConstants.AddressValidation.FloorRangeLengthError)]
        public int Floor { get; set; }

        [MinLength(ModelValidationConstants.AddressValidation.PostalCodeMinLength, ErrorMessage = ModelValidationConstants.AddressValidation.PostalCodeMinLengthError)]
        [MaxLength(ModelValidationConstants.AddressValidation.PostalCodeMaxLength, ErrorMessage = ModelValidationConstants.AddressValidation.PostalCodeMaxLengthError)]
        public string PostCode { get; set; }
    }
}
