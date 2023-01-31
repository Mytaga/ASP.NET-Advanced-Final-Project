using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;
using System;

namespace PizzaOrderingSystem.Web.ViewModels.ProductViewModels
{
    public class ListProductCategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}