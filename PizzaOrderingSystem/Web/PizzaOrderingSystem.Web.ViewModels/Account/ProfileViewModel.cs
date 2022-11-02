using AutoMapper;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.Account
{
    public class ProfileViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string ImageUrl { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int Floor { get; set; }

        public string PostCode { get; set; }

        public virtual ICollection<CreditCard> CreditCards => new HashSet<CreditCard>();

        public virtual ICollection<Order> Orders => new HashSet<Order>();

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
