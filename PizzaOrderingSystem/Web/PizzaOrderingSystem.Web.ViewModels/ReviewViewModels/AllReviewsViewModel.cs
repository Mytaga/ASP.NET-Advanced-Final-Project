using System.Collections.Generic;

namespace PizzaOrderingSystem.Web.ViewModels.ReviewViewModels
{
    public class AllReviewsViewModel
    {
        public ICollection<ReviewViewModel> Reviews { get; set; }
    }
}
