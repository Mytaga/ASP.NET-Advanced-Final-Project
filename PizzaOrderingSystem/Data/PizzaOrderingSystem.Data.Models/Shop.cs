using PizzaOrderingSystem.Common;
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
        [MaxLength(ModelValidationConstants.ShopValidation.ShopNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.ShopValidation.ShopDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Address))]
        public string AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}