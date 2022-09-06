using System;

namespace Budmate
{
    public interface IInvestmentAccountTypeFactory
    {
        /// <summary>
        /// create a simple plan for using the account for free
        /// </summary>
        /// <param name="id">account name</param>
        /// <param name="initialAmount">the initial money</param>
        /// <returns>an account for investment</returns>
        IInvestmentAccount CreateForFree(string id, double initialAmount = 0);
        
        /// <summary>
        /// create a simple plan for using the account with an implied operation fee.
        /// </summary>
        /// <param name="fees">fee implied</param>
        /// <param name="id">account name</param>
        /// <param name="initialAmount">the initial money</param>
        /// <returns>an account for investment</returns>
        IInvestmentAccount CreateWithOperationFees(Func<double, double> fees, string id, double initialAmount = 0);
    }
}