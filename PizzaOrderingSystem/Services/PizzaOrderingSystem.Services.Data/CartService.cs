using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using System;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class CartService : ICartService
    {
        private readonly IDeletableEntityRepository<CartItem> cartItemRepo;
        private ShoppingCart shoppingCart;

        public CartService(IDeletableEntityRepository<CartItem> cartItemRepo, ShoppingCart shoppingCart)
        {
            this.cartItemRepo = cartItemRepo;
            this.shoppingCart = shoppingCart;
        }

        public async Task AddToCartAsync(Product product, int quantity)
        {
            var shoppingCartItem = await this.cartItemRepo
                .All()
                .FirstOrDefaultAsync(i => i.Product.Id == product.Id && i.ShoppingCartId == this.shoppingCart.ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem()
                {
                    ShoppingCartId = this.shoppingCart.ShoppingCartId,
                    Quantity = 1,
                    Product = product,
                    ProductId = product.Id,
                };

                await this.cartItemRepo.AddAsync(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }

            await this.cartItemRepo.SaveChangesAsync();
        }

        public Task RemoveFromCartAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
