using PizzaOrderingSystem.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrderingSystem.Web.ViewModels.Manager.Sales
{
    public class UpdateSaleViewModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public string OrderId { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }
    }
}
