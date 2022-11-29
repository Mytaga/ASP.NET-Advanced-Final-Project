using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Web.ViewModels.ReviewViewModels
{
    public class CreateReviewViewModel
    {
        [Required(ErrorMessage = ReviewValidation.NameRequiredError)]
        [MinLength(ReviewValidation.AuthorNameMinLength, ErrorMessage = ReviewValidation.NameMinLengthError)]
        [MaxLength(ReviewValidation.AuthorNameMaxLength, ErrorMessage = ReviewValidation.NameMaxLengthError)]
        [DisplayName("Author")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = ReviewValidation.ContentRequiredError)]
        [MinLength(ReviewValidation.ContentMinLength, ErrorMessage = ReviewValidation.ContentMinLengthError)]
        [MaxLength(ReviewValidation.ContentMaxLength, ErrorMessage = ReviewValidation.ContentMaxLengthError)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
