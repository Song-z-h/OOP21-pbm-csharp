namespace PaoloPietrelli;

public class Asset
{
    public string NameAsset { get; set; }
    public string SymbolAsset { get; set; }
    public List<Transaction> Transactions { get; set; }
    
    public double getTotalAmount()
    {
        double totalAmount = 0;
            
        foreach (var transaction in Transactions)
        {
            totalAmount += transaction.Amount;
        }

        return totalAmount;
    }

    public bool transactionIsPossible(double amount)
    {
        return (getTotalAmount() + amount) >= 0 ? true : false;
    }
}