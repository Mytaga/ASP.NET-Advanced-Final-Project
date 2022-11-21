using System;

namespace PizzaOrderingSystem.Services.Exceptions
{
    public class PizzaOrderingException : ApplicationException
    {
        public PizzaOrderingException()
        {

        }

        public PizzaOrderingException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}
