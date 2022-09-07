namespace PaoloPietrelli;

public class Utente
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string FC { get; set; }
    public string Password { get; set; }
    public List<BankAccount> BankAccounts { get; set; }
    public List<MoneyBox> MoneyBoxes { get; set; }
    public List<InvestmentAccount> InvestmentAccounts { get; set; }
}