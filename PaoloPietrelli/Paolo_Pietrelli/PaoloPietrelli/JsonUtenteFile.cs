namespace PaoloPietrelli;
using System;
using System.IO;
using Windows.Storage;
using Newtonsoft.Json;
public class JsonUtenteFile
{
    public string? FileName { get; set; }

    public String getFullPathOfFile()
    {
        string FullPathFile;

        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        
        FullPathFile = projectDirectory + "/" + this.FileName;

        return FullPathFile;
    }

    private async Task<List<Utente>> getUtenteList()
    {
        //Retrieve the source file that containing JSON.
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync(this.getFullPathOfFile());
        
        //Read text content (JSON data) from source file.
        var jsonString = await FileIO.ReadTextAsync(file);
        
        //Deserialize JSON data to List of utente object.
        var utenteList = JsonConvert.DeserializeObject<List<Utente>>(jsonString);

        return utenteList;
    }

    private async Task writeOnFile(List<Utente> utenteList)
    {
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync(this.getFullPathOfFile());
        //Serialize the student list, you will get JSON data string as return from that method.
        var updatedJsonString = JsonConvert.SerializeObject(utenteList, Formatting.Indented);
        
        //Save JSON string to the source file.
        await FileIO.WriteTextAsync(file, updatedJsonString);   
    }

    public async Task CreateNewUser(string Name, string LastName, string Email, string FC, string Password)
    {
        var newUtente = new Utente
        {
            Name = Name,
            LastName = LastName,
            Email = Email,
            FC = FC,
            Password = Password,
            BankAccounts = new List<BankAccount>(),
            MoneyBoxes = new List<MoneyBox>(),
            InvestmentAccounts = new List<InvestmentAccount>()
        };

        bool exist = await this.userExist(FC);

        if (!exist)
        {
            var utenteList = await this.getUtenteList();
            utenteList.Add(newUtente);
            await this.writeOnFile(utenteList);
        }
        else
        {
            throw new ArgumentException("I'm sorry already exist a user with this fiscal code");
        }

    }

    public async Task<bool> userExist(String FC)
    {
        var utenteList = await this.getUtenteList();

        return utenteList.Any(utente => utente.FC == FC);
    }
    
    public async Task<bool> userPasswordCheck(string Email, string Password)
    {
        var utenteList = await this.getUtenteList();

        return utenteList.Any(utente => utente.Email == Email && utente.Password == Password);
    }

    public async Task NewBankAccount(string FC, string NameBanckAccount)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.BankAccounts.Any(bank => bank.NameBanckAccount == NameBanckAccount))
                    {
                        throw new ArgumentException("Already exist a bank account with this name");
                    }
                    else
                    {
                        var newBank = new BankAccount
                        {
                            NameBanckAccount = NameBanckAccount,
                            Transactions = new List<Transaction>()
                        };
                        utente.BankAccounts.Add(newBank);
                        await this.writeOnFile(utenteList);
                    }
                } 
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a new bank account because user doesn't exist");
        }
    }
    
    public async Task NewBankTransaction(string FC, string NameBanckAccount, string nameTransaction, double amount, string date, string time)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.BankAccounts.Any(bank => bank.NameBanckAccount == NameBanckAccount))
                    {
                        foreach (var bank in utente.BankAccounts)
                        {
                            if (bank.NameBanckAccount == NameBanckAccount)
                            {
                                if (bank.transactionIsPossible(amount))
                                {
                                    var newBankTransaction = new Transaction()
                                    {
                                        NameTransaction = nameTransaction,
                                        Amount = amount,
                                        Currency = "Dollar",
                                        Date = date,
                                        Time = time
                                    };
                                
                                    bank.Transactions.Add(newBankTransaction);
                                    await this.writeOnFile(utenteList);
                                }
                                else
                                {
                                    throw new ArgumentException("Your balance is " + bank.getTotalAmount() + 
                                                                " Dollar, " + "not enough for the requested transaction");
                                }
                            }
                        }
                            
                    }
                    else
                    {
                        throw new ArgumentException("doesn't exist a bank account with this name for this user, please check data");
                    }
                }
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a new bank account transaction because user doesn't exist");
        }
    }
    
    public async Task NewMoneyBox(string FC, string NameMoneyBox)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.MoneyBoxes.Any(moneyBox => moneyBox.NameMoneyBox == NameMoneyBox))
                    {
                        throw new ArgumentException("Already exist a money box with this name");
                    }
                    else
                    {
                        var newMoneyBox = new MoneyBox()
                        {
                            NameMoneyBox = NameMoneyBox,
                            Transactions = new List<Transaction>()
                        };
                        utente.MoneyBoxes.Add(newMoneyBox);
                        await this.writeOnFile(utenteList);
                    }
                } 
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a new Money Box because user doesn't exist");
        }
    }
    
    public async Task NewMoneyBoxTransaction(string FC, string NameMoneyBox, string nameTransaction, double amount, string currency, string date, string time)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.MoneyBoxes.Any(MoneyBox => MoneyBox.NameMoneyBox == NameMoneyBox))
                    {
                        foreach (var MoneyBox in utente.MoneyBoxes)
                        {
                            if (MoneyBox.NameMoneyBox == NameMoneyBox)
                            {
                                if (MoneyBox.transactionIsPossible(amount, currency))
                                {
                                    var newMoneyBoxTransaction = new Transaction()
                                    {
                                        NameTransaction = nameTransaction,
                                        Amount = amount,
                                        Currency = currency,
                                        Date = date,
                                        Time = time
                                    };
                                
                                    MoneyBox.Transactions.Add(newMoneyBoxTransaction);
                                    await this.writeOnFile(utenteList); 
                                }
                                else
                                {
                                    var totalAmount = MoneyBox.getTotalAmount();
                                    if (totalAmount.Any(amount => amount.SymbolAsset == currency))
                                    {
                                        foreach (var value in totalAmount)
                                        {
                                            if (value.SymbolAsset == currency)
                                            {
                                                throw new ArgumentException("currency available in this money box is " + 
                                                                            value.Amount + " " + currency + " not enough for the transaction");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new ArgumentException("You miss this currency in your money box balance");
                                    }
                                }
                                
                            }
                        }
                            
                    }
                    else
                    {
                        throw new ArgumentException("doesn't exist a Money Box with this name for this user, please check data");
                    }
                }
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a the money box transaction because user doesn't exist");
        }

    }

    public async Task NewInvestmentAccount(string FC, string NameInvestmentAccount)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.InvestmentAccounts.Any(investmentAccount => 
                            investmentAccount.NameInvestmentAccount == NameInvestmentAccount))
                    {
                        throw new ArgumentException("Already exist a Investment Account with this name");
                    }
                    else
                    {
                        var newInvestmentAccount = new InvestmentAccount()
                        {
                            NameInvestmentAccount = NameInvestmentAccount,
                            Assets = new List<Asset>()
                        };
                        utente.InvestmentAccounts.Add(newInvestmentAccount);
                        await this.writeOnFile(utenteList);
                    }
                } 
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a new Investment Account because user doesn't exist");
        }
    
    }
    
    public async Task NewInvestmentAccountAsset(string FC, string NameInvestmentAccount, string NameAsset, string SymbolAsset)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.InvestmentAccounts.Any(investmentAccount => 
                            investmentAccount.NameInvestmentAccount == NameInvestmentAccount))
                    {
                        foreach (var invAccount in utente.InvestmentAccounts)
                        {
                            if (invAccount.NameInvestmentAccount == NameInvestmentAccount)
                            {
                                if (invAccount.Assets.Any(asset =>
                                        asset.SymbolAsset == SymbolAsset))
                                {
                                    throw new ArgumentException("This Asset already exist in investment account " + NameInvestmentAccount);
                                }
                                else
                                {
                                    var newAsset = new Asset()
                                    {
                                        NameAsset = NameAsset,
                                        SymbolAsset = SymbolAsset,
                                        Transactions = new List<Transaction>()
                                    };
                                    
                                    invAccount.Assets.Add(newAsset);
                                    await this.writeOnFile(utenteList);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("This Investment Account doesn't exist, please check input data");
                    }
                } 
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a new asset because user doesn't exist");
        }
    
    }
    
    public async Task NewInvestmentAccountAsset(string FC, string NameInvestmentAccount, string SymbolAsset)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.InvestmentAccounts.Any(investmentAccount => 
                            investmentAccount.NameInvestmentAccount == NameInvestmentAccount))
                    {
                        foreach (var invAccount in utente.InvestmentAccounts)
                        {
                            if (invAccount.NameInvestmentAccount == NameInvestmentAccount)
                            {
                                if (invAccount.Assets.Any(asset =>
                                        asset.SymbolAsset == SymbolAsset))
                                {
                                    throw new ArgumentException("This Asset already exist in investment account " + NameInvestmentAccount);
                                }
                                else
                                {
                                    var newAsset = new Asset()
                                    {
                                        NameAsset = "Unknown",
                                        SymbolAsset = SymbolAsset,
                                        Transactions = new List<Transaction>()
                                    };
                                    
                                    invAccount.Assets.Add(newAsset);
                                    await this.writeOnFile(utenteList);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("This Investment Account doesn't exist, please check input data");
                    }
                } 
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a new asset because user doesn't exist");
        }
    
    }
    
    public async Task NewAssetTransaction(string FC, string NameInvestmentAccount, string SymbolAsset, string NameTransaction, double Amount, string Data, string Time)
    {
        bool check = await this.userExist(FC);
        if (check)
        {
            var utenteList = await this.getUtenteList();
            foreach (var utente in utenteList)
            {
                if (utente.FC == FC)
                {
                    if (utente.InvestmentAccounts.Any(investmentAccount => 
                            investmentAccount.NameInvestmentAccount == NameInvestmentAccount))
                    {
                        foreach (var invAccount in utente.InvestmentAccounts)
                        {
                            if (invAccount.NameInvestmentAccount == NameInvestmentAccount)
                            {
                                if (invAccount.Assets.Any(asset =>
                                        asset.SymbolAsset == SymbolAsset))
                                {
                                    foreach (var asset in invAccount.Assets)
                                    {
                                        if (asset.SymbolAsset == SymbolAsset)
                                        {
                                            if (asset.transactionIsPossible(Amount))
                                            {
                                                var newTransaction = new Transaction()
                                                {
                                                    NameTransaction = NameTransaction,
                                                    Currency = SymbolAsset,
                                                    Amount = Amount,
                                                    Date = Data,
                                                    Time = Time
                                                };
                                            
                                                asset.Transactions.Add(newTransaction);
                                                await this.writeOnFile(utenteList); 
                                            }
                                            else
                                            {
                                                throw new ArgumentException("Transaction not possible, asset " + 
                                                                            SymbolAsset + " balance is " + asset.getTotalAmount());
                                            }
                                            
                                        }
                                    }
                                }
                                else
                                {
                                    if (Amount >= 0)
                                    {
                                        await NewInvestmentAccountAsset(FC, NameInvestmentAccount, SymbolAsset);
                                        await NewAssetTransaction(FC, NameInvestmentAccount, SymbolAsset, NameTransaction,
                                            Amount, Data, Time);
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Withdrawal not allowed, currency "+ SymbolAsset +" not held");
                                    }
                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("This Investment Account doesn't exist, please check input data");
                    }
                } 
            }
        }
        else
        {
            throw new ArgumentException("Impossible add a new asset because user doesn't exist");
        }
    
    }
}