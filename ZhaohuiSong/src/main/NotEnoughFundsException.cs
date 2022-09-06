using System;

namespace Budmate
{
    public class NotEnoughFundsException : ArgumentException

    {
        public NotEnoughFundsException()
        {
        }

        public NotEnoughFundsException(string message) : base(message)
        {
        }

        public NotEnoughFundsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}