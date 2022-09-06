using System;

namespace Budmate
{
    public interface IInvestmentAccountTypeFactory
    {
        IInvestmentAccount CreateForFree(string id, double initialAmount = 0);
        
        IInvestmentAccount CreateWithOperationFees(Func<double, double> fees, string id, double initialAmount = 0);
    }
}