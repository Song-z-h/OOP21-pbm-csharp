using NUnit.Framework;

namespace Budmate
{
    [TestFixture]
    public class TestAccounts
    {
        
        [Test]
        public void TestEquality()
        {
            IAccount acc1 = new SimpleAccount("uni credit");
            IAccount acc2 = new SimpleAccount("san polo", 0);
            IAccount acc3 = new SimpleAccount("san polo", 200);
            Assert.IsFalse(acc1.Equals(acc2));
            Assert.IsTrue(acc2.Equals(acc3));
            Assert.IsFalse(acc1.Equals(acc3));
        }
        
        [Test]
        public void TestEqualBalance()
        {
            var acc1 = new SimpleAccount("uni credit");
            var acc2 = new SimpleAccount("san polo", 24.2);
            acc1.Deposit(24.2);
            Assert.IsTrue(acc1 == acc2);
            acc1.Withdraw(24);
            Assert.IsTrue(acc1 != acc2);
        }
    }
}