using PizzaOrderingSystem.Data.Common.Models;
using System;
using static PizzaOrderingSystem.Common.ModelValidationConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingSystem.Data.Models
{
    public class Beverage : BaseDeletableModel<string>
    {
        public Beverage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(BeverageValidation.NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
