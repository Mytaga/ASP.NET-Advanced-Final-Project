using PizzaOrderingSystem.Data.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(CategoryValidation.NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
