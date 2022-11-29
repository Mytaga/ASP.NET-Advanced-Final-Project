using PizzaOrderingSystem.Data.Models;
using PizzaOrderingSystem.Services.Mapping;

namespace PizzaOrderingSystem.Web.ViewModels.OrderViewModels
{
    public class OrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string TimeOfOrder { get; set; }

        public string TotalPrice { get; set; }

        public string DeliveryType { get; set; }

        public string OrderId { get; set; }

        public string Status { get; set; }
    }
}