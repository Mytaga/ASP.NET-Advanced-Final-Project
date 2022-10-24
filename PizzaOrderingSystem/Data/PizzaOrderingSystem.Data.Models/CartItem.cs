using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrderingSystem.Data.Models
{
    public class CartItem : BaseDeletableModel<string>
    {
        public CartItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int Quantity { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string ShoppingCartId { get; set; }
    }
}
