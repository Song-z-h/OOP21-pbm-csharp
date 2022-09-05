using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OOP21pbm_csharp.AlessandroStefani
{
    public class ProfileEconomy : IProfileEconomy
    {
        private double _totalBalance;
        private List<InvestmentAccount> _invAccs = new ArrayList<>();
        private List<HoldingAccount> _holAccs = new ArrayList<>();

        public void AddHoldingAccount(HoldingAccount newAccount) => _holAccs.Add(newAccount);

        public void AddInvestmentAccount(InvestmentAccount newAccount) => _invAccs.Add(newAccount);

        public List<HoldingAccount> GetHoldingAccounts() => _holAccs;

        public List<InvestmentAccount> GetInvestmentAccounts() => _invAccs;

        public double GetTotalBalance()
        {
            updateTotalBalance();
            return _totalBalance;
        }

        private void updateTotalBalance()
        {
            _totalBalance = 0.0;
            _invAccs.ForEach(delegate (InvestmentAccount invAcc)
            {
                _totalBalance += invAcc.GetTotalValue();
            });
            _holAccs.ForEach(delegate (HoldingAccount holAcc) 
            {
                _totalBalance += holAcc.GetTotalValue();
            });
        }
    }
}
