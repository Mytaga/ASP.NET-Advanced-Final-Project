using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PizzaOrderingSystem.Common.ModelValidationConstants.ShopValidation;

namespace PizzaOrderingSystem.Data.Models
{
    public class Shop : BaseDeletableModel<string>
    {
        public Shop()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(ShopNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ShopDescriptionMaxLength)]
        public string Description { get; set; }

        [MaxLength(ShopPhoneMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [ForeignKey(nameof(Address))]
        public string AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}