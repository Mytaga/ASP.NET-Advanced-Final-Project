using System;

namespace PizzaOrderingSystem.Services.Exceptions
{
    public class Guard : IGuard
    {
        public void AgainstNull<T>(T value, string? errorMessage = null)
        {
            if (value == null)
            {
                var exception = errorMessage == null ?
                    new PizzaOrderingException() :
                    new PizzaOrderingException(errorMessage);

                throw exception;
            }
        }
    }
}
