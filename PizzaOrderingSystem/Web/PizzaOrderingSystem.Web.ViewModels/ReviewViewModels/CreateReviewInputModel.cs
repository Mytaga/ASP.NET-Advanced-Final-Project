using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Web.ViewModels.ReviewViewModels
{
    public class CreateReviewInputModel : IMapTo<Review>
    {
        [Required(ErrorMessage = ReviewValidation.NameRequiredError)]
        [MinLength(ReviewValidation.AuthorNameMinLength, ErrorMessage = ReviewValidation.NameMinLengthError)]
        [MaxLength(ReviewValidation.AuthorNameMaxLength, ErrorMessage = ReviewValidation.NameMaxLengthError)]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = ReviewValidation.ContentRequiredError)]
        [MinLength(ReviewValidation.ContentMinLength, ErrorMessage = ReviewValidation.ContentMinLengthError)]
        [MaxLength(ReviewValidation.ContentMaxLength, ErrorMessage = ReviewValidation.ContentMaxLengthError)]
        public string Content { get; set; }

        public DateTime PublishedOn => DateTime.UtcNow;

        public string UserId { get; set; }
    }
}
