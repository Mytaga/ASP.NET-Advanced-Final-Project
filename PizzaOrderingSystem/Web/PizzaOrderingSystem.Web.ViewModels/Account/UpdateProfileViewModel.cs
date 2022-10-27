using AutoMapper;
using Microsoft.AspNetCore.Http;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.Account
{
    public class UpdateProfileViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
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

        public int PostCode { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                 .ForMember(d => d.City, mo => mo.MapFrom(s => s.Address.City));
            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                 .ForMember(d => d.Street, mo => mo.MapFrom(s => s.Address.Street));
            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                 .ForMember(d => d.StreetNumber, mo => mo.MapFrom(s => s.Address.StreetNumber));
            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                 .ForMember(d => d.Floor, mo => mo.MapFrom(s => s.Address.Floor));
            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                 .ForMember(d => d.PostCode, mo => mo.MapFrom(s => s.Address.PostCode));
        }
    }
}
