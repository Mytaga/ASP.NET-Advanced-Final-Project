﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(ConfirmPassword))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = UserValidation.UsernameRequired)]
        //[MinLength(UserValidation.UsernameMinLength, ErrorMessage = UserValidation.UserNameMinLengthError)]
        //[MaxLength(UserValidation.UsernameMaxLength, ErrorMessage = UserValidation.UserNameMaxLengthError)]
        //public string UserName { get; set; }

        [Required(ErrorMessage = UserValidation.FirstNameRequired)]
        [MinLength(UserValidation.FirstNameMinLength, ErrorMessage = UserValidation.FirstNameMinLengthError)]
        [MaxLength(UserValidation.FirstNameMaxLength, ErrorMessage = UserValidation.FirstNameMaxLengthError)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = UserValidation.LastNameRequired)]
        [MinLength(UserValidation.LastNameMinLength, ErrorMessage = UserValidation.LastNameMinLengthError)]
        [MaxLength(UserValidation.LastNameMaxLength, ErrorMessage = UserValidation.LastNameMaxLengthError)]
        public string LastName { get; set; }

    }
}