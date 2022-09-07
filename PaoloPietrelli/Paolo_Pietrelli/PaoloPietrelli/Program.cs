using System;
using System.IO;
using Windows.Storage;
using Newtonsoft.Json;

namespace PaoloPietrelli
{
    //This class is use for manually testing,
    //the automatic test is available in the project PaoloPietrelliTests
    class Program
    {
        static async Task Main(string[] args)
        {
            
            JsonUtenteFile prova = new JsonUtenteFile
            {
                FileName = "provaUtente.json"
            };
            
            //await prova.CreateNewUser("Pippo", "Pluto", "paperino@inf.com", "PPPPLT99C05F205A", "oopspacca");
            
            bool verify = await prova.userExist("PPYPLT99C05F205A");
            Console.WriteLine(verify);
            
            bool verifyEmailPassword = await prova.userPasswordCheck("paperino@inf.com", "opspacca");
            Console.WriteLine(verifyEmailPassword);
            
            verifyEmailPassword = await prova.userPasswordCheck("paperino@inf.com", "oopspacca");
            Console.WriteLine(verifyEmailPassword);

            //await prova.NewBankAccount("PPPPLT99C05F205A", "Poste Italiane");

            //await prova.NewBankTransaction("PPPPLT99C05F205A", "Poste Italiane", "Stipendio", 700.55, "12/02/2022", "12:20");

            //await prova.NewMoneyBox("GNIPNI85S05A944R", "Vacation");
            
            //await prova.NewMoneyBoxTransaction("PPPPLT99C05F205A", "Vacation", "Revolut", 2, "ETH" ,"12/02/2022", "12:20");
            
            //await prova.NewMoneyBoxTransaction("PPPPLT99C05F205A", "Vacation", "gift", 20.0, "Euro" ,"12/02/2022", "12:20");
            //await prova.NewBankTransaction("PPPPLT99C05F205A", "Revolut", "CONAD", -700.55, "12/02/2022", "12:20");

            //await prova.NewMoneyBoxTransaction("PPPPLT99C05F205A", "Vacation", "Revolut", -3, "ETH" ,"12/02/2022", "12:20");
            //await prova.NewInvestmentAccount("GNIPNI85S05A944R", "Binance");

            //await prova.NewInvestmentAccountAsset("GNIPNI85S05A944R", "Binance", "BTC");
            //await prova.NewAssetTransaction("GNIPNI85S05A944R", "Binance", "DOT","prova", 1.22,"11/03/2022","10:55");
        }
    }


    

    
}
