namespace Budmate
{
    /// <summary>
    /// Represents a simple interface for bank account. 
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// retrieve the money from the account.
        /// </summary>
        /// <param name="amount">the amount of money</param>
        void Withdraw(double amount);
        
        /// <summary>
        /// save the money to the account
        /// </summary>
        /// <param name="amount">the amount of money</param>
        void Deposit(double amount);
        
        /// <summary>
        /// show the current balance.
        /// </summary>
        /// <returns>current balance</returns>
        double GetBalance();
        
        /// <summary>
        /// show the name of this account.
        /// </summary>
        /// <returns>current name of the account</returns>
        string GetId();
    }
}