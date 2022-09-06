using System;

namespace Budmate
{
    public class SimpleAccount : BaseAccount
    {
        private readonly string _id;
        private readonly Func<double, double, bool> _predicateWithdraw, _predicateDeposit;

        public SimpleAccount(string id, double amount = 0) : this(id,
            (balance, money) => balance >= money,
            (balance, money) => true, amount)
        {
        }

        public SimpleAccount(string id, Func<double, double, bool> predicateWithdraw,
            Func<double, double, bool> predicateDeposit, double amount = 0)
        {
            _id = id;
            _predicateWithdraw = predicateWithdraw;
            _predicateDeposit = predicateDeposit;
            Deposit(amount);
        }

        public SimpleAccount(string id) => _id = id;

        protected override bool CheckWithdrawValidity(double amount) => _predicateWithdraw(GetBalance(), amount);

        protected override bool CheckDepositValidity(double amount) => _predicateDeposit(GetBalance(), amount);

        public override string GetId() => _id;

        /// <summary>
        /// Overloading operator to compare balances
        /// </summary>
        /// <param name="a">first account</param>
        /// <param name="b">second account</param>
        /// <returns>if they have equal balance</returns>
        public static bool operator ==(SimpleAccount a, SimpleAccount b) =>
            Math.Abs(a.GetBalance() - b.GetBalance()) < 0.01;


        public static bool operator !=(SimpleAccount a, SimpleAccount b) =>
            Math.Abs(a.GetBalance() - b.GetBalance()) > 0.01;


        private bool Equals(SimpleAccount other) => _id == other._id;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SimpleAccount) obj);
        }

        public override int GetHashCode()
        {
            return (_id != null ? _id.GetHashCode() : 0);
        }
    }
}