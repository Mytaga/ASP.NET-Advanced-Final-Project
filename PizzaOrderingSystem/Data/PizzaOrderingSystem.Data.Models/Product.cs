﻿using PizzaOrderingSystem.Data.Common.Models;
using PizzaOrderingSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Data.Models
{
    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(ProductVadidation.NameMaxLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(ProductVadidation.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
