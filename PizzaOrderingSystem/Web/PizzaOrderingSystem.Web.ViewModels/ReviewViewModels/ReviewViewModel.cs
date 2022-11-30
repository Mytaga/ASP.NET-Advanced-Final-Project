using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using System;
using System.Globalization;

namespace PizzaOrderingSystem.Web.ViewModels.ReviewViewModels
{
    public class ReviewViewModel : IMapFrom<Review>
    {
        public string Id { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public string PublishedOn { get; set; }

        public string UserId { get; set; }
    }
}
