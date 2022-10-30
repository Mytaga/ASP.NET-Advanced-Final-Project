﻿using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ReviewViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepo;

        public ReviewService(IDeletableEntityRepository<Review> reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }

        public async Task AddReview(Review review)
        {
            await this.reviewRepo.AddAsync(review);
            await this.reviewRepo.SaveChangesAsync();
        }

        public async Task DeleteReview(Review review)
        {
            this.reviewRepo.Delete(review);
            await this.reviewRepo.SaveChangesAsync();
        }

        public async Task<AllReviewsViewModel> GetAll()
        {
            var reviews = this.reviewRepo.AllAsNoTracking();
            AllReviewsViewModel viewModel = new AllReviewsViewModel()
            {
                Reviews = await reviews.To<ReviewViewModel>().ToListAsync(),
            };

            return viewModel;
        }

        public Review GetById(string id)
        {
            return this.reviewRepo.All().FirstOrDefault(r => r.Id == id);
        }

        public async Task<Review> GetByIdAsync(string id)
        {
            return await this.reviewRepo.All().FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
