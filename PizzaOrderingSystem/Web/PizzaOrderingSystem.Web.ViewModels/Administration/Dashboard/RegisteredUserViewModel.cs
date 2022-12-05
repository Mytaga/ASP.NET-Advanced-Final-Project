using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.ViewModels.Administration.Dashboard
{
    public class RegisteredUserViewModel
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int OrdersMade { get; set; }
    }
}
