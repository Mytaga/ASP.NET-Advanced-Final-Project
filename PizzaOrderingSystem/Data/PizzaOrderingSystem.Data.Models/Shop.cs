using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingSystem.Data.Models
{
    public class Shop : BaseDeletableModel<string>
    {
        public Shop()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Sales = new HashSet<Sale>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Address))]
        public string AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}