﻿using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddToCartAsync(Product product)
        {
            if (this.cartItemRepo.All().Any(i => i.ProductId == product.Id && i.ShoppingCartId == shoppingCart.ShoppingCartId))
            {
                var item = await this.cartItemRepo.All().FirstOrDefaultAsync(i => i.ProductId == product.Id && i.ShoppingCartId == shoppingCart.ShoppingCartId);
                item.Quantity++;
                this.cartItemRepo.Update(item);
            }
            else
            {
                var shoppingCartItem = new CartItem()
                {
                    ShoppingCartId = this.shoppingCart.ShoppingCartId,
                    Quantity = 1,
                    Product = product,
                    ProductId = product.Id,
                };

                await this.cartItemRepo.AddAsync(shoppingCartItem);
            }

            await this.cartItemRepo.SaveChangesAsync();
        }

        public async Task ClearCartAsync()
        {
            var cartItems = await this.cartItemRepo
                .All()
                .Where(ci => ci.ShoppingCartId == this.shoppingCart.ShoppingCartId)
                .ToListAsync();

            foreach (var item in cartItems)
            {
                item.ShoppingCartId = null;
            }

            await this.cartItemRepo.SaveChangesAsync();
        }

        public async Task DecreaseQuantity(CartItem item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                this.cartItemRepo.Update(item);
            }
            else
            {
                this.shoppingCart.Items.Remove(item);
                this.cartItemRepo.Delete(item);
            }

            await this.cartItemRepo.SaveChangesAsync();
        }

        public async Task IncreaseQuantity(CartItem item)
        {
            item.Quantity++;
            this.cartItemRepo.Update(item);

            await this.cartItemRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            return await this.cartItemRepo
                .All()
                .Where(ci => ci.ShoppingCartId == this.shoppingCart.ShoppingCartId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCartProductsAsync()
        {
            return await this.cartItemRepo
                .All()
                .Where(ci => ci.ShoppingCartId == this.shoppingCart.ShoppingCartId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task RemoveFromCartAsync(CartItem item)
        {
            this.shoppingCart.Items.Remove(item);
            this.cartItemRepo.Delete(item);
            await this.cartItemRepo.SaveChangesAsync();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = this.cartItemRepo.All()
                .Where(ci => ci.ShoppingCartId == this.shoppingCart.ShoppingCartId)
                .Include(p => p.Product)
                .Select(ci => ci.Product.Price * ci.Quantity).Sum();

            return total;
        }

        public int GetShoppingCartItemCount()
        {
            return this.cartItemRepo
                .All()
                .Where(ci => ci.ShoppingCartId == this.shoppingCart.ShoppingCartId)
                .Include(p => p.Product)
                .Sum(ci => ci.Quantity);               
        }

        public ShoppingCartViewModel GetShoppingCart(IEnumerable<CartItem> items)
        {
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();

            var allViewItems = new HashSet<CartItemViewModel>();

            foreach (var item in items)
            {
                var viewItem = new CartItemViewModel()
                {
                    Id = item.Id,
                    ItemName = item.Product.Name,
                    ItemPrice = item.Product.Price,
                    Quantity = item.Quantity,
                    ImageUrl = item.Product.ImageUrl,
                };

                allViewItems.Add(viewItem);
            }

            viewModel.CartItems = allViewItems;

            return viewModel;
        }
    }
}