using System;
using Budmate;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class TestAccounts
    {
        [Test]
        public void TestEquality()
        {
            IAccount acc1 = new SimpleAccount("uni credit");
            IAccount acc2 = new SimpleAccount("san polo", (balance, money) => balance >= money,
                (balance, money) => true);
            IAccount acc3 = new SimpleAccount("san polo", (balance, money) => balance >= money,
                (balance, money) => true, 200);
            Assert.IsFalse(acc1.Equals(acc2));
            Assert.IsTrue(acc2.Equals(acc3));
            Assert.IsFalse(acc1.Equals(acc3));
        }

        [Test]
        public void TestEqualBalance()
        {
            SimpleAccount acc1 = new SimpleAccount("uni credit", 0);
            SimpleAccount acc2 = new SimpleAccount("san polo", 24.2);
            IAccount acc3 = acc1;
            IAccount acc4 = acc2;
            acc1.Deposit(24.2);
            Assert.IsTrue(acc1 == 24.2);
            Assert.IsFalse(acc3 == acc4); //because acc3 and acc4 are IAccounts
            acc1.Withdraw(24);
            Assert.IsTrue(acc1 != acc2);
        }

        [Test]
        public void TestFreeInvestAccount()
        {
            IInvestmentAccountTypeFactory factory = new InvestmentAccountTypeFactoryImpl();
            IAccount acc1 = factory.CreateForFree("uni");
            Assert.IsTrue(Math.Abs(acc1.GetBalance()) < 0.0001);

            acc1.Deposit(100);
            Assert.IsTrue(Math.Abs(acc1.GetBalance()) < 100.0001 && Math.Abs(acc1.GetBalance()) > 99.9999);

            acc1.Withdraw(99);
            Assert.IsTrue(Math.Abs(acc1.GetBalance()) < 1.1); //1.000000001
        }

        [Test]
        public void TestFeeInvestAccount()
        {
            IInvestmentAccountTypeFactory factory = new InvestmentAccountTypeFactoryImpl();
            IInvestmentAccount acc1 = factory.CreateWithOperationFees(d => d * 0.01, "uni"); // % 1% of fee
            Assert.IsTrue(Math.Abs(acc1.GetBalance()) < 0.0001);
            
            acc1.Deposit(10);
            Assert.IsTrue(acc1.GetBalance() < 10, $"balance = {acc1.GetBalance()}"); // 9.9
            acc1.Invest(5);
            Assert.IsTrue(acc1.GetBalance() < 5, $"balance = {acc1.GetBalance()}"); // 4.85
            Assert.IsTrue(acc1.GetInvestedBalance() >= 4.99);
            
            acc1.Withdraw(4);
            Assert.IsTrue(acc1.GetBalance() < 1, $"balance = {acc1.GetBalance()}"); // 0.81
        }
    }
}