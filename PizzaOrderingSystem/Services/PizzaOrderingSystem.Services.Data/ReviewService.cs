using Microsoft.EntityFrameworkCore;
using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using PizzaOrderingSystem.Web.ViewModels.ReviewViewModels;
using System;
using System.Globalization;
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

        public async Task AddReview(CreateReviewInputModel model, string userId)
        {
            Review review = new Review()
            {
                AuthorName = model.AuthorName,
                Content= model.Content,
                PublishedOn = model.PublishedOn,
                UserId = userId,
            };

            await this.reviewRepo.AddAsync(review);
            await this.reviewRepo.SaveChangesAsync();
        }

        public async Task DeleteReview(Review review)
        {
            this.reviewRepo.Delete(review);
            await this.reviewRepo.SaveChangesAsync();
        }

        public async Task<AllReviewsViewModel> GetAllAsync()
        {
            var reviews = this.reviewRepo.AllAsNoTracking();

            AllReviewsViewModel viewModel = new AllReviewsViewModel()
            {
                Reviews = await reviews.Select(r => new ReviewViewModel()
                {
                    AuthorName = r.AuthorName,
                    Content = r.Content,
                    PublishedOn = r.PublishedOn.ToString("f", CultureInfo.InvariantCulture),
                    UserId = r.UserId,
                })
                .ToListAsync(),
            };

            return viewModel;
        }

		public async Task<int> GetAllReviewsCountAsync()
		{
            return await this.reviewRepo
                .AllAsNoTracking()
                .CountAsync();
		}

        public Task<Review> GetByIdAsync(string id)
        {
            return this.reviewRepo
                .All()
                .FirstOrDefaultAsync(r => r.Id == id);  
        }
    }
}
