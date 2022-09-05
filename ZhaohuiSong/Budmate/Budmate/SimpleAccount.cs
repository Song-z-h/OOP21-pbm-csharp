using System;

namespace Budmate
{
    public class SimpleAccount : BaseAccount
    {
        private readonly string _id;
        
        public SimpleAccount(string id, double amount)
        {
            _id = id;
           Deposit(amount);
        }

        public SimpleAccount(string id) => _id = id;

        protected override bool CheckWithdrawValidity(double amount) => GetBalance() >= amount;

        protected override bool CheckDepositValidity(double amount) => true;

        public override string GetId() => _id;

    }
}