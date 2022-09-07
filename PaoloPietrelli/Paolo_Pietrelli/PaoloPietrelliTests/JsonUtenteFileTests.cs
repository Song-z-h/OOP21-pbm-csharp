using System.Threading.Tasks;
using NUnit.Framework;
using PaoloPietrelli;

namespace TestPaoloPietrelli
{
    
    public class JsonUtenteFileTests
    {
        private JsonUtenteFile prova;
        
        [SetUp]
        public void Setup()
        {
            prova = new JsonUtenteFile
            {
                FileName = "provaUtente.json"
            };
        }

        [Test]
        //new account successful
        [TestCase("Pippo", "Pluto", "paperino@inf.com", "PPPPLT99C05F205A", "oopspacca")]
        //new account successful
        [TestCase("Gino", "Pino", "gino@gin.com", "GNIPNI85S05A944R", "BEER")]
        public async Task Test1_Create_New_Account(string Name, string LastName, string Email, string FC, string Password)
        {
            await prova.CreateNewUser(Name, LastName, Email, FC, Password);
        }

        [Test]
        //new bank account successful 
        [TestCase("PPPPLT99C05F205A", "Poste Italiane")]
        //new bank account successful 
        [TestCase("GNIPNI85S05A944R", "Revolut")]
        public async Task Test2_Create_New_Bank_Account(string FC, string NameBankAccount)
        {
            await prova.NewBankAccount(FC, NameBankAccount);
        }
        
        [Test]
        [TestCase("PPPPLT99C05F205A", "Poste Italiane", "Stipendio", 700.55, "12/02/2022", "12:20")]
        [TestCase("GNIPNI85S05A944R", "Revolut", "Stipendio", 1700.55, "12/02/2022", "12:20")]
        public async Task Test3_New_Bank_Transaction(string FC, string NameBankAccount, string NameTransaction, double Amount, string Date, string Time)
        {
            await prova.NewBankTransaction(FC, NameBankAccount, NameTransaction, Amount, Date, Time);
        }
        
        [Test]
        [TestCase("GNIPNI85S05A944R", "Vacation")]
        [TestCase("PPPPLT99C05F205A", "Emergency")]
        public async Task Test4_Create_New_Money_Box(string FC, string NameMoneyBox)
        {
            await prova.NewMoneyBox(FC, NameMoneyBox);
        }
        
        [Test]
        [TestCase("PPPPLT99C05F205A", "Emergency", "Poste Italiane", 70.55, "Dollar", "12/02/2022", "12:20")]
        [TestCase("GNIPNI85S05A944R", "Vacation", "Revolut", 100.55, "Dollar", "12/02/2022", "12:20")]
        public async Task Test5_New_Money_Box_Transaction(string FC, string NameMoneyBox, string NameTransaction, double Amount, string Currency,string Date, string Time)
        {
            await prova.NewBankTransaction(FC, NameTransaction, NameMoneyBox, -Amount, Date, Time);
            await prova.NewMoneyBoxTransaction(FC, NameMoneyBox, NameTransaction, Amount, Currency, Date, Time);
        }
        
        [Test]
        [TestCase("GNIPNI85S05A944R", "Binance")]
        [TestCase("PPPPLT99C05F205A", "EToro")]
        public async Task Test6_Create_New_Investment_Account(string FC, string NameInvestmentAccount)
        {
            await prova.NewInvestmentAccount(FC, NameInvestmentAccount);
        }
        
        [Test]
        [TestCase("GNIPNI85S05A944R", "Binance", "Bitcoin","BTC")]
        [TestCase("PPPPLT99C05F205A", "EToro", "Apple","APPL")]
        public async Task Test7_Create_New_Investment_Account(string FC, string NameInvestmentAccount, string NameAsset, string SymbolAsset)
        {
            await prova.NewInvestmentAccountAsset(FC, NameInvestmentAccount, NameAsset, SymbolAsset);
        }
        
        
        [Test]
        [TestCase("GNIPNI85S05A944R", "Binance", "BTC","exchange", 1.22,"11/03/2022","10:55")]
        [TestCase("PPPPLT99C05F205A", "EToro", "APPL", "investment", 5,"11/03/2022","10:55")]
        public async Task Test8_Create_New_Investment_Account_Transaction(string FC, string NameInvestmentAccount, string SymbolAsset, string NameTransaction, double Amount, string Data, string Time)
        {
            await prova.NewAssetTransaction(FC, NameInvestmentAccount, SymbolAsset, NameTransaction, Amount, Data, Time);
        }
        
        [Test]
        [TestCase("GNIPNI85S05A944R", "Binance", "Vacation","BTC", 0.22,"11/03/2022","10:55")]
        [TestCase("PPPPLT99C05F205A", "EToro", "Emergency", "APPL", 2,"11/03/2022","10:55")]
        public async Task Test9_Mix_Transaction(string FC, string NameInvestmentAccount, string NameMoneyBox,string SymbolAsset, double Amount, string Data, string Time)
        {
            await prova.NewAssetTransaction(FC, NameInvestmentAccount, SymbolAsset, NameMoneyBox, -Amount, Data, Time);
            await prova.NewMoneyBoxTransaction(FC, NameMoneyBox, NameInvestmentAccount, Amount, SymbolAsset, Data, Time);
        }
        
    }
}
