using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using static PizzaOrderingSystem.Common.ModelValidationConstants;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class CreateProductInputModel : IMapTo<Product>
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
        public IFormFile ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
