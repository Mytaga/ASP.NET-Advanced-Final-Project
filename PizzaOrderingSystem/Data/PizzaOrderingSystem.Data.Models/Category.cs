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
            this.Pizzas = new HashSet<Pizza>();
            this.Beverages = new HashSet<Beverage>();
            this.Desserts = new HashSet<Dessert>();
        }

        [Required]
        [MaxLength(CategoryValidation.NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }

        public virtual ICollection<Beverage> Beverages { get; set; }

        public virtual ICollection<Dessert> Desserts { get; set; }
    }
}
