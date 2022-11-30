using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ReviewViewModels;
using System.Threading.Tasks;


namespace PizzaOrderingSystem.Services.Data
{
    public interface IReviewService
    {
        Task<AllReviewsViewModel> GetAllAsync();

        Task AddReview(CreateReviewInputModel review, string userId);

        Task DeleteReview(Review review);

        Task<int> GetAllReviewsCountAsync();

        Task<Review> GetByIdAsync(string id);
    }
}
