namespace Budmate
{
    public interface IInvestmentAccount : IAccount
    {
        double GetInvestedBalance();
        
        double GetReturn(double netWorthInvested);
        
        double GetReturnInPercentage(double netWorthInvested);	
        
        void Invest(double amounts);
        
        void CashOut(double amounts);
    }
}