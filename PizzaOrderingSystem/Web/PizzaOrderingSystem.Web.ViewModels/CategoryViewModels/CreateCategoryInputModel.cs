using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using static PizzaOrderingSystem.Common.ModelValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.CategoryViewModels
{
    public class CreateCategoryInputModel : IMapTo<Category>
    {

        [Required(ErrorMessage = CategoryValidation.NameRequiredError)]
        [MinLength(CategoryValidation.NameMinLength, ErrorMessage = CategoryValidation.NameMinLengthError)]
        [MaxLength(CategoryValidation.NameMaxLength, ErrorMessage = CategoryValidation.NameMaxLengthError)]
        public string Name { get; set; }
    }
}
