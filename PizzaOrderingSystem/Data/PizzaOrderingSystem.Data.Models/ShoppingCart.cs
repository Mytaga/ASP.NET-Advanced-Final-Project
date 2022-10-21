using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PizzaOrderingSystem.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Data.Models
{
    public class ShoppingCart : BaseDeletableModel<string>
    { 
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<CartItem>();
        }

        public virtual ICollection<CartItem> Items { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart() { Id = cartId };
        }
    }
}
