using AutoMapper;
using Microsoft.AspNetCore.Http;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.Account
{
    public class UpdateProfileViewModel 
    {
        [Phone]
        [MinLength(ModelValidationConstants.UserValidation.PhoneNumberMinLength, ErrorMessage = ModelValidationConstants.UserValidation.PhoneNumberMinLengthError)]
        [MaxLength(ModelValidationConstants.UserValidation.PhoneNumberMaxLength, ErrorMessage = ModelValidationConstants.UserValidation.PhoneNumberMaxLengthError)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageUrl { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int Floor { get; set; }

        public string PostCode { get; set; }
    }
}
