using PizzaOrderingSystem.Data.Common.Repositories;
using PizzaOrderingSystem.Data.Models;
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

        public async Task EditReview(Review review)
        {
            this.reviewRepo.Update(review);
            await this.reviewRepo.SaveChangesAsync();
        }

        public IQueryable<Review> GetAll()
        {
            return this.reviewRepo.All();
        }
    }
}
