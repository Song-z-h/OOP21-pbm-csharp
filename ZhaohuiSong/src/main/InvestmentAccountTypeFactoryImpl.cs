using System;

namespace Budmate
{
    /// <inheritdoc />
    public class InvestmentAccountTypeFactoryImpl : IInvestmentAccountTypeFactory
    {
        /// <inheritdoc />
        public IInvestmentAccount CreateForFree(string id, double initialAmount) =>
            new Generalised<string>(id, "", fee => 0, s => "", (state, amount) => amount >= 0, initialAmount);

        /// <inheritdoc />
        public IInvestmentAccount CreateWithOperationFees(Func<double, double> fees, string id, double initialAmount) =>
            new Generalised<string>(id, "", fees, s => "",
                (state, amount) => amount >= 0 && fees(amount) <= amount, initialAmount);
    }

    internal class Generalised<T> : IInvestmentAccount
    {
        private double InvestedBalance { get; set; }
        private T State { get; set; }

        private readonly IAccount _account;
        private readonly Func<double, double> _operationFee;

        public Generalised(string id, T initialState, Func<double, double> operationFee,
            Func<T, T> stageChanger, Func<T, double, bool> predicate, double initialAmount)
        {
            _operationFee = operationFee;
            State = initialState;
            _account = new SimpleAccount(id, (balance, amount) =>
                    operationFee(amount) <= balance && predicate(State, amount) && ChangeState(stageChanger),
                (balance, amount) => predicate(State, amount), initialAmount);
        }


        public double GetInvestedBalance() => InvestedBalance;


        private bool ChangeState(Func<T, T> f)
        {
            State = f(State);
            return true;
        }

        public double GetReturn(double netWorthInvested) => netWorthInvested - GetInvestedBalance();

        public double GetReturnInPercentage(double netWorthInvested) =>
            InvestedBalance != 0 ? GetReturn(netWorthInvested) / GetInvestedBalance() : 0;

        public void Invest(double amounts)
        {
            Withdraw(amounts);
            IncreaseInvestedMoney(amounts);
        }

        private void IncreaseInvestedMoney(double amounts) => InvestedBalance += amounts;

        public void CashOut(double amounts)
        {
            DecreaseInvestedMoney(amounts);
            Deposit(amounts);
        }

        private void DecreaseInvestedMoney(double amounts) => InvestedBalance -= amounts;

        private void ChargeOperationFees(double amount) => _account.Withdraw(_operationFee(amount));

        public void Withdraw(double amount)
        {
            ChargeOperationFees(amount);
            _account.Withdraw(amount);
        }

        public void Deposit(double amount)
        {
            _account.Deposit(amount);
            ChargeOperationFees(amount);
        }

        public double GetBalance() => _account.GetBalance();

        public string GetId() => _account.GetId();
    }
}