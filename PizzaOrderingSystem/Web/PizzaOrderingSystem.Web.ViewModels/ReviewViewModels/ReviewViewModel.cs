using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using System;

namespace PizzaOrderingSystem.Web.ViewModels.ReviewViewModels
{
    public class ReviewViewModel : IMapFrom<Review>
    {
        public string AuthorName { get; set; }

        public string Content { get; set; }

        public string PublishedOn { get; set; }

        public string UserId { get; set; }
    }
}
