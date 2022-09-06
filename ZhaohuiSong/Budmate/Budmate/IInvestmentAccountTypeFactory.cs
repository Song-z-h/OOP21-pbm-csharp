namespace Budmate
{
    public interface IInvestmentAccountTypeFactory
    {
        IInvestmentAccount CreateForFree(string id);
    }
}