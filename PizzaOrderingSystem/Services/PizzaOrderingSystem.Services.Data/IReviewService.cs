using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Web.ViewModels.ReviewViewModels;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IReviewService
    {
        Task<AllReviewsViewModel> GetAll();

        Task AddReview(Review review);

        Task DeleteReview(Review review);

        Task<Review> GetByIdAsync(string id);

        Review GetById(string id);
    }
}
