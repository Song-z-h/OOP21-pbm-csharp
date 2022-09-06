using System;

namespace Budmate
{
    /// <inheritdoc />
    public abstract class BaseAccount : IAccount
    {
        private double _balance;
        
        public void Withdraw(double amount)
        {
            if (CheckWithdrawValidity(amount)) 
            {
                _balance -= amount;
            } else 
            {
                throw new NotEnoughFundsException("Sorry, your fund is insufficient!");
            }
        }

        /// <summary>
        ///Check if the user is able to do withdrawl operation
        /// </summary>
        /// <param name="amount">the amount of money</param>
        /// <returns>if the operation went through successfully</returns>
        protected abstract bool CheckWithdrawValidity(double amount);

        public void Deposit(double amount)
        {
            if (CheckDepositValidity(amount))
            {
                _balance += amount;
            }
            else
            {
                throw new ArgumentException("Something went wrong during the operation, please try it again!");
            }
        }

        /// <summary>
        /// Check if the user is able to do deposit operation
        /// </summary>
        /// <param name="amount">the amount of money</param>
        /// <returns>if the operation went through successfully</returns>
        protected abstract bool CheckDepositValidity(double amount);

        public double GetBalance() => _balance;

        public abstract string GetId();
    }
}