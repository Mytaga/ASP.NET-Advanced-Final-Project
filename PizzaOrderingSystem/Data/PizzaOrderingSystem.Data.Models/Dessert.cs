using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Data.Models
{
    public class Dessert : BaseDeletableModel<string>
    {
        public Dessert()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(DessertValidation.NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(DessertValidation.DescriptionMaxLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
