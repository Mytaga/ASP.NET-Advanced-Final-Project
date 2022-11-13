using PizzaOrderingSystem.Common;

namespace PizzaOrderingSystem.Web.ViewModels.ShopViewModels
{
    public class ShopViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string ImageUrl => GlobalConstants.ShopImageUrl;
    }
}
