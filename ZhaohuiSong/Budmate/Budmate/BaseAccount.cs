using System;

namespace Budmate
{
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

        protected abstract bool CheckDepositValidity(double amount);

        public double GetBalance() => _balance;

        public abstract string GetId();
    }
}