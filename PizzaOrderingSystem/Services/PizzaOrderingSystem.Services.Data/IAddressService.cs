using PizzaOrderingSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Services.Data
{
    public interface IAddressService
    {
        Task AddAddress(Address address);
    }
}
