using PizzaOrderingSystem.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IReviewService
    {
        IQueryable<Review> GetAll();

        Task AddReview(Review review);

        Task DeleteReview(Review review);

        Task EditReview(Review review);

        Task<Review> GetByIdAsync(string id);

        Review GetById(string id);  
    }
}
