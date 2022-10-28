using PizzaOrderingSystem.Data.Models;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.Account
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int Floor { get; set; }

        public string PostCode { get; set; }

        public virtual CreditCard CreditCard { get; set; }

        public virtual ICollection<Order> Orders => new HashSet<Order>();
    }
}
