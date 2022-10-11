using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PizzaOrderingSystem.Common.ModelValidationConstants;

namespace PizzaOrderingSystem.Data.Models
{
    public class CreditCard : BaseDeletableModel<string>
    {
        public CreditCard()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(CreditCardValidation.CardNumberMaxLength)]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(CreditCardValidation.ExpirationDateMaxLength)]
        public string ExpirationDate { get; set; }

        [Required]
        [MaxLength(CreditCardValidation.CardHolderMaxLength)]
        public string CardHolder { get; set; }

        [Required]
        [MaxLength(CreditCardValidation.CvcMaxLength)]
        public string Cvc { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
