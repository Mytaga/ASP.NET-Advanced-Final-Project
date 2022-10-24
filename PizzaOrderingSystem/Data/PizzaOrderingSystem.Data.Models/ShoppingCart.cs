using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Data.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Items = new HashSet<CartItem>();
        }

        public string ShoppingCartId { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart() { ShoppingCartId = cartId };
        }
    }
}
