using System;

namespace Budmate
{
    /// <summary>
    /// Exception for when running out of money
    /// </summary>
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