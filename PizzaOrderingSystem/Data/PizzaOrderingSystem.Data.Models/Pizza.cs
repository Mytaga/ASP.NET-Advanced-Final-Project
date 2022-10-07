using PizzaOrderingSystem.Data.Common.Models;
using PizzaOrderingSystem.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Data.Models
{
    public class Pizza : BaseDeletableModel<string>
    {
        public Pizza()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(PizzaVadidation.NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(PizzaVadidation.DescriptionMaxLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public TypeOfDough Dought { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
