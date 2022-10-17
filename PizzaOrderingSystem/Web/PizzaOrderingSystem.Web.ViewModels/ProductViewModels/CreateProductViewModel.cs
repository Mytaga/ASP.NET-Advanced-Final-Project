﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = ProductVadidation.NameRequiredError)]
        [MinLength(ProductVadidation.NameMinLength)]
        [MaxLength(ProductVadidation.NameMaxLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ProductVadidation.PriceRequiredError)]
        [Range(typeof(decimal), ProductVadidation.PriceMinValue, ProductVadidation.PriceMaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ProductVadidation.DescriptionRequiredError)]
        [MinLength(ProductVadidation.DescriptionMinLength, ErrorMessage = ProductVadidation.DescriptionMinLengthError)]
        [MaxLength(ProductVadidation.DescriptionMaxLength, ErrorMessage = ProductVadidation.DescriptionMaxLengthError)]
        public string Description { get; set; }

        [Required(ErrorMessage = ProductVadidation.ImageRequiredError)]
        [Display(Name = "Image")]
        public IFormFile ImageUrl { get; set; }

        public ICollection<ListProductCategoriesViewModel> Categories { get; set; }
    }
}