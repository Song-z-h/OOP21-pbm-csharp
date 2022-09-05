using System;
using System.Collections.Generic;
using System.Text;

namespace OOP21pbm_csharp.AlessandroStefani
{
	public interface IProfileEconomy
	{
        List<InvestmentAccount> GetInvestmentAccounts();

        List<HoldingAccount> GetHoldingAccounts();

        void AddInvestmentAccount(InvestmentAccount newAccount);

        void AddHoldingAccount(HoldingAccount newAccount);

        double GetTotalBalance();
    }
}


