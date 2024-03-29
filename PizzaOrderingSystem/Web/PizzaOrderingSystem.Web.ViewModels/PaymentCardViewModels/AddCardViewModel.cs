﻿using System.Collections.Generic;
using static PizzaOrderingSystem.Common.ModelValidationConstants.CreditCardValidation;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.PaymentCardViewModels
{
    public class AddCardViewModel
    {
        [Required(ErrorMessage = CardNumberRequiredError)]
        [MinLength(CardNumberMinLength, ErrorMessage = CardNumberMinLengthError)]
        [MaxLength(CardNumberMaxLength, ErrorMessage = CardNumberMaxLengthError)]
        [CreditCard(ErrorMessage = CardNumberValidError)]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = ExpirationDateRequiredError)]
        [MinLength(ExpirationDateMinLength, ErrorMessage = ExpirationDateMinLengthError)]
        [MaxLength(ExpirationDateMaxLength, ErrorMessage = ExpirationDateMaxLengthError)]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = CardHolderRequiredError)]
        [MinLength(CardHolderMinLength, ErrorMessage = CardHolderMinLengthError)]
        [MaxLength(CardHolderMaxLength, ErrorMessage = CardHolderMaxLengthError)]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = CvcRequiredError)]
        [MinLength(CvcMinLength, ErrorMessage = CvcMinLengthError)]
        [MaxLength(CvcMaxLength, ErrorMessage = CvcMaxLengthError)]
        public string Cvc { get; set; }

        public string UserId { get; set; }

        public virtual IEnumerable<PaymentCardViewModel> SavedCards { get; set; }
    }
}