using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingSystem.Data.Models
{
    public class Address : BaseDeletableModel<string>
    {
        public Address()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int PostCode { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
