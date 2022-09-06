namespace Budmate
{
    public interface IInvestmentAccount : IAccount
    {
        /// <summary>
        /// Get the currently invested money 
        /// </summary>
        /// <returns>money in dollar</returns>
        double GetInvestedBalance();
        
        /// <summary>
        /// get the total return from investing
        /// </summary>
        /// <param name="netWorthInvested">the current worth of your assets bought</param>
        /// <returns>money in dollar</returns>
        double GetReturn(double netWorthInvested);
        
        /// <summary>
        /// get the money earned in percentage.
        /// </summary>
        /// <param name="netWorthInvested">the current worth of your assets bought</param>
        /// <returns>money in dollar</returns>
        double GetReturnInPercentage(double netWorthInvested);	
        
        /// <summary>
        ///increase the invested money
        /// </summary>
        /// <param name="amounts">the amount of money</param>
        void Invest(double amounts);
        
        /// <summary>
        /// decrease the invested money i.e cashing out from the market
        /// </summary>
        /// <param name="amounts">the amount of money</param>
        void CashOut(double amounts);
    }
}